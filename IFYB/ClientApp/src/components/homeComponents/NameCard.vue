<template>
  <section class="pb-7">
    <div class="author-header min-vh-25">
      <span class="mask bg-gradient-primary opacity-5"></span>
    </div>
    <div class="container">
      <div class="row bg-white px-4 py-5 mt-n7 position-relative shadow border-radius-lg justify-content-center">
        <div class="col-lg-3 col-md-5 col-sm-12 position-relative my-auto d-flex align-items-center justify-content-center">
          <picture>
            <source 
              media="(min-width: 576px)"
              width="210"
              height="210"
              srcset="../../assets/img/gabor.webp">
            <img
              width="210"
              height="210"
              class="img rounded-circle position-relative z-index-2"
              src="../../assets/img/gabor_mobile.webp" 
              alt="gabor">
          </picture>
        </div>
        <div class="col-md-7 col-sm-12 z-index-2 position-relative px-md-2 px-sm-5 mt-sm-0 mt-4">
          <div class="d-flex justify-content-center justify-content-sm-start mb-2">
            <h4 class="mb-0">{{ $t('nameCard.name') }}</h4>
          </div>
          <div class="row mb-4 d-flex justify-content-center justify-content-sm-start">
            <div class="col-auto">
              <a href="https://github.com/agabor" target="_blank" @click="$event('navigate', {'value': 'github'})"><span class="fab fa-github" aria-hidden="true"></span> GitHub</a>
            </div>
            <div class="col-auto">
              <a href="https://stackoverflow.com/users/3580498/gábor-angyal" target="_blank" @click="$event('navigate', {'value': 'stackoverflow'})"><span class="fab fa-stack-overflow" aria-hidden="true"></span> Stack Overflow</a>
            </div>
          </div>
          <p class="text-lg mb-0 text-justify" v-html="$t('nameCard.description')"></p>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'NameCard',
}
</script>

<style>
  .author-header {
    object-fit: cover;
    position: relative;
    background-size: cover;
    background-position: top;
  }
  @media (min-width: 576px) {
    .author-header {
      background-image: url('../../assets/img/bg1.webp');
    }
  }
  @media (max-width: 576px) {
    .author-header {
      background-image: url('../../assets/img/bg1_mobile.webp');
    }
  }
</style>
