<template>
  <section class="bg-dark position-relative mb-7" id="stackoverflow">
    <div class="container py-5">
      <div class="row">
        <div class="col-12 d-flex flex-column text-center">
          <h3 class="text-white">{{ $t('stackoverflow.title') }}</h3>
          <p class="lead" v-html="$t('stackoverflow.subTitle')"></p>
        </div>
      </div>
      <div class="row">
        <div class="card shadow-lg d-flex justify-content-center p-4 mt-3" v-if="page === 'stackoverflow'">
          <div class="row">
            <div class="col-md-6 col-12 pe-md-1">
              <label>{{ $t('stackoverflow.name') }}</label>
              <input id="name-input" :class="{'is-invalid': (showError && !!inputErrors.name)}" class="form-control" :placeholder="$t('stackoverflow.name')" type="text" v-model="stackoverflow.name" :disabled="isLoggedIn">
              <span class="text-danger" v-if="showError && inputErrors.name"><em><small>{{ $t(`${inputErrors.name}`) }}</small></em></span>
            </div>
            <div class="col-md-6 col-12 ps-md-1">
              <label>{{ $t('stackoverflow.email') }}</label>
              <input type="email" id="email-input" :class="{'is-invalid': (showError && !!inputErrors.email)}" class="form-control" :placeholder="$t('stackoverflow.emailPlaceholder')" v-model="stackoverflow.email" :disabled="isLoggedIn">
              <span class="text-danger" v-if="showError && inputErrors.email"><em><small>{{ $t(`${inputErrors.email}`) }}</small></em></span>
            </div>
          </div>
          <div class="row">
            <div class="col-12">
              <label>{{ $t('stackoverflow.url') }}</label>
              <input id="url-input" :class="{'is-invalid': (showError && !!inputErrors.name)}" class="form-control" :placeholder="$t('stackoverflow.urlPlaceholder')" type="text" v-model="stackoverflow.url">
              <span class="text-danger" v-if="showError && inputErrors.url"><em><small>{{ $t(`${inputErrors.url}`) }}</small></em></span>
            </div>
          </div>
          <div class="form-group mb-0 mt-md-0 mt-4">
            <label>{{ $t('stackoverflow.howCanWeHelp') }}</label>
            <textarea name="message" :class="{'is-invalid': (showError && !!inputErrors.message)}" class="form-control border-radius-lg" id="message-input" rows="6" :placeholder="$t('stackoverflow.problemDes')" v-model="stackoverflow.message"></textarea>
            <span class="text-danger" v-if="showError && inputErrors.message"><em><small>{{ $t(`${inputErrors.message}`) }}</small></em></span>
          </div>              
          <div v-if="!isLoggedIn" class="align-items-center justify-content-center">
            <div class="d-flex align-items-center justify-content-center mt-3">
              <div class="form-check form-switch">
                <input class="form-check-input" type="checkbox" id="customCheck" v-model="acceptedPolicy">
                <label class="form-check-label" for="customCheck">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toPrivacyPolicy">{{ $t('policies.privacyPolicy') }}</a></label>
              </div>
            </div>
            <span class="text-danger d-flex justify-content-center" v-if="showError && !acceptedPolicy"><em><small>{{ $t('policies.requiredPrivacyPolicy') }}</small></em></span>
          </div>
          <div class="row">      
            <div class="col-md-12 d-flex justify-content-center mt-3">
              <one-click-btn v-model:active="activeBtn" :text="$t('stackoverflow.sendRequest')" class="bg-gradient-primary mx-2" @click="trySubmit()"></one-click-btn>
            </div>
          </div>
        </div>
        <div class="card shadow-lg d-flex justify-content-center text-center p-4 mt-3" v-if="page === 'success'">
          <div class="info mb-4">
            <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
              <i :class="`ni ni-check-bold opacity-10 mt-2`"></i>
            </div>
          </div>
          <h2>{{ $t('stackoverflowSuccess.title') }}</h2>
          <p class="mb-4"><span v-html="$t('stackoverflowSuccess.subTitle')"></span></p>
          <div class="text-center">
            <button type="button" class="btn bg-gradient-primary my-4 mx-1" @click="newRequest">{{ $t('stackoverflowSuccess.newRequest') }}</button>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { ref, watch, reactive } from 'vue';
import { validEmail, required, validUrl } from '../../utils/Validate';
import { useServerError, useUserAuthentication, useInputError, useMessages } from "../../store";
import OneClickBtn from '@/components/OneClickBtn.vue';

export default {
  name: 'StackoverflowComponent',
  components: { OneClickBtn },
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { isLoggedIn, name, email } = useUserAuthentication();
    const { inputErrors, setInputError, hasInputError } = useInputError();
    const { tm } = useMessages();
    const stackoverflow = reactive({
      name: null,
      email: null,
      url: null,
      message: null
    });
    const page = ref('stackoverflow');
    const activeBtn = ref(true);
    const showError = ref(false);
    const acceptedPolicy = ref(false);
    
    stackoverflow.name = name.value;
    stackoverflow.email = email.value;
    
    setInputError('name', required(stackoverflow.name, tm('errors.requiredName')));
    setInputError('email', validEmail(stackoverflow.email, tm('errors.requiredEmail')));
    setInputError('url', validUrl(stackoverflow.url));
    setInputError('message', required(stackoverflow.message, tm('errors.requiredMessage')));

    watch(stackoverflow, () => {
      setInputError('name', required(stackoverflow.name, tm('errors.requiredName')));
      setInputError('email', validEmail(stackoverflow.email, tm('errors.requiredEmail')));
      setInputError('url', validUrl(stackoverflow.url));
      setInputError('message', required(stackoverflow.message, tm('errors.requiredMessage')));
    })

    async function trySubmit() {
      if(hasInputError() || (!isLoggedIn.value && !acceptedPolicy.value)) {
        activeBtn.value = true;
        showError.value = true;
      } else {
        await submit();
      }
    }
    
    async function submit() {
      window.rdt('track', 'Lead');
      let response = await fetch('/api/stackoverflow', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': stackoverflow.name, 'email': stackoverflow.email, 'url': stackoverflow.url, 'text': stackoverflow.message})
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
    
    function newRequest() {
      page.value = 'stackoverflow';
      stackoverflow.url = null;
      stackoverflow.message = null;
      activeBtn.value = true;
    }

    return { stackoverflow, inputErrors, showError, acceptedPolicy, page, isLoggedIn, activeBtn, trySubmit, toPrivacyPolicy, newRequest };
  }
}
</script>