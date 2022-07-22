<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication :page="page" :error="error" :progress="progress" :showPolicy="showPolicy" :acceptedPolicy="acceptedPolicy" :showRequired="showRequired" @submitEmail="submitEmail" @changePolicy="changePolicy" @authentication="authentication" @setName="setUserName"></authentication>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useI18n } from "vue-i18n";
import Authentication from '../components/Authentication.vue';
import { useServerError, useUserAuthentication } from "../store";
import router from '../router';

export default {
  name: 'AuthenticationView',
  components: { Authentication },
  setup() {
    const { setServerError } = useServerError();
    const { requestedPage, jwt, name, setJwt, setName } = useUserAuthentication();
    const { tm } = useI18n();
    const page = ref('email');
    const error = ref(null);
    const progress = ref(0);
    const showPolicy = ref(false);
    const acceptedPolicy = ref(false);
    const showRequired = ref(false);
    let clientId;

    setServerError(null);
    if(jwt.value) {
      toNamePageOrToTargetPage();
    }

    async function submitEmail(email) {
      progress.value = 30;
      let response = await fetch('/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': email, 'privacyPolicyAccepted': acceptedPolicy.value})
      });
      progress.value = 100;
      if(response.status == 200) {
        setServerError(null);
        clientId = (await response.json()).id;
        setTimeout(() => {
          page.value = 'auth';
          progress.value = 100;
        }, "500");
      } else if(response.status == 401) {
        if(showPolicy.value) {
          showRequired.value = true;
        }
        showPolicy.value = true;
        progress.value = 0;
      } else {
        setServerError(response.statusText);
        progress.value = null;
      }
    }

    function changePolicy(accepted) {
      acceptedPolicy.value = accepted;
      showRequired.value = false;
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

    async function setUserName(n) {
      await setName(n);
      if(name.value) {
        router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
      }
    }

    async function toNamePageOrToTargetPage() {
      await setName();
      if(name.value) {
        router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
      } else {
        page.value = 'name';
      }
    }

    return { page, error, progress, showPolicy, acceptedPolicy, showRequired, submitEmail, changePolicy, authentication, setUserName }
  }
}
</script>