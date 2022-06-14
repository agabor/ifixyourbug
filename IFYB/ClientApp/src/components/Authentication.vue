<template>
  <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error" @onClickBtn="trySubmitEmail()">
    <div class="row mb-4">
      <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="user.email">
    </div>
  </carousel-item>
  <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="tryAuthentication"></two-fa>
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
import { useServerError } from "../store";

export default {
  name: 'AuthenticationForm',
  components: { TwoFa, CarouselItem },
  emits: [ 'update:jwt', 'toTargetPage', 'setGitAccesses' ],
  props: {
    jwt: String,
    checkName: Boolean
  },
  setup(props, context) {
    const { setServerError } = useServerError();
    const { tm } = useI18n();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    const auth = ref('');
    let clientId;
    
    setJwtIfActive();

    async function setJwtIfActive() {
      if(localStorage.getItem('jwt')) {
        let response = await fetch('/api/authenticate/check-jwt', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(response.status == 200) {
          setServerError(null);
          context.emit('update:jwt', localStorage.getItem('jwt'));
          toNamePageOrToTargetPage();
        } else{
          setServerError(response.statusText);
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
      let response = await fetch('/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': user.value.email})
      });
      if(response.status == 200) {
        setServerError(null);
        clientId = (await response.json()).id;
        page.value = 'auth';
        error.value = null;
      } else{
        setServerError(response.statusText);
      }
    }

    async function tryAuthentication(code) {
      auth.value = code;
      let response = await fetch(`/api/authenticate/${clientId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'clientId': clientId, 'password': auth.value})
      });
      if(response.status == 200) {
        setServerError(null);
        let jwt = (await response.json()).jwt;
        context.emit('update:jwt', jwt);
        localStorage.setItem('jwt', jwt);
        error.value = null;
        toNamePageOrToTargetPage();
      } else if(response.status == 403) {
        handleAuthenticationError();
      } else {
        setServerError(response.statusText);
      }
    }

    function handleAuthenticationError() {
      setServerError(null);
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
      let response = await fetch('/api/clients/name', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${props.jwt}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': user.value.name})
      });
      if(response.status == 200) {
        setServerError(null);
        page.value = '';
        context.emit('toTargetPage');
        error.value = null;
      } else{
        setServerError(response.statusText);
      }
    }

    async function toNamePageOrToTargetPage() {
      if(props.checkName) {
        let response = await fetch('/api/clients/name', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(response.status == 404) {
          page.value = 'name';
        } else if(response.status == 200) {
          setServerError(null);
          page.value = '';
          context.emit('setGitAccesses');
          context.emit('toTargetPage');
        } else{
          page.value = 'name';
          setServerError(response.statusText);
        }
      } else {
        page.value = '';
        context.emit('toTargetPage');
      }
    }

    return { page, error, user, auth, trySubmitEmail, tryAuthentication, trySetName }
  }
}
</script>