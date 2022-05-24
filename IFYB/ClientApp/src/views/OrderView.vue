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
                            <label class="">Framework*</label>
                            <select class="form-control" :class="{'text-black-50': order.framework == undefined}" name="choices-framework" id="choices-framework" v-model="order.framework" @change="order.version = undefined">
                              <option :value="undefined" selected hidden>Select a framework</option>
                              <option :value="0">Vue.js</option>
                              <option :value="1">ASP.NET Core</option>
                            </select>
                          </div>
                          <div class="col-md-6 ps-md-2">
                            <label class="">Version*</label>
                            <select v-if="order.framework == 0" class="form-control" :class="{'text-black-50': order.version == undefined}" name="choices-version" id="choices-version" v-model="order.version">
                              <option :value="undefined" selected hidden>Select version</option>
                              <option :value="version" v-for="version in vueVersions" :key="version">{{ version }}</option>
                            </select>
                            <select v-else-if="order.framework == 1" class="form-control" :class="{'text-black-50': order.version == undefined}" name="choices-version" id="choices-version" v-model="order.version">
                              <option :value="undefined" selected hidden>Select version</option>
                              <option :value="version" v-for="version in aspVersions" :key="version">{{ version }}</option>
                            </select>
                            <input v-else type="text" placeholder="Please select a framework first" class="form-control"  disabled>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3" v-if="gitAccesses.length > 0">
                          <label class="">Previous accesses</label>
                          <select class="form-control text-black-50" name="choices-git-access" id="choices-git-access" v-model="selectedAccess">
                            <option :value="{}" selected hidden>Select a previous access</option>
                            <option :value="access" v-for="(access, idx) in gitAccesses" :key="idx">{{ access.accessMode == 0 ? 'Public repo' : access.accessMode == 1 ? 'Invite' : 'User account' }} - {{ access.url }}</option>
                          </select>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <label>Git repo url*</label>
                          <input class="form-control" id="repo-url-input" placeholder="https://..." type="text" v-model="order.repoUrl" :disabled="selectedAccess.url">
                        </div>
                        <label>Project sharing with*</label>
                        <div class="col-md-12 d-flex pe-2 mb-3">
                          <div>
                            <div class="form-check me-3" >
                              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1" :value="0" v-model="order.accessMode" :disabled="selectedAccess.accessMode != undefined">
                              <label class="form-check-label" for="flexRadioDefault1">
                                Public repo
                              </label>
                            </div>
                          </div>
                          <div>
                            <div class="form-check me-3">
                              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" :value="1" v-model="order.accessMode" :disabled="selectedAccess.accessMode != undefined">
                              <label class="form-check-label" for="flexRadioDefault2">
                                Invite
                              </label>
                            </div>
                          </div>
                          <div>
                            <div class="form-check me-3">
                              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault3" :value="2" v-model="order.accessMode" :disabled="selectedAccess.accessMode != undefined">
                              <label class="form-check-label" for="flexRadioDefault3">
                                User account
                              </label>
                            </div>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <div class="form-group mb-0">
                            <label>Project description*</label>
                            <textarea name="message" class="form-control border-radius-lg" id="project-description-input" rows="6" placeholder="Project description" v-model="order.projectDescription"></textarea>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <div class="form-group mb-0">
                            <label>Bug description*</label>
                            <textarea name="message" class="form-control border-radius-lg" id="bug-description-input" rows="6" placeholder="Bug description" v-model="order.bugDescription"></textarea>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2">
                          <div class="form-check form-switch">
                            <input class="form-check-input" type="checkbox" id="third-party-tool-input" v-model="order.isThirdPartyTool">
                            <label class="form-check-label" for="third-party-tool-input">Is the bug potentially related to a third party library?</label>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3" v-if="order.isThirdPartyTool">
                          <div class="form-group mb-0">
                            <label>Third party library name*</label>
                            <input id="third-party-tool-input" class="form-control" placeholder="Third party library name" type="text" v-model="order.thirdPartyTool">
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
        <div class="carousel-item" :class="{'active': page === 'success'}">
          <div class="container">
            <div class="row">
              <div class="col-lg-5 col-md-7 mx-auto">
                <div class="card">
                  <div class="card-body px-lg-5 py-lg-5 text-center">
                    <div class="info mb-4">
                      <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                        <i class="ni ni-send opacity-10 mt-2"></i>
                      </div>
                    </div>
                    <h2>Successful order</h2>
                    <p class="mb-4">We will contact you shortly via email.</p>
                    <div class="text-center">
                      <button type="button" class="btn bg-gradient-primary my-4" @click="$router.push('/')">Bact to home</button>
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
import { validEmail, required } from '../utils/Validate';
import TwoFa from '../components/2FA.vue';
export default {
  name: 'OrderView',
  components: { TwoFa },
  setup() {
    const page = ref('email');
    const order = ref({});
    const error = ref(null);
    const auth = ref('');
    const aspVersions = ['3.1', '5.0', '6.0', '7.0'];
    const vueVersions = ['2.6', '3.0', '3.1', '3.2'];
    const gitAccesses = ref([]);
    const selectedAccess = ref({});
    let clientId;
    let jwt;
    order.value.isThirdPartyTool = false;

    watch(selectedAccess, () => {
      if(selectedAccess.value) {
        order.value.accessMode = selectedAccess.value.accessMode;
        order.value.repoUrl = selectedAccess.value.url;
      } else {
        order.value.accessMode = undefined;
        order.value.repoUrl = undefined;
      }
    })

    checkJwtIsActive();

    async function checkJwtIsActive() {
      if(localStorage.getItem('jwt')) {
        let authResponse = await fetch('/authenticate/check-jwt', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(authResponse.status == 200) {
          jwt = localStorage.getItem('jwt');
          let nameResponse = await fetch('/clients/name', {
            method: 'GET',
            headers: {
              'Authorization': `bearer ${jwt}`
            }
          })
          if(nameResponse.status == 404) {
            page.value = 'name';
            document.getElementById('name-input').focus();
          } else {
            let gitResponse = await fetch('/git-accesses', {
              method: 'GET',
              headers: {
                'Authorization': `bearer ${jwt}`
              }
            })
            gitAccesses.value = await gitResponse.json();
            page.value = 'data';
            document.getElementById('choices-framework').focus();
          }
        }
      }
    }
    
    async function submitEmail() {
      let err = validEmail(order.value.email);
      if(err) {
        error.value = err;
        document.getElementById('emailInput').focus();
      } else {
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
        error.value = null;
      }
    }

    async function checkAuthentication(code) {
      auth.value = code;
      try {
        const response = await fetch(`/authenticate/${clientId}`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'clientId': clientId, 'password': auth.value})
        });
        jwt = (await response.json()).jwt;
        localStorage.setItem('jwt', jwt);
        error.value = null;
      } catch(e) {
        jwt = null;
        error.value = 'Wrong code.';
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
          document.getElementById('name-input').focus();
        } else {
          let gitResponse = await fetch('/git-accesses', {
            method: 'GET',
            headers: {
              'Authorization': `bearer ${jwt}`
            }
          })
          gitAccesses.value = await gitResponse.json();
          page.value = 'data';
          document.getElementById('choices-framework').focus();
        }
      }
    }

    async function setName() {
      let err = required(order.value.name, 'Name', 'name-input');
      if(err) {
        error.value = err;
      } else {
        await fetch('/clients/name', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${jwt}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'name': order.value.name})
        });
        page.value = 'data';
        document.getElementById('choices-framework').focus();
        error.value = null;
      }
    }

    async function submitOrder() {
      let err =
        required(order.value.framework, 'Framework', 'choices-framework') ||
        required(order.value.version, 'Version', 'choices-version') ||
        required(order.value.repoUrl, 'Git repo url', 'repo-url-input') ||
        required(order.value.accessMode, 'Project sharing') ||
        required(order.value.projectDescription, 'Project description', 'project-description-input') ||
        required(order.value.bugDescription, 'Bug description', 'bug-description-input');
      if(!err && order.value.isThirdPartyTool)
        err = required(order.value.thirdPartyTool, 'Third party tool', 'third-party-tool-input');
      if(err) {
        error.value = err;
      } else {
        let gitAccessId;
        if(selectedAccess.value.id != undefined){
          gitAccessId = selectedAccess.value.id;
        } else {
          let gitResponse = await fetch('/git-accesses', {
            method: 'POST',
            headers: {
              'Authorization': `bearer ${jwt}`,
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({'url': order.value.repoUrl, 'accessMode': order.value.accessMode})
          });
          gitAccessId = (await gitResponse.json()).id;
        }
        let orderResponse = await fetch('/orders', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${jwt}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            'framework': order.value.framework,
            'version': order.value.version,
            'thirdPartyTool': order.value.isThirdPartyTool ? order.value.thirdPartyTool : '',
            'projectDescription': order.value.projectDescription,
            'bugDescription': order.value.bugDescription,
            'gitAccessId': gitAccessId
          })
        });
        if(orderResponse.status == 200){
          page.value = 'success';
          error.value = null;
        } else {
          error.value = `Something wrong - ${orderResponse.statusText} (${orderResponse.status})`;
        }
      }
    }
    return { page, error, order, auth, aspVersions, vueVersions, gitAccesses, selectedAccess, submitEmail, checkAuthentication, setName, submitOrder }
  }
}
</script>

<style>
#carousel-testimonials {
  background-image: url('../assets/img/pricing3.jpg');
}
</style>