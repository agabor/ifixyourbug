<template>
 <header>
    <div class="page-header min-vh-85">
      <div>
        <img class="position-absolute fixed-top ms-auto w-50 h-100 z-index-0 d-none d-sm-none d-md-block border-radius-xl border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0" src="../../assets/img/pricing3.jpg" alt="image">
      </div>
      <div class="container">
        <div class="row">
          <div class="col-lg-7 d-flex">
            <div class="card card-body text-md-start text-center px-sm-5 shadow mt-sm-5 py-sm-5">
              <h2 class="mb-0">{{ $t('mainCard.title') }}</h2>
              <h2 class="text-dark mb-4">{{ $t('mainCard.subTitle') }}</h2>
              <p class="lead pe-md-5 me-md-5">{{ $t('mainCard.description') }}</p>
              <img class="col-5 col-md-4 position-absolute bottom-0 end-0 z-index-1 header-logo" src="../../assets/logo.webp" alt="logo">
              <div class="buttons z-index-2">
                <button type="button" class="btn btn-rounded bg-gradient-primary mt-4" @click="$router.push('/new-order')">{{ $t('mainCard.order') }}</button>
                <button type="button" class="btn btn-rounded btn-outline-secondary mt-4 ms-2" @click="$router.push('/faq')">{{ $t('mainCard.faq') }}</button>
                <button type="button" class="btn btn-rounded btn-outline-secondary mt-4 ms-2" @click="$router.push('/contact-form')">{{ $t('mainCard.contact') }}</button>
              </div>
              <div class="row mt-2 z-index-2">
                <div class="col-md-4 col-6 d-flex justify-content-center align-items-center p-0">
                  <img class="w-15" src="../../assets/img/logos/gray-logos/vue-logo.png" alt="logo">
                  <p class="m-0 mx-2 fw-bolder">{{ $t('mainCard.vuejs') }}</p>
                </div>
                <div class="col-md-4 col-6 d-flex justify-content-center align-items-center p-0">
                  <img class="w-15" src="../../assets/img/logos/gray-logos/dotnet-logo.png" alt="logo">
                  <p class="m-0 mx-2 fw-bolder">{{ $t('mainCard.asp') }}</p>
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
  @media (max-width: 768px) {
    .header-logo {
      position: unset!important;
      display: flex;
      align-self: center;
    }
  }
  
</style>