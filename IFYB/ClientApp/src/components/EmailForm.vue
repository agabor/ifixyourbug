<template>
  <carousel-item :icon="true" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :progress="progress">
    <template v-slot:icon>
      <i :class="`ni ni-email-83 opacity-10 mt-2`"></i>
    </template>
    <template v-slot:content>
      <div class="row mb-4">
        <input id="emailInput" class="form-control" ref="emailInput" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="email" @input="email = $event.target.value.toLowerCase()" :disabled="progress !== 0">
        <div v-if="firstLogin">
          <div class="d-flex align-items-center justify-content-center mt-3">
            <div class="form-check form-switch">
              <input class="form-check-input" type="checkbox" id="customCheck" :value="acceptedPolicy" @input="changeAcceptedPolicy">
              <label class="form-check-label" for="customCheck">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toPrivacyPolicy">{{ $t('policies.privacyPolicy') }}</a></label>
            </div>
          </div>
          <span class="text-danger" v-if="showRequired"><em><small>{{ $t('policies.requiredPrivacyPolicy') }}</small></em></span>
        </div>
      </div>
      <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="validationError ? validationError : authenticationError">
        {{ validationError ? validationError : authenticationError }}
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
import { useMessages } from "../store";
import { authenticator } from '@/store/authentication';

export default {
  name: 'EmailForm',
  components: { CarouselItem, OneClickBtn },
  props: {
    isClient: Boolean
  },
  setup(props) {
    const auth = authenticator(props.isClient);
    const { tm } = useMessages();
    const email = ref('');
    const acceptedPolicy = ref(false);
    const validationError = ref(null);
    const showRequired = ref(false);
    const emailInput = ref(null);
    auth.progress.value = 0;

    onMounted(() => {
      emailInput.value.focus();
    })
    
    watch(email, () => {
      auth.firstLogin.value = false;
      showRequired.value = false;
    })

    function changeAcceptedPolicy() {
      acceptedPolicy.value = !acceptedPolicy.value;
      showRequired.value = false;
    }
    async function trySubmitEmail() {
      let err = validEmail(email.value);
      if(err) {
        validationError.value = tm(err);
        emailInput.value.focus();
        showRequired.value = false;
      } else if(props.isClient) {
        if(auth.firstLogin.value && !acceptedPolicy.value) {
          showRequired.value = true;
        }
        validationError.value = null;
        await auth.authenticate(email.value, acceptedPolicy.value);
      } else if(!props.isClient) {
        validationError.value = null;
        await auth.authenticate(email.value);
      }
    }

    function toPrivacyPolicy() {
      window.open('/privacy-policy', '_blank');
    }

    return { changeAcceptedPolicy, 'progress' : auth.progress, 'firstLogin': auth.firstLogin ? auth.firstLogin : false, 'authenticationError': auth.authenticationError, acceptedPolicy, showRequired, validationError, email, emailInput, trySubmitEmail, toPrivacyPolicy }
  }
}
</script>