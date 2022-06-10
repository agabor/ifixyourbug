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
          <order-list :orders="orders"></order-list>
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
import CarouselItem from '../components/CarouselItem.vue';
import OrderList from '../components/OrderList.vue';

export default {
  name: 'AdminView',
  components: { TwoFa, CarouselItem, OrderList },
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
      const response = await fetch(`/api/authenticate/admin/${clientId}`, {
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