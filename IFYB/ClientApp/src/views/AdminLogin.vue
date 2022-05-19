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
        <div class="carousel-item" :class="{'active': page === 'auth'}">
          <div class="container">
            <div class="row">
              <div class="col-lg-5 col-md-7 mx-auto">
                <div class="card">
                  <div class="card-body px-lg-5 py-lg-5 text-center">
                    <div class="info mb-4">
                      <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                        <i class="ni ni-atom opacity-10 mt-2"></i>
                      </div>
                    </div>
                    <h2>2FA Security</h2>
                    <p class="mb-4">Enter 6-digits code from your athenticatior app.</p>
                    <div class="row mb-4">
                      <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2">
                        <input type="text" id="2fa-0" class="form-control text-lg text-center" @keyup.enter="checkAuthentication()" @keyup.delete="deleteFromAuth(0)" v-model="auth[0]" aria-label="2fa">
                      </div>
                      <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2">
                        <input type="text" id="2fa-1" class="form-control text-lg text-center" @keyup.enter="checkAuthentication()" @keyup.delete="deleteFromAuth(1)" v-model="auth[1]" aria-label="2fa">
                      </div>
                      <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2">
                        <input type="text" id="2fa-2" class="form-control text-lg text-center" @keyup.enter="checkAuthentication()" @keyup.delete="deleteFromAuth(2)" v-model="auth[2]" aria-label="2fa">
                      </div>
                      <div class="col-lg-2 col-md-2 col-2 pe-0 pe-md-2">
                        <input type="text" id="2fa-3" class="form-control text-lg text-center" @keyup.enter="checkAuthentication()" @keyup.delete="deleteFromAuth(3)" v-model="auth[3]" aria-label="2fa">
                      </div>
                      <div class="col-lg-2 col-md-2 col-2 pe-0 pe-md-2">
                        <input type="text" id="2fa-4" class="form-control text-lg text-center" @keyup.enter="checkAuthentication()" @keyup.delete="deleteFromAuth(4)" v-model="auth[4]" aria-label="2fa">
                      </div>
                      <div class="col-lg-2 col-md-2 col-2 pe-0 pe-md-2">
                        <input type="text" id="2fa-5" class="form-control text-lg text-center" @keyup.enter="checkAuthentication()" @keyup.delete="deleteFromAuth(5)" v-model="auth[5]" aria-label="2fa">
                      </div>
                    </div>
                    <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
                      {{error}}
                    </div>
                    <div class="text-center">
                      <button type="button" id="2fa-btn" class="btn bg-gradient-primary my-4" @click="checkAuthentication()">Check</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { ref, watch } from 'vue';
import { validEmail, required, min } from '../utils/Validate';
export default {
  name: 'AdminLogin',
  setup() {
    const page = ref('email');
    const order = ref({});
    const error = ref(null);
    const auth = ref([])
    let clientId;
    let jwt;
    let authLength = 6;
    
    watch(auth.value, () => {
      for(let i = 0; i < authLength; i++) {
        if(auth.value[i] && auth.value[i].length > 1) {
          let code = auth.value[i];
          for(let j = 0; j < code.length; j++){
            if(i+j < authLength) {
              auth.value[i+j] = code[j];
            } else {
              break;
            }
          }
        }
      }

      let focused = false;
      for(let i = 0; i < authLength; i++) {
        if(auth.value[i] === '' || auth.value[i] === undefined) {
          document.getElementById('2fa-' + i).focus();
          focused = true;
          break;
        }
      }
      if(!focused) {
        checkAuthentication();
      }
    })
    
    function deleteFromAuth(idx) {
      if(idx - 1 > -1 && (auth.value[idx] === '' || auth.value[idx] === undefined)) { 
        auth.value[idx - 1] = ''
      }
    }
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

    async function checkAuthentication() {
      let err = required(auth.value.join(''), 'Authentication code', '2fa-0')?.message;
      if(!err)
        err = min(auth.value.join(''), 6);
      if(err) {
        error.value = err;
      } else {
        try {
          error.value = null;
          const response = await fetch(`authenticate/admin/${clientId}`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({'clientId': clientId, 'password': auth.value.join('')})
          });
          jwt = (await response.json()).jwt;
        } catch(e) {
          error.value = 'Wrong code.';
        }
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
    return { page, error, order, auth, submitEmail, checkAuthentication, deleteFromAuth }
  }
}
</script>
