<template>
  <carousel-item icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :progress="progress">
    <div class="row mb-4">
      <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="email" @input="email = $event.target.value.toLowerCase()">
      <div v-if="showPolicy">
        <div class="form-check d-flex align-items-center justify-content-center mt-3">
          <input type="checkbox" class="form-check-input m-0" id="customCheck" :value="acceptedPolicy" @input="$emit('update:acceptedPolicy', !acceptedPolicy)">
          <label class="custom-control-label m-0 mx-2" for="customCheck">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toPrivacyPolicy">{{ $t('policies.privacyPolicy') }}</a></label>
        </div>
        <span class="text-danger" v-if="showRequired"><em><small>{{ $t('policies.requiredPrivacyPolicy') }}</small></em></span>
      </div>
    </div>
    <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error ? error: validationError">
      {{ error ? error: validationError }}
    </div>
    <div class="d-flex justify-content-center">
      <one-click-btn v-model:active="activeBtn" :text="$t('order.submit')" class="bg-gradient-primary mx-2" @click="trySubmitEmail()"></one-click-btn>
    </div>
  </carousel-item>
</template>

<script>
import { ref, watch } from 'vue';
import { validEmail } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import CarouselItem from '../components/CarouselItem.vue';
import OneClickBtn from '../components/OneClickBtn.vue';
import { event } from 'vue-gtag';

export default {
  name: 'EmailForm',
  components: { CarouselItem, OneClickBtn },
  props: {
    modelValue: String,
    error: String,
    progress: Number,
    showPolicy: Boolean,
    acceptedPolicy: Boolean,
    showRequired: Boolean,
    activeButton: Boolean
  },
  emits: [ 'update:modelValue', 'update:acceptedPolicy', 'update:activeButton' ],
  setup(props, context) {
    const { tm } = useI18n();
    const email = ref(props.modelValue ?? '');
    const validationError = ref(null);
    const activeBtn = ref(props.activeButton ?? true);

    watch(props, () => {
      activeBtn.value = props.activeButton;
      email.value = props.modelValue;
    })

    function trySubmitEmail() {
      event('try-set-email');
      let err = validEmail(email.value);
      if(err) {
        validationError.value = tm(err);
        activeBtn.value = true;
      } else {
        validationError.value = null;
        context.emit('update:activeButton', activeBtn.value);
        context.emit('update:modelValue', email.value);
      }
    }

    function toPrivacyPolicy() {
      event('navigate-to-privacy-policy');
      window.open('/privacy-policy', '_blank');
    }

    return { validationError, email, activeBtn, trySubmitEmail, toPrivacyPolicy }
  }
}
</script>