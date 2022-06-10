<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error" @onClickBtn="trySubmitEmail()">
          <div class="row mb-4">
            <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="order.email">
          </div>
        </carousel-item>
        <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
        <carousel-item :class="{'active': page === 'name'}" icon="badge" :title="$t('order.name')" :subTitle="$t('order.nameDes')" :buttonText="$t('order.save')" :error="error" @onClickBtn="trySetName()">
          <div class="row mb-4">
            <input id="name-input" class="form-control" placeholder="Your Name" type="text" @keyup.enter="trySetName()" v-model="order.name">
          </div>
        </carousel-item>
        <carousel-item class="full-height" :class="{'active': page === 'data'}" width="col-lg-9 col-md-11" icon="spaceship" :title="$t('order.orderData')" :subTitle="$t('order.orderDataDes')">
          <new-order-form :jwt="jwt" :gitAccesses="gitAccesses" @toSuccessPage="page = 'success'"></new-order-form>
        </carousel-item>
        <carousel-item :class="{'active': page === 'success'}" icon="send" :title="$t('order.successfulOrder')" :subTitle="$t('order.successfulOrderDes')" :buttonText="$t('order.backToHome')" @onClickBtn="$router.push('/')"></carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { validEmail, required } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import TwoFa from '../components/2FA.vue';
import CarouselItem from '../components/CarouselItem.vue';
import NewOrderForm from '../components/NewOrderForm.vue';

export default {
  name: 'OrderView',
  components: { TwoFa, CarouselItem, NewOrderForm },
  setup() {
    const { tm } = useI18n();
    const page = ref('email');
    const order = ref({});
    const error = ref(null);
    const auth = ref('');
    const gitAccesses = ref([]);
    let clientId;
    const jwt = ref('');

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
          jwt.value = localStorage.getItem('jwt');
          toNamePageOrToDataPage();
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
      const response = await fetch('/api/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': order.value.email})
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
      if(jwt.value) {
        toNamePageOrToDataPage();
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
      jwt.value = (await response.json()).jwt;
      localStorage.setItem('jwt', jwt.value);
      error.value = null;
    }

    function handleAuthenticationError() {
      jwt.value = null;
      error.value = tm('errors.wrongCode');
    }

    function trySetName() {
      let err = required(order.value.name, tm('errors.requiredName'), 'name-input');
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
          'Authorization': `bearer ${jwt.value}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': order.value.name})
      });
      page.value = 'data';
      error.value = null;
    }

    async function toNamePageOrToDataPage() {
      let nameResponse = await fetch('/api/clients/name', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt.value}`
        }
      })
      if(nameResponse.status == 404) {
        page.value = 'name';
      } else {
        let gitResponse = await fetch('/api/git-accesses', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${jwt.value}`
          }
        })
        gitAccesses.value = await gitResponse.json();
        page.value = 'data';
      }
    }

    return { page, error, order, jwt, auth, trySubmitEmail, checkAuthentication, trySetName, gitAccesses }
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