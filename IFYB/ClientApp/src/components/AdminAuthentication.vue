<template>
  <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error" @onClickBtn="trySubmitEmail()">
    <div class="row mb-4">
      <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="user.email">
    </div>
  </carousel-item>
  <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
</template>

<script>
import { ref } from 'vue';
import { validEmail } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import TwoFa from '../components/2FA.vue';
import CarouselItem from '../components/CarouselItem.vue';

export default {
  name: 'AdminAuthenticationForm',
  components: { TwoFa, CarouselItem },
  emits: [ 'update:jwt', 'toTargetPage', 'setGitAccesses' ],
  props: {
    jwt: String,
    checkName: Boolean
  },
  setup(props, context) {
    const { tm } = useI18n();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    const auth = ref('');
    let clientId;

    setJwtIfActive();

    async function setJwtIfActive() {
      if(localStorage.getItem('jwt')) {
        let authResponse = await fetch('/api/authenticate/admin/check-jwt', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(authResponse.status == 200) {
          context.emit('update:jwt', localStorage.getItem('jwt'));
          toTargetPage();
        }
      }
    }
    
    function trySubmitEmail() {
      let err = validEmail(user.value.email);
      if(err) {
        error.value = tm(err);
      } else {
        submitEmail();
      }
    }

    async function submitEmail() {
      const response = await fetch('/api/authenticate/admin', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': user.value.email})
      });
      if(response.status == 200){
        clientId = (await response.json()).id;
        page.value = 'auth';
        error.value = null;
      } else {
        error.value = tm('errors.notAdministratorEmail')
      }
    }

    async function checkAuthentication(code) {
      auth.value = code;
      try {
        await tryAuthentication();
      } catch(e) {
        handleAuthenticationError();
      }
      if(props.jwt) {
        toTargetPage();
      }
    }

    async function tryAuthentication() {
      const response = await fetch(`/api/authenticate/admin/${clientId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'clientId': clientId, 'password': auth.value})
      });
      let jwt = (await response.json()).jwt;
      context.emit('update:jwt', jwt);
      localStorage.setItem('jwt', jwt);
      error.value = null;
    }

    function handleAuthenticationError() {
      context.emit('update:jwt', null);
      error.value = tm('errors.wrongCode');
    }

    async function toTargetPage() {
      page.value = '';
      context.emit('toTargetPage');
    }

    return { page, error, user, auth, trySubmitEmail, checkAuthentication }
  }
}
</script>