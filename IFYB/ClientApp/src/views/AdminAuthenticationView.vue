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
import { useServerError, useAdminAuthentication } from "../store";
import router from '../router'

export default {
  name: 'AdminAuthenticationView',
  components: { Authentication },
  setup() {
    const { setServerError } = useServerError();
    const { requestedPage, setJwt } = useAdminAuthentication();
    const { tm } = useI18n();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    const progress = ref(0);
    let adminId;

    async function submitEmail(email) {
      progress.value = 30;
      let response = await fetch('/api/authenticate/admin', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': email})
      });
      progress.value = 100;
      if(response.status == 200) {
        setServerError(null);
        adminId = (await response.json()).id;
        localStorage.setItem('adminId', adminId);
        error.value = null;
        setTimeout(() => {
          page.value = 'auth';
          progress.value = 100;
        }, "500");
      } else if(response.status == 403) {
				error.value = tm('errors.notAdministratorEmail');
        progress.value = null;
			}  else {
        setServerError(response.statusText);
        progress.value = null;
      }
    }

    async function authentication(code) {
      let response = await fetch(`/api/authenticate/admin/${adminId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'adminId': adminId, 'password': code})
      });
      if(response.status == 200) {
        setServerError(null);
        error.value = null;
        let jwt = (await response.json()).jwt;
        setJwt(jwt);
        router.push(requestedPage.value);
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


    return { page, error, user, progress, submitEmail, authentication }
  }
}
</script>