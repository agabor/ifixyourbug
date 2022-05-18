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
        <div class="carousel-item" :class="{'active': page === 'name'}">
          <div class="container">
            <div class="row">
              <div class="col-lg-5 col-md-7 mx-auto">
                <div class="card">
                  <div class="card-body px-lg-5 py-lg-5 text-center">
                    <div class="info mb-4">
                      <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                        <i class="ni ni-badge opacity-10 mt-2"></i>
                      </div>
                    </div>
                    <h2>Name</h2>
                    <p class="mb-4">Enter your name.</p>
                    <div class="row mb-4">
                      <input id="name-input" class="form-control" placeholder="Your Name" type="text" @keyup.enter="setName()" v-model="order.name">
                    </div>
                    <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
                      {{error}}
                    </div>
                    <div class="text-center">
                      <button type="button" class="btn bg-gradient-primary my-4" @click="setName()">Save</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="carousel-item" :class="{'active': page === 'data'}">
          <div class="container">
            <div class="row">
              <div class="col-lg-9 col-md-11 mx-auto">
                <div class="card">
                  <div class="card-body px-lg-5 py-lg-5">
                    <div class="info mb-4 text-center">
                      <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                        <i class="ni ni-spaceship opacity-10 mt-2"></i>
                      </div>
                    </div>
                    <h2 class="text-center">Order data</h2>
                    <p class="mb-4 text-center">Enter data from your app.</p>
                    <form>
                      <div class="row">
                        <div class="col-md-12 d-flex pe-2 mb-3">
                          <div class="col-md-6">
                            <label class="">Framework</label>
                            <select class="form-control" name="choices-framework" id="choices-framework" placeholder="Framework" v-model="order.framework">
                              <option :value="0">Vuejs</option>
                              <option :value="1">.Net</option>
                            </select>
                          </div>
                          <div class="col-md-6 ps-md-2">
                            <label>Version</label>
                            <input class="form-control" id="version-input" placeholder="x.x" type="text" v-model="order.version">
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <label>Git repo url</label>
                          <input class="form-control" id="repo-url-input" placeholder="https://..." type="text" v-model="order.repoUrl">
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <label>Third party tool</label>
                          <input class="form-control" id="third-party-tool-input" placeholder="..." type="text" v-model="order.thirdPartyTool">
                        </div>
                        <label>Project sharing with</label>
                        <div class="col-md-12 d-flex pe-2 mb-3">
                          <div>
                            <div class="form-check me-3" >
                              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1" value="public-repo" v-model="order.repoType">
                              <label class="form-check-label" for="flexRadioDefault1">
                                Public repo
                              </label>
                            </div>
                          </div>
                          <div>
                            <div class="form-check me-3">
                              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" value="invite" v-model="order.repoType">
                              <label class="form-check-label" for="flexRadioDefault2">
                                Invite
                              </label>
                            </div>
                          </div>
                          <div>
                            <div class="form-check me-3">
                              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault3" value="user-acc" v-model="order.repoType">
                              <label class="form-check-label" for="flexRadioDefault3">
                                User account
                              </label>
                            </div>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <div class="form-group mb-0">
                            <label>Project description</label>
                            <textarea name="message" class="form-control border-radius-lg" id="project-description-input" rows="6" placeholder="Project description" v-model="order.projectDescription"></textarea>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <div class="form-group mb-0">
                            <label>Error description</label>
                            <textarea name="message" class="form-control border-radius-lg" id="error-description-input" rows="6" placeholder="Error description" v-model="order.errorDescription"></textarea>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
                          {{error}}
                        </div>
                        <div class="col-md-6 text-end ms-auto">
                          <button type="button" class="btn btn-round bg-gradient-primary mb-0" @click="submitOrder()">Submit</button>
                        </div>
                      </div>
                    </form>
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
  name: 'OrderView',
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
        document.getElementById("emailInput").focus();
      } else {
        error.value = null;
        const response = await fetch('/authenticate', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'email': order.value.email})
        });
        clientId = (await response.json()).id;
        page.value = 'auth';
        document.getElementById('2fa-0').focus();
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
          const response = await fetch(`/authenticate/${clientId}`, {
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
        let nameResponse = await fetch('/clients/name', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${jwt}`
          }
        })
        if(nameResponse.status == 404) {
          page.value = 'name';
          document.getElementById("name-input").focus();
        } else {
          page.value = 'data';
          document.getElementById("choices-framework").focus();
        }
      }
    }

    async function setName() {
      let err = required(order.value.name, 'Name', 'name-input');
      if(err) {
        error.value = err;
      } else {
        error.value = null;
        await fetch('/clients/name', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${jwt}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'name': order.value.name})
        });
        page.value = 'data';
        document.getElementById("choices-framework").focus();
      }
    }

    async function submitOrder() {
      let err =
        required(order.value.framework, 'Framework', 'choices-framework') ||
        required(order.value.version, 'Version', 'version-input') ||
        required(order.value.repoUrl, 'Git repo url', 'repo-url-input') ||
        required(order.value.repoType, 'Project sharing') ||
        required(order.value.projectDescription, 'Project description', 'project-description-input') ||
        required(order.value.errorDescription, 'Error description', 'error-description-input');
      if(err) {
        error.value = err;
      } else {
        error.value = null;
        let gitResponse = await fetch('/git-accesses', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${jwt}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'url': order.value.repoUrl, 'accessMode': 0})
        });
        let id = (await gitResponse.json()).id;
        await fetch('/orders', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${jwt}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            "framework": order.value.framework,
            "version": order.value.version,
            "thirdPartyTool": order.value.thirdPartyTool,
            "projectDescription": order.value.projectDescription,
            "bugDescription": order.value.errorDescription,
            "gitAccessId": id
          })
        });
      }
    }
    return { page, error, order, auth, submitEmail, checkAuthentication, setName, submitOrder, deleteFromAuth }
  }
}
</script>

<style>
#carousel-testimonials {
  background-image: url('../assets/img/pricing3.jpg');
}
</style>