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
            <div class="d-block">
              <button type="button" class="btn btn-sm btn-outline-primary text-nowrap mb-0">{{ $t('nameCard.follow') }}</button>
            </div>
          </div>
          <div class="row mb-4">
            <div class="col-auto">
              <span class="h6">323</span>
              <span>{{ $t('nameCard.posts') }}</span>
            </div>
            <div class="col-auto">
              <span class="h6">3.5k</span>
              <span>{{ $t('nameCard.followers') }}</span>
            </div>
            <div class="col-auto">
              <span class="h6">260</span>
              <span>{{ $t('nameCard.following') }}</span>
            </div>
          </div>
          <p class="text-lg mb-0">
            {{ $t('nameCard.description') }}
            <br><a href="javascript:;" class="text-primary text-decoration-underline-hover">{{ $t('nameCard.more') }}</a>
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
