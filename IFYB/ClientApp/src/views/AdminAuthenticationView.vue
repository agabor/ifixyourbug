<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication :page="page" :error="error" @submitEmail="submitEmail" @authentication="authentication" @setName="setName"></authentication>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useI18n } from "vue-i18n";
import Authentication from '../components/Authentication.vue';
import { useServerError, useAuthentication } from "../store";
import router from '../router'

export default {
  name: 'AdminAuthenticationView',
  components: { Authentication },
  setup() {
    const { setServerError } = useServerError();
    const { authenticationPage } = useAuthentication();
    const { tm } = useI18n();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    let clientId;
    
    setJwtIfActive();

    async function setJwtIfActive() {
      if(localStorage.getItem('jwt')) {
        let response = await fetch('/api/authenticate/admin/check-jwt', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(response.status == 200) {
          setServerError(null);
          toTargetPage();
        } else if(response.status != 403) {
					setServerError(response.statusText);
        }
      }
    }

    async function submitEmail(email) {
      let response = await fetch('/api/authenticate/admin', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': email})
      });
      if(response.status == 200) {
        setServerError(null);
        clientId = (await response.json()).id;
        page.value = 'auth';
        error.value = null;
      } else if(response.status == 403) {
				error.value = tm('errors.notAdministratorEmail');
			}  else {
        setServerError(response.statusText);
      }
    }

    async function authentication(code) {
      let response = await fetch(`/api/authenticate/admin/${clientId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'clientId': clientId, 'password': code})
      });
      if(response.status == 200) {
        setServerError(null);
        let jwt = (await response.json()).jwt;
        localStorage.setItem('jwt', jwt);
        toTargetPage();
      } else if(response.status == 403) {
        handleAuthenticationError();
      } else {
        setServerError(response.statusText);
      }
    }

    function handleAuthenticationError() {
      setServerError(null);
      error.value = tm('errors.wrongCode');
      console.log(error.value);
    }

    function toTargetPage() {
      if(authenticationPage.value) {
        router.push(authenticationPage.value);
      } else {
        router.push('/admin');
      }
    }

    return { page, error, user, submitEmail, authentication }
  }
}
</script>