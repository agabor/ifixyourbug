<template>
  <div class="page-header min-vh-85">
    <div>
      <img class="position-absolute fixed-top ms-auto w-70 h-100 z-index-0 d-block border-radius-lg border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0 fit-cover" src="../assets/img/bg2.webp" alt="image">
    </div>
    <div class="container" v-if="page == 'contact'">
      <div class="row">
        <div class="col-lg-7 d-flex justify-content-center flex-column">
          <div class="card shadow-lg d-flex justify-content-center p-4 my-sm-0 my-sm-6 mt-8 mb-5">
            <div class="text-center">
              <h3>{{ $t('contact.title') }}</h3>
              <p class="mb-0">{{ $t('contact.subTitle') }}</p>
            </div>
            <div class="card-body pb-2">
              <div class="row">
                <div class="col-md-6">
                  <label>{{ $t('contact.fullName') }}</label>
                  <div class="input-group mb-4">
                    <input id="name-input" class="form-control" :placeholder="$t('contact.fullName')" type="text" v-model="contact.name" autofocus :disabled="isLoggedIn">
                  </div>
                </div>
                <div class="col-md-6 ps-md-2">
                  <label>{{ $t('contact.email') }}</label>
                  <div class="input-group">
                    <input type="email" id="email-input" class="form-control" :placeholder="$t('contact.emailPlaceholder')" v-model="contact.email" :disabled="isLoggedIn">
                  </div>
                </div>
              </div>
              <div class="form-group mb-0 mt-md-0 mt-4">
                <label>{{ $t('contact.howCanWeHelp') }}</label>
                <textarea name="message" class="form-control border-radius-lg" id="message-input" rows="6" :placeholder="$t('contact.problemDes')" v-model="contact.message"></textarea>
              </div>
              <div class="row">
                <div class="alert alert-warning text-white font-weight-bold mt-3 mb-0" role="alert" v-if="error">
                  {{error}}
                </div>                  
                <div class="col-md-12 text-center mt-3">
                  <button type="submit" class="btn bg-gradient-primary" @click="trySubmitMessage">{{ $t('contact.sendMessage') }}</button>
                </div>
                <span class="text-danger text-center" v-if="!isLoggedIn"><em><small>{{ $t('contact.createAccount') }}</small></em></span>
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
import { ref } from 'vue';
import { validEmail, required } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import { useServerError, useUserAuthentication } from "../store";

export default {
  name: 'ContactForm',
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { get, isLoggedIn } = useUserAuthentication();
    const { tm } = useI18n();
    const contact = ref({
      name: '',
      email: ''
    });
    const error = ref(null);
    const page = ref('contact');

    setClientContact();

    async function setClientContact() {
      let response = await get('/api/clients/client');
      if(response.status == 200) {
        resetServerError();
        let client = await response.json();
        contact.value.name = client.name;
        contact.value.email = client.email;
      } else if(response.status != 401) {
        setServerError(response.statusText);
      }
    }

    function trySubmitMessage() {
      let err =  getFormError();
      if(err) {
        error.value = err;
      } else {
        submitMessage();
      }
    }

    async function submitMessage() {
      let response = await fetch('/api/contact', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': contact.value.name, 'email': contact.value.email, 'text': contact.value.message})
      });
      if(response.status == 200) {
        resetServerError();
        page.value = 'success';
        error.value = null;
      } else {
        setServerError(response.statusText);
      }
    }

    function getFormError() {
      let err =
        required(contact.value.name, tm('errors.requiredName'), 'name-input') ||
        required(contact.value.email, tm('errors.requiredEmail'), 'email-input');
      if(!err && validEmail(contact.value.email)) {
        err = tm(validEmail(contact.value.email));
      }
      if(!err) {
        err = required(contact.value.message, tm('errors.requiredMessage'), 'message-input');
      }
      return err;
    }

    return { contact, error, page, isLoggedIn, trySubmitMessage }
  }
}
</script>
