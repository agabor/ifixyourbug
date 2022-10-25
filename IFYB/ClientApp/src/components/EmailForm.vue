<template>
  <carousel-item :icon="true" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :progress="progress">
    <template v-slot:icon>
      <i :class="`ni ni-email-83 opacity-10 mt-2`"></i>
    </template>
    <template v-slot:content>
      <div class="row mb-4">
        <input id="emailInput" class="form-control" :class="{'is-invalid': (inputErrors.email && showErrors)}" ref="emailInput" :placeholder="$t('order.emailExample')" type="email"
          @keyup.enter="trySubmitEmail()" :value="email" @input="email = $event.target.value.toLowerCase()" :disabled="progress !== 0">
        <span class="text-danger" v-if="((inputErrors.email || authenticationError) && showErrors)"><em><small>{{ $t(inputErrors.email ? inputErrors.email : authenticationError) }}</small></em></span>
        <div v-if="firstLogin">
          <div class="d-flex align-items-center justify-content-center mt-3">
            <div class="form-check form-switch">
              <input class="form-check-input" type="checkbox" id="customCheck" v-model="acceptedPolicy">
              <label class="form-check-label" for="customCheck">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toPrivacyPolicy">{{ $t('policies.privacyPolicy') }}</a></label>
            </div>
          </div>
          <span class="text-danger" v-if="showRequired"><em><small>{{ $t('policies.requiredPrivacyPolicy') }}</small></em></span>
        </div>
      </div>
      <div class="d-flex justify-content-center">
        <one-click-btn :active="progress === 0" :text="$t('order.submit')" class="bg-gradient-primary mx-2" @click="trySubmitEmail()" :disabled="email === ''"></one-click-btn>
      </div>
    </template>
  </carousel-item>
</template>

<script>
import { ref, onMounted, watch } from 'vue';
import { validEmail } from '../utils/Validate';
import CarouselItem from '../components/CarouselItem.vue';
import OneClickBtn from '../components/OneClickBtn.vue';
import { useInputError } from "../store";
import { authenticator } from '@/store/authentication';

export default {
  name: 'EmailForm',
  components: { CarouselItem, OneClickBtn },
  props: {
    isClient: Boolean
  },
  setup(props) {
    const auth = authenticator(props.isClient);
    const { inputErrors, setInputError } = useInputError();
    const email = ref('');
    const acceptedPolicy = ref(false);
    const showRequired = ref(false);
    const showErrors = ref(false);
    const emailInput = ref(null);
    auth.progress.value = 0;

    onMounted(() => {
      emailInput.value.focus();
    })
    
    watch(email, () => {
      if(props.sClient) {
        auth.firstLogin.value = false;
      }
      showRequired.value = false;
      acceptedPolicy.value = false;
      setInputError('email', validEmail(email.value));
      auth.authenticationError.value = null;
    })

    watch(acceptedPolicy, () => {
      showRequired.value = false;
    })

    async function trySubmitEmail() {
      let err = validEmail(email.value);
      showErrors.value = true
      if(err) {
        setInputError('email', err);
        emailInput.value.focus();
      } else if(props.isClient) {
        if(auth.firstLogin.value && !acceptedPolicy.value) {
          showRequired.value = true;
        }
        setInputError('email', null);
        await auth.authenticate(email.value, acceptedPolicy.value);
      } else if(!props.isClient) {
        setInputError('email', null);
        await auth.authenticate(email.value);
      }
    }

    function toPrivacyPolicy() {
      window.open('/privacy-policy', '_blank');
    }

    return { inputErrors, 'authenticationError': auth.authenticationError, showErrors, 'progress' : auth.progress, 'firstLogin': auth.firstLogin, acceptedPolicy, showRequired, email, emailInput, trySubmitEmail, toPrivacyPolicy }
  }
}
</script>