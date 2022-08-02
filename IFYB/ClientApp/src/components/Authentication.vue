<template>
  <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error ? error: validationError" :progress="progress" @onClickBtn="trySubmitEmail()">
    <div class="row mb-4">
      <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="user.email" @input="user.email = $event.target.value.toLowerCase()">
      <div v-if="showPolicy">
      <div class="form-check d-flex align-items-center justify-content-center mt-3">
        <input type="checkbox" class="form-check-input m-0" id="customCheck" :value="acceptedPolicy" @input="$emit('changePolicy', !acceptedPolicy)">
        <label class="custom-control-label m-0 mx-2" for="customCheck">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toPrivacyPolicy">{{ $t('policies.privacyPolicy') }}</a></label>
      </div>
      <span class="text-danger" v-if="showRequired"><em><small>{{ $t('policies.requiredPrivacyPolicy') }}</small></em></span>
      </div>
    </div>
  </carousel-item>
  <two-fa :class="{'active': page === 'auth'}" :error="error ? error: validationError" v-model:modelValue="user.auth" @update:modelValue="tryAuthentication" @cancel="cancel"></two-fa>
  <carousel-item :class="{'active': page === 'name'}" icon="badge" :cancelable="true" :title="$t('order.name')" :subTitle="$t('order.nameDes')" :buttonText="$t('order.save')" :error="error ? error: validationError" @onClickBtn="trySetName()" @cancel="cancel">
    <div class="row mb-4">
      <input id="name-input" class="form-control" placeholder="Your Name" type="text" @keyup.enter="trySetName()" v-model="user.name">
    </div>
  </carousel-item>
</template>

<script>
import { ref } from 'vue';
import { validEmail, required } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import TwoFa from '../components/2FA.vue';
import CarouselItem from '../components/CarouselItem.vue';

export default {
  name: 'AuthenticationForm',
  components: { TwoFa, CarouselItem },
  props: {
    page: String,
    error: String,
    progress: Number,
    showPolicy: Boolean,
    acceptedPolicy: Boolean,
    showRequired: Boolean
  },
  emits: [ 'submitEmail', 'changePolicy', 'authentication', 'setName', 'cancel' ],
  setup(props, context) {
    const { tm } = useI18n();
    const user = ref({});
    const validationError = ref(null);

    function trySubmitEmail() {
      let err = validEmail(user.value.email);
      if(err) {
        validationError.value = tm(err);
      } else {
        validationError.value = null;
        context.emit('submitEmail', user.value.email);
      }
    }
    
    function toPrivacyPolicy() {
      window.open('/privacy-policy', '_blank');
    }

    async function tryAuthentication(code) {
      user.value.auth = code;
      context.emit('authentication', user.value.auth);
    }

    function trySetName() {
      let err = required(user.value.name, tm('errors.requiredName'), 'name-input');
      if(err) {
        validationError.value = err;
      } else {
        validationError.value = null;
        context.emit('setName', user.value.name);
      }
    }

    function cancel() {
      user.value = {};
      validationError.value = null;
      context.emit('cancel');
    }

    return { validationError, user, trySubmitEmail, toPrivacyPolicy, tryAuthentication, trySetName, cancel }
  }
}
</script>