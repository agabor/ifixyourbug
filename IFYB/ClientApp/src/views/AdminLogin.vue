<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <div class="carousel-item" :class="{'active': page === 'email'}">
          <div class="container">
            <div class="row">
              <div class="col-lg-5 col-md-7 mx-auto">
                <div class="card">
                  <div class="card-body px-lg-5 py-lg-5 text-center">
                    <div class="info mb-4">
                      <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                        <i class="ni ni-email-83 opacity-10 mt-2"></i>
                      </div>
                    </div>
                    <h2>Email</h2>
                    <p class="mb-4">Enter your email.</p>
                    <div class="row mb-4">
                      <input id="emailInput" class="form-control" placeholder="email@example.com" type="email" @keyup.enter="submitEmail()" v-model="order.email" autofocus>
                    </div>
                    <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
                      {{error}}
                    </div>
                    <div class="text-center">
                      <button type="button" class="btn bg-gradient-primary my-4" @click="submitEmail()">Submit</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { validEmail } from '../utils/Validate';
import TwoFa from '../components/2FA.vue';
export default {
  name: 'AdminLogin',
  components: { TwoFa },
  setup() {
    const page = ref('email');
    const order = ref({});
    const error = ref(null);
    const auth = ref('');
    let clientId;
    let jwt;
    
    async function submitEmail() {
      let err = validEmail(order.value.email);
      if(err) {
        error.value = err;
        document.getElementById('emailInput').focus();
      } else {
        error.value = null;
        try {
          const response = await fetch('authenticate/admin', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({'email': order.value.email})
          });
          clientId = (await response.json()).id;
          page.value = 'auth';
          document.getElementById('2fa-0').focus();
        } catch(e) {
          error.value = 'This email is not an administrator email.'
        }
      }
    }

    async function checkAuthentication(code) {
      auth.value = code;
      try {
        error.value = null;
        const response = await fetch(`authenticate/admin/${clientId}`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'clientId': clientId, 'password': auth.value})
        });
        jwt = (await response.json()).jwt;
      } catch(e) {
        jwt = null;
        error.value = 'Wrong code.';
      }
      if(jwt) {
        let adminResponse = await fetch('/admin/contact-messages', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${jwt}`,
            'Content-Type': 'application/json'
          }
        })
        console.log('adminResponse', await adminResponse.json());
      }
    }
    return { page, error, order, auth, submitEmail, checkAuthentication }
  }
}
</script>
