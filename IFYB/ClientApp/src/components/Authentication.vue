<template>
  <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error" @onClickBtn="trySubmitEmail()">
    <div class="row mb-4">
      <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="user.email">
    </div>
  </carousel-item>
  <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
  <carousel-item :class="{'active': page === 'name'}" icon="badge" :title="$t('order.name')" :subTitle="$t('order.nameDes')" :buttonText="$t('order.save')" :error="error" @onClickBtn="trySetName()">
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
        let authResponse = await fetch('/api/authenticate/check-jwt', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(authResponse.status == 200) {
          context.emit('update:jwt', localStorage.getItem('jwt'));
          toNamePageOrToTargetPage();
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
      const response = await fetch('/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': user.value.email})
      });
      clientId = (await response.json()).id;
      page.value = 'auth';
      error.value = null;
    }

    async function checkAuthentication(code) {
      auth.value = code;
      try {
        await tryAuthentication();
      } catch(e) {
        handleAuthenticationError();
      }
      if(props.jwt) {
        toNamePageOrToTargetPage();
      }
    }

    async function tryAuthentication() {
      const response = await fetch(`/api/authenticate/${clientId}`, {
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

    function trySetName() {
      let err = required(user.value.name, tm('errors.requiredName'), 'name-input');
      if(err) {
        error.value = err;
      } else {
        setName();
      }
    }

    async function setName() {
      await fetch('/api/clients/name', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${props.jwt}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': user.value.name})
      });
      page.value = '';
      context.emit('toTargetPage');
      error.value = null;
    }

    async function toNamePageOrToTargetPage() {
      if(props.checkName) {
        let nameResponse = await fetch('/api/clients/name', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(nameResponse.status == 404) {
          page.value = 'name';
        } else {
          page.value = '';
          context.emit('setGitAccesses');
          context.emit('toTargetPage');
        }
      } else {
        page.value = '';
        context.emit('toTargetPage');
      }
    }

    return { page, error, user, auth, trySubmitEmail, checkAuthentication, trySetName }
  }
}
</script>