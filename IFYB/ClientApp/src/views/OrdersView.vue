<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error" @onClickBtn="trySubmitEmail()">
          <div class="row mb-4">
            <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="user.email">
          </div>
        </carousel-item>
        <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
        <carousel-item :class="{'active': page === 'name'}" icon="badge" :title="$t('order.name')" :subTitle="$t('order.nameDes')" :buttonText="$t('order.save')" :error="error" @onClickBtn="trySetName()">
          <div class="row mb-4">
            <input id="name-input" class="form-control" placeholder="Your Name" type="text" @keyup.enter="trySetName()" v-model="user.name">
          </div>
        </carousel-item>
        <carousel-item :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders"></order-list>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { validEmail, required } from '../utils/Validate';
import TwoFa from '../components/2FA.vue';
import CarouselItem from '../components/CarouselItem.vue';
import OrderList from '../components/OrderList.vue';
import { useI18n } from "vue-i18n";

export default {
  name: 'OrdersView',
  components: { TwoFa, CarouselItem, OrderList },
  setup() {
    const { tm } = useI18n();
    const page = ref('email');
    const user = ref({});
    const error = ref(null);
    const auth = ref('');
    let clientId;
    let jwt;
    const orders = ref([]);

    setJwtIfActive();

    async function setJwtIfActive() {
      if(localStorage.getItem('jwt')) {
        let authResponse = await fetch('/api/authenticate/check-jwt', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(authResponse.status == 200) {
          jwt = localStorage.getItem('jwt');
          toNamePageOrToOrdersPage();
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
      const response = await fetch('/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': user.value.email})
      });
      clientId = (await response.json()).id;
      page.value = 'auth';
      error.value = null;
    }

    async function checkAuthentication(code) {
      auth.value = code;
      try {
        await tryAuthentication();
      } catch(e) {
        handleAuthenticationError();
      }
      if(jwt) {
        toNamePageOrToOrdersPage();
      }
    }

    async function tryAuthentication() {
      const response = await fetch(`/api/authenticate/${clientId}`, {
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

    function trySetName() {
      let err = required(user.value.name, tm('errors.requiredName'), 'name-input');
      if(err) {
        error.value = err;
      } else {
        setName();
      }
    }

    async function setName() {
      await fetch('/api/clients/name', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${jwt}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': user.value.name})
      });
      page.value = 'orders';
      error.value = null;
    }

    async function toNamePageOrToOrdersPage() {
      let nameResponse = await fetch('/api/clients/name', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt}`
        }
      })
      if(nameResponse.status == 404) {
        page.value = 'name';
      } else {
        setOrders();
      }
    }

    async function setOrders() {
      let orderResponse = await fetch('/api/orders', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt}`
        }
      })
      orders.value = await orderResponse.json();
      console.log(orders.value);
      page.value = 'orders';
    }

    return { page, error, user, auth, orders, trySubmitEmail, checkAuthentication, trySetName }
  }
}
</script>

<style>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/pricing3.jpg');
}
.full-height{
  overflow: auto;
  height: 100vh;
}

</style>