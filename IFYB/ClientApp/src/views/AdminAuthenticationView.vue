<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication :page="page" :error="error" :progress="progress" v-model:activeBtn="activeBtn" @submitEmail="submitEmail" @authentication="authentication" @cancel="cancelLogin"></authentication>
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
    const { setServerError, resetServerError } = useServerError();
    const { requestedPage, setJwt } = useAdminAuthentication();
    const { tm } = useI18n();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    const progress = ref(0);
    const activeBtn = ref(true);
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
        resetServerError();
        adminId = (await response.json()).id;
        localStorage.setItem('adminId', adminId);
        error.value = null;
        setTimeout(() => {
          page.value = 'auth';
          progress.value = 100;
          activeBtn.value = true;
        }, "500");
      } else if(response.status == 403) {
				error.value = tm('errors.notAdministratorEmail');
        progress.value = null;
        activeBtn.value = true;
			}  else {
        setServerError(response.statusText);
        progress.value = null;
        activeBtn.value = true;
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
        resetServerError();
        error.value = null;
        let jwt = (await response.json()).jwt;
        setJwt(jwt);
        router.push(requestedPage.value ? requestedPage.value.fullPath : '/admin');
      } else if(response.status == 401) {
        const passwordExpired = (await response.json()).passwordExpired;
        if (!passwordExpired) {
          resetServerError();
          error.value = tm('errors.wrongCode');
        } else {
          page.value = 'failed';
        }
      } else {
        setServerError(response.statusText);
      }
      activeBtn.value = true;
    }

    function cancelLogin() {
      activeBtn.value = true;
      progress.value = 0;
      error.value = null;
      user.value = {};
      setJwt(null);
      page.value = 'email';
    }
    
    return { page, error, user, progress, activeBtn, submitEmail, authentication, cancelLogin }
  }
}
</script>