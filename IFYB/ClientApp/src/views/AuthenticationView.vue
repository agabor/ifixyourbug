<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication :page="page" :error="error" :progress="progress" @submitEmail="submitEmail" @authentication="authentication" @setName="setName"></authentication>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useI18n } from "vue-i18n";
import Authentication from '../components/Authentication.vue';
import { useServerError, useUserAuthentication } from "../store";
import router from '../router'

export default {
  name: 'AuthenticationView',
  components: { Authentication },
  setup() {
    const { setServerError } = useServerError();
    const { requestedPage, setJwt, get, post } = useUserAuthentication();
    const { tm } = useI18n();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    const progress = ref(0);
    let clientId;

    setServerError(null);
    
    async function submitEmail(email) {
      progress.value = 30;
      let response = await fetch('/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': email})
      });
      progress.value = 100;
      if(response.status == 200) {
        setServerError(null);
        clientId = (await response.json()).id;
        setTimeout(() => {
          page.value = 'auth';
          progress.value = 100;
        }, "500");
      } else {
        setServerError(response.statusText);
        progress.value = null;
      }
    }

    async function authentication(code) {
      let response = await fetch(`/api/authenticate/${clientId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'clientId': clientId, 'password': code})
      });
      if(response.status == 200) {
        setServerError(null);
        error.value = null;
        let jwt = (await response.json()).jwt;
        setJwt(jwt);
        toNamePageOrToTargetPage();
      } else if(response.status == 403) {
        handleAuthenticationError();
      } else {
        setServerError(response.statusText);
      }
    }

    function handleAuthenticationError() {
      setServerError(null);
      error.value = tm('errors.wrongCode');
    }

    async function setName(name) {
      let response = await post('/api/clients/name', {'name': name});
      if(response.status == 200) {
        setServerError(null);
        page.value = '';
        router.push(requestedPage.value ? requestedPage.value : '/my-orders');
      } else{
        setServerError(response.statusText);
      }
    }

    async function toNamePageOrToTargetPage() {
      let response = await get('/api/clients/name')
      if(response.status == 404) {
        page.value = 'name';
      } else if(response.status == 200) {
        setServerError(null);
        page.value = '';
        router.push(requestedPage.value ? requestedPage.value : '/my-orders');
      } else{
        page.value = 'name';
        setServerError(response.statusText);
      }
    }


    return { page, error, user, progress, submitEmail, authentication, setName }
  }
}
</script>