<template>
  <div class="container" v-if="!cookieConsentAnswered">
    <div class="row">
      <div class="col-sm-3 col-6 mx-auto">
        <div class="modal fade show" tabindex="-1" aria-labelledby="cookieModal" aria-hidden="true">
          <div class="modal-dialog modal-danger modal-dialog-centered modal-" role="document">
            <div class="modal-content">
              <div class="modal-body p-0">
                <div class="modal-content">
                    <div class="modal-body text-center">
                      <img class="image-center img-fluid w-50" src="../assets/img/cookie.webp" alt="image">
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
                      <button type="button" class="btn bg-gradient-secondary" data-bs-dismiss="modal" @click="acceptCookies">{{ $t('cookie.accept') }}</button>
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
import { event, optIn, optOut } from 'vue-gtag';

export default {
  name: "CookieModal",
  setup() {
    const cookieConsentAnswered = ref(false);
    const analytics = ref(false);
    const advertisement = ref(false);
    const showCustomize = ref(false);

    setShowModal();

    function setShowModal() {
      if(localStorage.getItem('cookieConsentAnswered'))
        cookieConsentAnswered.value = localStorage.getItem('cookieConsentAnswered');
    }

    function acceptCookies() {
      optIn();
      event('save-cookies', { 'value': { analytics: true, advertisement: true } });
      localStorage.setItem('cookieConsentAnswered', true);
      localStorage.setItem('acceptedCookies', JSON.stringify({ analytics: true, advertisement: true }));
      cookieConsentAnswered.value = true;
    }

    function rejectAllCookies() {
      optOut();
      event('save-cookies', { 'value': { analytics: false, advertisement: false } });
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

    function save() {
      if(analytics.value) {
        optIn();
      } else {
        optOut();
      }
      event('save-cookies', { 'value': { analytics: analytics.value, advertisement: advertisement.value } });
      localStorage.setItem('cookieConsentAnswered', true);
      localStorage.setItem('acceptedCookies', JSON.stringify({ analytics: analytics.value, advertisement: advertisement.value }));
      cookieConsentAnswered.value = true;
    }

    return { cookieConsentAnswered, showCustomize, analytics, advertisement, acceptCookies, rejectAllCookies, acceptAllCookies, customizeCookies, save };
  },
}
</script>

<style scoped>
.show {
  display: block;
}
</style>