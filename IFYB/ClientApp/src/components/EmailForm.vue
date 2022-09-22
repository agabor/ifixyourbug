<template>
  <carousel-item :icon="true" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :progress="progress">
    <template v-slot:icon>
      <i :class="`ni ni-email-83 opacity-10 mt-2`"></i>
    </template>
    <template v-slot:content>
      <div class="row mb-4">
        <input id="emailInput" class="form-control" ref="userEmailInput" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="email" @input="email = $event.target.value.toLowerCase()">
        <div v-if="showPolicy">
          <div class="form-check form-switch d-flex align-items-center justify-content-center mt-3">
            <input type="checkbox" class="form-check-input m-0" id="customCheck" :value="acceptedPolicy" @input="$emit('update:acceptedPolicy', !acceptedPolicy)">
            <label class="custom-control-label m-0 mx-2" for="customCheck">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toPrivacyPolicy">{{ $t('policies.privacyPolicy') }}</a></label>
          </div>
          <span class="text-danger" v-if="showRequired"><em><small>{{ $t('policies.requiredPrivacyPolicy') }}</small></em></span>
        </div>
      </div>
      <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error ? error : validationError">
        {{ error ? error: validationError }}
      </div>
      <div class="d-flex justify-content-center">
        <one-click-btn v-model:active="activeBtn" :text="$t('order.submit')" class="bg-gradient-primary mx-2" @click="trySubmitEmail()"></one-click-btn>
      </div>
    </template>
  </carousel-item>
</template>

<script>
import { ref, watch, onMounted } from 'vue';
import { validEmail } from '../utils/Validate';
import CarouselItem from '../components/CarouselItem.vue';
import OneClickBtn from '../components/OneClickBtn.vue';
import { useMessages } from "../store";

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
    const { tm } = useMessages();
    const email = ref(props.modelValue ?? '');
    const validationError = ref(null);
    const activeBtn = ref(props.activeButton ?? true);
    const userEmailInput = ref(null);

    onMounted(() => {
      userEmailInput.value.focus();
    })

    watch(props, () => {
      activeBtn.value = props.activeButton;
      email.value = props.modelValue;
    })

    function trySubmitEmail() {
      let err = validEmail(email.value);
      if(err) {
        validationError.value = tm(err);
        activeBtn.value = true;
        userEmailInput.value.focus();
      } else {
        validationError.value = null;
        context.emit('update:activeButton', activeBtn.value);
        context.emit('update:modelValue', email.value);
      }
    }

    function toPrivacyPolicy() {
      window.open('/privacy-policy', '_blank');
    }

    return { validationError, email, activeBtn, userEmailInput, trySubmitEmail, toPrivacyPolicy }
  }
}
</script>