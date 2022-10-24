<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication :page="page" :error="error" :progress="progress" v-model:activeBtn="activeBtn" @submitEmail="submitEmail" @authentication="authentication" @cancel="cancelLogin"></authentication>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import Authentication from '../components/Authentication.vue';
import { useMessages } from "../store";
import { useUserAuthentication } from "../store/authentication";
import { useAdminAuthentication } from "../store/admin";
import router from '../router';
import { fetchPost } from '@/store/web';

export default {
  name: 'AdminAuthenticationView',
  components: { Authentication },
  setup() {
    const { requestedPage, setJwt } = useAdminAuthentication();
    const userAuth = useUserAuthentication();
    const { tm } = useMessages();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    const progress = ref(0);
    const activeBtn = ref(true);
    let adminId;
    
    userAuth.logout();
    
    async function submitEmail(email) {
      progress.value = 30;
      let response = await fetchPost('/api/authenticate/admin', {'email': email})
      progress.value = 100;
      if(response.status === 200) {
        adminId = (await response.json()).id;
        localStorage.setItem('adminId', adminId);
        error.value = null;
        setTimeout(() => {
          page.value = 'auth';
          progress.value = 100;
          activeBtn.value = true;
        }, "500");
      } else if(response.status === 403) {
				error.value = tm('errors.notAdministratorEmail');
        progress.value = null;
        activeBtn.value = true;
			}  else {
        progress.value = null;
        activeBtn.value = true;
      }
    }

    async function authentication(code) {
      let response = await fetchPost(`/api/authenticate/admin/${adminId}`, {'adminId': adminId, 'password': code})
      if(response.status === 200) {
        error.value = null;
        let jwt = (await response.json()).jwt;
        setJwt(jwt);
        router.push(requestedPage.value ? requestedPage.value.fullPath : '/admin');
      } else if(response.status === 401) {
        const passwordExpired = (await response.json()).passwordExpired;
        if (!passwordExpired) {
          error.value = tm('errors.wrongCode');
        } else {
          page.value = 'failed';
        }
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