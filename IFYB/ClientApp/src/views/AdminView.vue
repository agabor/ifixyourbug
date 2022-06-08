<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :class="{'active': page === 'email'}" icon="email-83" title="Email" subTitle="Enter your email." buttonText="Submit" :error="error" @onClickBtn="trySubmitEmail()">
          <div class="row mb-4">
            <input id="emailInput" class="form-control" placeholder="email@example.com" type="email" @keyup.enter="trySubmitEmail()" v-model="order.email">
          </div>
        </carousel-item>
        <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
        <carousel-item :class="{'active': page === 'orders'}" width="col-12">
          <table class="table align-items-center mb-0">
            <thead>
              <tr>
                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('admin.framework') }}</th>
                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">{{ $t('admin.version') }}</th>
                <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('admin.thirdPartyTool') }}</th>
                <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('admin.bugDescription') }}</th>
                <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('admin.projectDescription') }}</th>
                <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('admin.gitAccessId') }}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(order, idx) in orders" :key="idx">
                <td>
                  <span class="text-secondary text-xs font-weight-bold">{{ order.framework == 0 ? 'Vue.js' : 'ASP.NET Core' }}</span>
                </td>
                <td>
                  <span class="text-secondary text-xs font-weight-bold">{{ order.version }}</span>
                </td>
                <td class="align-middle text-center text-sm">
                  <span class="badge badge-sm badge-success">{{ order.thirdPartyTool }}</span>
                </td>
                <td class="align-middle text-center">
                  <text-viewer :id="`bug-description-input-${idx}`" :value="order.bugDescription"></text-viewer>
                </td>
                <td class="align-middle text-center">
                  <text-viewer :id="`project-description-input-${idx}`" :value="order.projectDescription"></text-viewer>
                </td>
                <td class="align-middle text-center">
                  <span class="text-secondary text-xs font-weight-bold">{{ order.gitAccessId }}</span>
                </td>
              </tr>
            </tbody>
          </table>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { validEmail } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import TwoFa from '../components/2FA.vue';
import TextViewer from '../components/TextViewer.vue';
import CarouselItem from '../components/CarouselItem.vue';

export default {
  name: 'AdminView',
  components: { TwoFa, TextViewer, CarouselItem },
  setup() {
    const { tm } = useI18n();
    const page = ref('email');
    const order = ref({});
    const error = ref(null);
    const auth = ref('');
    const orders = ref([]);
    let clientId;
    let jwt;

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
          jwt = localStorage.getItem('jwt');
          setOrders();
        }
      }
    }

    function trySubmitEmail() {
      let err = validEmail(order.value.email);
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
        body: JSON.stringify({'email': order.value.email})
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
      if(jwt) {
        setOrders();
      }
    }

    async function tryAuthentication() {
      const response = await fetch(`authenticate/admin/${clientId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'clientId': clientId, 'password': auth.value})
      });
      jwt = (await response.json()).jwt;
      localStorage.setItem('jwt', jwt);
      error.value = null;
    }

    function handleAuthenticationError() {
      jwt = null;
      error.value = tm('errors.wrongCode');
    }

    async function setOrders() {
      let orderResponse = await fetch('/api/admin/orders', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt}`,
          'Content-Type': 'application/json'
        }
      })
      orders.value = await orderResponse.json();
      page.value = 'orders';        
    }

    return { page, error, order, orders, auth, trySubmitEmail, checkAuthentication }
  }
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>