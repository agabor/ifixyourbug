<template>
 <header>
    <div class="page-header min-vh-85">
      <div>
        <img class="position-absolute fixed-top ms-auto w-70 h-100 z-index-0 d-block border-radius-xl border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0 fit-cover" src="../../assets/img/bg5.webp" alt="image">
      </div>
      <div class="container">
        <div class="row">
          <div class="col-lg-7 d-flex">
            <div class="card card-body text-center px-sm-5 shadow mt-sm-5 py-sm-5">
              <div class="row justify-content-center">
                <img class="col-md-4 col-12 header-logo w-30" src="../../assets/logo.webp" alt="logo">
                <div class="col-md-8 col-12 text-md-start text-center">
                  <h2 class="mb-0">{{ $t('mainCard.title') }}</h2>
                  <p class="lead pe-md-3 me-md-3 py-md-3 my-md-3">{{ $t('mainCard.description') }}</p>
                  <button type="button" class="btn btn-rounded bg-gradient-primary me-2" @click="toPricing">{{ $t('mainCard.pricing') }}</button>
                  <button type="button" class="btn btn-rounded btn-outline-secondary" @click="$router.push('/contact-form')">{{ $t('mainCard.contact') }}</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
import { event } from 'vue-gtag';

export default {
  name: 'HeaderComponent',
  setup() {
    function toPricing() {
      event('scroll-to-pricing');
      document.getElementById('pricing').scrollIntoView();
    }

    return { toPricing }
  }
}
</script>

<style>
  .btn-outline-secondary {
    background: white;
    opacity: 0.8;
  }
  .header-logo {
    object-fit: contain;
  }
  
</style>