<template>
  <div class="container" v-if="!cookieConsentAnswered && isEuropean">
    <div class="row">
      <div class="col-sm-3 col-6 mx-auto">
        <div class="modal fade show" tabindex="-1" aria-labelledby="cookieModal" aria-hidden="true">
          <div class="modal-dialog modal-danger modal-dialog-centered modal-" role="document">
            <div class="modal-content">
              <div class="modal-body p-0">
                <div class="modal-content">
                    <div class="modal-body text-center">
                      <picture>
                        <source 
                          media="(min-width: 576px)"
                          width="261"
                          height="229"
                          srcset="../assets/img/cookie.webp">
                        <img 
                          width="200"
                          height="158"
                          class="image-center img-fluid"
                          src="../assets/img/cookie_mobile.webp" 
                          alt="cookie">
                      </picture>
                      <h5 class="modal-title">{{ $t('cookie.title') }}</h5>
                      <p>{{ $t('cookie.subtitle') }}</p>
                      <div v-if="showCustomize" class="text-start">
                        <div class="form-check form-switch">
                          <input class="form-check-input" type="checkbox" id="analytics" v-model="analytics">
                          <label class="form-check-label" for="analytics">{{ $t('cookie.analytics') }}</label>
                        </div>
                        <div class="form-check form-switch">
                          <input class="form-check-input" type="checkbox" id="advertisement" v-model="advertisement">
                          <label class="form-check-label" for="advertisement">{{ $t('cookie.advertisement') }}</label>
                        </div>
                      </div>
                    </div>
                    <div class="modal-footer" v-if="!showCustomize">
                      <button type="button" class="btn bg-gradient-secondary" data-bs-dismiss="modal" @click="save">{{ $t('cookie.accept') }}</button>
                      <button type="button" class="btn bg-gradient-primary" @click="customizeCookies">{{ $t('cookie.customise') }}</button>
                    </div>
                    <div class="modal-footer" v-else>
                      <button type="button" class="btn bg-gradient-secondary" data-bs-dismiss="modal" @click="acceptAllCookies">{{ $t('cookie.acceptAll') }}</button>
                      <button type="button" class="btn bg-gradient-secondary" data-bs-dismiss="modal" @click="rejectAllCookies">R{{ $t('cookie.rejectAll') }}</button>
                      <button type="button" class="btn bg-gradient-primary" @click="save">{{ $t('cookie.save') }}</button>
                    </div>
                  </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="modal-backdrop fade show" v-if="!cookieConsentAnswered"></div>
</template>

<script>
import { ref } from 'vue';
import { fetchPost } from '../store/web';
import { useSettings } from '../store';

import { bootstrap, optIn } from 'vue-gtag'
export default {
  name: "CookieModal",
  setup() {
    let { isEuropean } = useSettings();
    const cookieConsentAnswered = ref(false);
    const analytics = ref(true);
    const advertisement = ref(true);
    const showCustomize = ref(false);

    setShowModal();

    function setShowModal() {
      if(localStorage.getItem('cookieConsentAnswered'))
        cookieConsentAnswered.value = localStorage.getItem('cookieConsentAnswered');
    }

    function rejectAllCookies() {
      analytics.value = false;
      advertisement.value = false;
    }

    function acceptAllCookies() {
      analytics.value = true;
      advertisement.value = true;
    }

    function customizeCookies() {
      showCustomize.value = true;
    }
    
    async function save() {
      localStorage.setItem('cookieConsentAnswered', true);
      localStorage.setItem('acceptedCookies', JSON.stringify({ analytics: analytics.value, advertisement: advertisement.value }));
      cookieConsentAnswered.value = true;
      if (analytics.value)
        bootstrap().then(optIn)
      await fetchPost('/api/visitor', {'referrer': document.referrer, 'search': window.location.search, 'timeZone': Intl.DateTimeFormat().resolvedOptions().timeZone, 'analytics': analytics.value, 'advertisement': advertisement.value})
    }

    return { isEuropean, cookieConsentAnswered, showCustomize, analytics, advertisement, rejectAllCookies, acceptAllCookies, customizeCookies, save };
  },
}
</script>

<style scoped>
.show {
  display: block;
}
</style>