<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100 carousel slide carousel-team" data-bs-ride="carousel" data-bs-interval="false" style="background-image: url('../assets/img/pricing3.jpg');">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <div class="carousel-item active">
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
                    <form>
                      <div class="row mb-4">
                        <input class="form-control" placeholder="email@example.com" type="email" v-model="order.email">
                      </div>
                      <div class="text-center" href="#carousel-testimonials" data-bs-slide="next">
                        <button type="button" class="btn bg-gradient-primary my-4" @click="submitEmail()">Submit</button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="carousel-item">
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
                    <form>
                      <div class="row mb-4">
                        <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2">
                          <input type="text" class="form-control text-lg text-center" value="" aria-label="2fa">
                        </div>
                        <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2">
                          <input type="text" class="form-control text-lg text-center" value="" aria-label="2fa">
                        </div>
                        <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2">
                          <input type="text" class="form-control text-lg text-center" value="" aria-label="2fa">
                        </div>
                        <div class="col-lg-2 col-md-2 col-2 pe-0 pe-md-2">
                          <input type="text" class="form-control text-lg text-center" value="" aria-label="2fa">
                        </div>
                        <div class="col-lg-2 col-md-2 col-2 pe-0 pe-md-2">
                          <input type="text" class="form-control text-lg text-center" value="" aria-label="2fa">
                        </div>
                        <div class="col-lg-2 col-md-2 col-2 pe-0 pe-md-2">
                          <input type="text" class="form-control text-lg text-center" aria-label="2fa">
                        </div>
                      </div>
                      <div class="text-center" href="#carousel-testimonials" data-bs-slide="next">
                        <button type="button" class="btn bg-gradient-primary my-4">Check</button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="carousel-item">
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
                              <option value="vuejs">Vuejs</option>
                              <option value="dotnet">.Net</option>
                            </select>
                          </div>
                          <div class="col-md-6 ps-md-2">
                            <label>Version</label>
                            <input class="form-control" placeholder="x.x" type="text" v-model="order.version">
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <label>Git repo url</label>
                          <input class="form-control" placeholder="https://..." type="text" v-model="order.repoUrl">
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
                            <textarea name="message" class="form-control border-radius-lg" id="message" rows="6" placeholder="Project description" v-model="order.projectDescription"></textarea>
                          </div>
                        </div>
                        <div class="col-md-12 pe-2 mb-3">
                          <div class="form-group mb-0">
                            <label>Error description</label>
                            <textarea name="message" class="form-control border-radius-lg" id="message" rows="6" placeholder="Error description" v-model="order.errorDescription"></textarea>
                          </div>
                        </div>
                      </div>
                      <div class="row">
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
import { ref } from 'vue';
export default {
  name: 'OrderView',
  setup() {
    const order = ref({});

    function submitEmail() {
      console.log(order.value);
    }
    function submitOrder() {
      console.log(order.value);
    }
    return { order, submitEmail, submitOrder }
  }
}
</script>
