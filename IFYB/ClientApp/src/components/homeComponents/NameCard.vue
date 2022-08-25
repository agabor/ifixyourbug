<template>
  <section class="pb-7">
    <div class="author-header min-vh-25">
      <span class="mask bg-gradient-primary opacity-5"></span>
    </div>
    <div class="container">
      <div class="row bg-white px-4 py-5 mt-n7 position-relative shadow border-radius-lg">
        <div class="col-lg-3 col-md-5 position-relative my-auto">
          <img class="img rounded-circle max-width-200 w-100 position-relative z-index-2" src="../../assets/img/gabor.webp" alt="bruce">
        </div>
        <div class="col-md-9 z-index-2 position-relative px-md-2 px-sm-5 mt-sm-0 mt-4">
          <div class="d-flex justify-content-between align-items-center mb-2">
            <h4 class="mb-0">{{ $t('nameCard.name') }}</h4>
          </div>
          <div class="row mb-4">
            <div class="col-auto">
              <a href="https://github.com/agabor" target="_blank" @click="$event('navigate-to-github')"><i class="fab fa-github" aria-hidden="true"></i> GitHub</a>
            </div>
            <div class="col-auto">
              <a href="https://stackoverflow.com/users/3580498/gábor-angyal" target="_blank" @click="$event('navigate-to-stackoverflow')"><i class="fab fa-stack-overflow" aria-hidden="true"></i> Stack Overflow</a>
            </div>
          </div>
          <p class="text-lg mb-0">
            {{ $t('nameCard.description') }}
          </p>
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
    background-image: url('../../assets/img/bg2.webp');
    object-fit: cover;
    position: relative;
    background-size: cover;
    background-position: top;
  }
</style>
