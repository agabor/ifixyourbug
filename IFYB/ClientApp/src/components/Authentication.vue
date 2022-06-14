<template>
  <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error ? error: validationError" @onClickBtn="trySubmitEmail()">
    <div class="row mb-4">
      <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="user.email">
    </div>
  </carousel-item>
  <two-fa :class="{'active': page === 'auth'}" :error="error ? error: validationError" v-model:modelValue="user.auth" @update:modelValue="tryAuthentication"></two-fa>
  <carousel-item :class="{'active': page === 'name'}" icon="badge" :title="$t('order.name')" :subTitle="$t('order.nameDes')" :buttonText="$t('order.save')" :error="error ? error: validationError" @onClickBtn="trySetName()">
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
    error: String
  },
  emits: [ 'submitEmail', 'authentication', 'setName' ],
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

    return { validationError, user, trySubmitEmail, tryAuthentication, trySetName }
  }
}
</script>