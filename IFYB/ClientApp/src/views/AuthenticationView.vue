<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header mt-5">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner">
          <authentication :page="page" :error="error" :progress="progress" :showPolicy="showPolicy" v-model:acceptedPolicy="acceptedPolicy" :showRequired="showRequired" v-model:activeBtn="activeBtn" @submitEmail="submitEmail" @changePolicy="changePolicy" @authentication="authentication" @setName="setUserName" @cancel="cancelLogin"></authentication>
        </div>
      </div>
      <footer-component></footer-component>
    </div>
  </section>
</template>

<script>
import { ref, watch } from 'vue';
import Authentication from '../components/Authentication.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import { useUserAuthentication, useMessages } from "../store";
import { setServerError, resetServerError } from "../store/serverError";
import { useAdminAuthentication } from "../store/admin";
import router from '../router';

export default {
  name: 'AuthenticationView',
  components: { Authentication, FooterComponent },
  setup() {
    const { requestedPage, jwt, name, email, setUserData, resetUserData, setName } = useUserAuthentication();
    const adminAuth = useAdminAuthentication();
    const { tm } = useMessages();
    const page = ref('email');
    const error = ref(null);
    const progress = ref(0);
    const showPolicy = ref(false);
    const acceptedPolicy = ref(false);
    const showRequired = ref(false);
    const activeBtn = ref(true);
    let clientId;

    adminAuth.logout();

    if(jwt.value) {
      toNamePageOrToTargetPage();
    }

    watch(jwt, () => {
      if(!jwt.value) {
        page.value = 'email';
      }
    })

    watch(name, () => {
      toNamePageOrToTargetPage();
    });

    async function submitEmail(e) {
      progress.value = 30;
      let response = await fetch('/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': e, 'privacyPolicyAccepted': acceptedPolicy.value})
      });
      progress.value = 100;
      email.value = e;
      if(response.status === 200) {
        resetServerError();
        clientId = (await response.json()).id;
        setTimeout(() => {
          page.value = 'auth';
          progress.value = 100;
        }, "500");
      } else if(response.status === 401) {
        if(showPolicy.value) {
          showRequired.value = true;
        }
        showPolicy.value = true;
        progress.value = 0;
        activeBtn.value = true;
      } else {
        setServerError(response.statusText);
        email.value = null;
        progress.value = null;
        activeBtn.value = true;
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
        body: JSON.stringify({ 'password': code})
      });
      if(response.status === 200) {
        resetServerError();
        error.value = null;
        await setUserData(response);
        toNamePageOrToTargetPage();
      } else if(response.status === 401) {
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


    async function setUserName(n) {
      await setName(n);
      if(name.value) {
        router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
      }
      activeBtn.value = true;
    }

    function toNamePageOrToTargetPage() {
      if(name.value) {
        router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
      } else {
        page.value = 'name';
      }
    }

    async function cancelLogin() {
      activeBtn.value = true;
      progress.value = 0;
      acceptedPolicy.value = false;
      showPolicy.value = false;
      error.value = null;
      resetUserData();
      page.value = 'email';
    }

    return { page, error, progress, showPolicy, acceptedPolicy, showRequired, activeBtn, submitEmail, changePolicy, authentication, setUserName, cancelLogin }
  }
}
</script>