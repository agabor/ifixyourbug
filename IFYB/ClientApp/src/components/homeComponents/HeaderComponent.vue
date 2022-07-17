<template>
 <header>
    <div class="page-header min-vh-85">
      <div>
        <img class="position-absolute fixed-top ms-auto w-50 h-100 z-index-0 d-none d-sm-none d-md-block border-radius-xl border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0" src="../../assets/img/pricing3.jpg" alt="image">
      </div>
      <div class="container">
        <div class="row">
          <div class="col-lg-7 d-flex">
            <div class="card card-body text-center px-sm-5 shadow mt-sm-5 py-sm-5">
              <div class="row">
                <img class="col-4 header-logo" src="../../assets/logo.webp" alt="logo">
                <div class="col-8 text-md-start">
                  <h2 class="mb-0">{{ $t('mainCard.title') }}</h2>
                  <p class="lead pe-md-3 me-md-3">{{ $t('mainCard.description') }}</p>
                </div>
              </div>
              <div class="buttons z-index-2">
                <button type="button" class="btn btn-rounded bg-gradient-primary mt-4" @click="$router.push('/new-order')">{{ $t('mainCard.order') }}</button>
                <button type="button" class="btn btn-rounded btn-outline-secondary mt-4 ms-2" @click="$router.push('/contact-form')">{{ $t('mainCard.contact') }}</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
export default {
  name: 'HeaderComponent',
}
</script>

<style>
  .author-header {
    background-image: url('../../assets/img/bg45.jpg');
    position: relative;
    background-size: cover;
    background-position: 50%;
  }

  .btn-outline-secondary {
    background: white;
    opacity: 0.8;
  }
  .header-logo {
    object-fit: contain;
  }
  
</style>