<template>
  <div class="page-header">
    <picture class="d-none d-md-block">
      <source 
        media="(min-width: 576px)"
        srcset="../assets/img/bg1.webp">
      <img 
        class="position-absolute fixed-top ms-auto w-70 h-100 z-index-0 d-block border-radius-lg border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0 fit-cover"
        src="../assets/img/bg1_mobile.webp" 
        alt="image">
    </picture>
    <div class="container" v-if="page == 'contact'">
      <div class="row">
        <div class="col-lg-7 d-flex justify-content-center flex-column">
          <div class="card shadow-lg d-flex justify-content-center p-4 my-md-0 my-md-6 mt-7 mb-5">
            <div class="text-center">
              <h3>{{ $t('contact.title') }}</h3>
              <p class="mb-0" v-html="$t('contact.subTitle')"></p>
            </div>
            <div class="card-body pb-2">
              <div class="row">
                <div class="col-md-6 col-12 pe-md-1">
                  <label>{{ $t('contact.name') }}</label>
                  <input id="name-input" :class="{'is-invalid': (showError && !!inputErrors.name)}" class="form-control" :placeholder="$t('contact.name')" type="text" v-model="contact.name" autofocus :disabled="isLoggedIn">
                  <span class="text-danger" v-if="showError && inputErrors.name"><em><small>{{ $t(`${inputErrors.name}`) }}</small></em></span>
                </div>
                <div class="col-md-6 col-12 ps-md-1">
                  <label>{{ $t('contact.email') }}</label>
                  <input type="email" id="email-input" :class="{'is-invalid': (showError && !!inputErrors.email)}" class="form-control" :placeholder="$t('contact.emailPlaceholder')" v-model="contact.email" :disabled="isLoggedIn">
                  <span class="text-danger" v-if="showError && inputErrors.email"><em><small>{{ $t(`${inputErrors.email}`) }}</small></em></span>
                </div>
              </div>
              <div class="form-group mb-0 mt-md-0 mt-4">
                <label>{{ $t('contact.howCanWeHelp') }}</label>
                <textarea name="message" :class="{'is-invalid': (showError && !!inputErrors.message)}" class="form-control border-radius-lg" id="message-input" rows="6" :placeholder="$t('contact.problemDes')" v-model="contact.message"></textarea>
                <span class="text-danger" v-if="showError && inputErrors.message"><em><small>{{ $t(`${inputErrors.message}`) }}</small></em></span>
              </div>              
              <div v-if="!isLoggedIn" class="align-items-center justify-content-center">
                <div class="form-check d-flex align-items-center justify-content-center mt-3">
                  <input type="checkbox" class="form-check-input m-0" id="customCheck" v-model="acceptedPolicy">
                  <label class="custom-control-label m-0 mx-2" for="customCheck">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toPrivacyPolicy">{{ $t('policies.privacyPolicy') }}</a></label>
                </div>
                <span class="text-danger d-flex justify-content-center" v-if="showError && !acceptedPolicy"><em><small>{{ $t('policies.requiredPrivacyPolicy') }}</small></em></span>
              </div>
              <div class="row">      
                <div class="col-md-12 d-flex justify-content-center mt-3">
                  <one-click-btn v-model:active="activeBtn" :text="$t('contact.sendMessage')" class="bg-gradient-primary mx-2" @click="trySubmitMessage()"></one-click-btn>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="container" v-else-if="page == 'success'">
      <div class="row">
        <div class="col-lg-7 d-flex justify-content-center flex-column">
          <div class="card shadow-lg d-flex justify-content-center p-4 my-sm-0 my-sm-6 mt-8 mb-5">
            <div class="text-center">
              <h3>{{ $t('contactSuccess.title') }}</h3>
              <p class="mb-0">{{ $t('contactSuccess.subTitle') }}</p>
            </div>
            <div class="card-body pb-2">
              <div class="row">
                <div class="col-md-12 text-center mt-3">
                  <button type="button" class="btn bg-gradient-primary" @click="$router.push('/')">{{ $t('contactSuccess.backToHome') }}</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, watch, reactive } from 'vue';
import { validEmail, required } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import { useServerError, useUserAuthentication, useInputError } from "../store";
import OneClickBtn from '@/components/OneClickBtn.vue';

export default {
  name: 'ContactForm',
  components: { OneClickBtn },
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { isLoggedIn, name, email } = useUserAuthentication();
    const { inputErrors, setInputError, hasInputError } = useInputError();
    const { tm } = useI18n();
    const contact = reactive({
      name: null,
      email: null,
      message: null
    });
    const page = ref('contact');
    const activeBtn = ref(true);
    const showError = ref(false);
    const acceptedPolicy = ref(false);
    
    contact.name = name.value;
    contact.email = email.value;
    
    setInputError('name', required(contact.name, tm('errors.requiredName')));
    setInputError('email', validEmail(contact.email, tm('errors.requiredEmail')));
    setInputError('message', required(contact.message, tm('errors.requiredMessage')));

    watch(contact, () => {
      setInputError('name', required(contact.name, tm('errors.requiredName')));
      setInputError('email', validEmail(contact.email, tm('errors.requiredEmail')));
      setInputError('message', required(contact.message, tm('errors.requiredMessage')));
    })

    async function trySubmitMessage() {
      if(hasInputError() || (!isLoggedIn.value && !acceptedPolicy.value)) {
        activeBtn.value = true;
        showError.value = true;
      } else {
        await submitMessage();
      }
    }
    
    async function submitMessage() {
      let response = await fetch('/api/contact', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': contact.name, 'email': contact.email, 'text': contact.message})
      });
      if(response.status == 200) {
        resetServerError();
        page.value = 'success';
      } else {
        setServerError(response.statusText);
        activeBtn.value = true;
      }
    }

    function toPrivacyPolicy() {
      window.open('/privacy-policy', '_blank');
    }

    return { contact, inputErrors, showError, acceptedPolicy, page, isLoggedIn, activeBtn, trySubmitMessage, toPrivacyPolicy };
    }
}
</script>

<style scoped>
  .page-header {
    background-image: none!important;
  }
  @media (min-width: 768px) {
    .page-header {
      min-height: 85vh;
    }
  }
  @media (max-width: 768px) {
    .page-header {
      min-height: unset;
    }
  }  
</style>