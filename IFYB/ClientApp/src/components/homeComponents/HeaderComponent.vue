<template>
 <header>
    <div class="page-header">
      <img 
        class="d-none d-md-block position-absolute fixed-top ms-auto w-70 h-100 z-index-0 d-block border-radius-xl border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0 fit-cover"
        src="/hero.webp" 
        alt="hero"
        v-if="windowWidth > 768"
      >
      <div class="container">
        <div class="row">
          <div class="col-lg-7 d-flex">
            <div class="card card-body text-center shadow px-sm-5 my-md-0 mt-7 mb-6">
              <div class="row justify-content-center my-4">
                <picture class="col-xl-4 col-12 d-flex justify-content-center align-items-center">
                  <source 
                    media="(min-width: 576px)"
                    width="200"
                    height="200"
                    srcset="../../assets/img/logo.webp">
                  <img
                    width="100"
                    height="100"
                    class="header-logo"
                    src="../../assets/img/logo_mobile.webp" 
                    alt="logo">
                </picture>
                <div class="col-xl-8 col-12 text-xl-start text-center ps-xl-4">
                  <h2 class="mb-0">{{ $t('mainCard.title') }}</h2>
                  <p class="lead my-4">{{ $t('mainCard.description') }}</p>
                  <button type="button" class="btn btn-rounded bg-gradient-primary me-2 mb-0 mt-2" @click="toPricing">{{ $t('mainCard.pricing') }}</button>
                  <button type="button" class="btn btn-rounded btn-outline-secondary mb-0 mt-2" @click="$router.push('/contact-form')">{{ $t('mainCard.contact') }}</button>
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
import { onMounted, onUnmounted, ref } from "vue"

export default {
  name: 'HeaderComponent',
  setup() {
    function toPricing() {
      document.getElementById('pricing').scrollIntoView();
    }
    let windowWidth = ref(window.innerWidth)

    const onWidthChange = () => windowWidth.value = window.innerWidth
    onMounted(() => window.addEventListener('resize', onWidthChange))
    onUnmounted(() => window.removeEventListener('resize', onWidthChange))

    return { toPricing, windowWidth }
  }
}
</script>

<style scoped>
  .header-logo {
    object-fit: contain;
  }
  .page-header {
    background-image: none!important;
  }

  @media (min-width: 768px) {
    .page-header {
      min-height: 85vh;
    }
  }
  @media (max-width: 768px) {
    .page-header {
      min-height: unset;
    }
  }
</style>