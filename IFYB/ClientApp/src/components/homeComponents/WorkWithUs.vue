<template>
  <section class="bg-dark position-relative overflow-hidden mt-7">
    <div class="container py-6">
      <div class="row">
        <div class="col-lg-6 d-flex justify-content-center flex-column">
          <div id="carouselExampleIndicator" class="carousel slide py-3" data-bs-ride="carousel">
            <div class="carousel-inner">
              <h2 class="text-white mb-0">{{ $t('workWithUs.title') }}</h2>
              <p class="text-white opacity-8 mb-1">{{ $t('workWithUs.subTitle') }}</p>
              <hr class="text-white horizontal opacity-6 mb-4 mt-2 w-25 text-start">
              <div class="carousel-item active">
                <h6 class="text-white opacity-8 pe-5">{{ $t('workWithUs.carouselDes1') }}</h6>
              </div>
            </div>
          </div>
        </div>
        <div class="col-lg-6 justify-content-center flex-column d-flex">
          <div class="row justify-content-center d-flex mb-3">
            <div class="col-12 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom">
                <span class="fab fa-apple" aria-hidden="true"></span>
              </div>
            </div>
          </div>
          <div class="row justify-content-center d-flex my-4">
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom">
                <span class="fab fa-windows" aria-hidden="true"></span>
              </div>
            </div>
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom">
                <span class="fab fa-linux" aria-hidden="true"></span>
              </div>
            </div>
          </div>
          <div class="row justify-content-center d-flex mt-4 mb-3">
            <div class="col-md-3 col-4 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom">
                <span class="fab fa-vuejs" aria-hidden="true"></span>
              </div>
            </div>
            <div class="col-md-3 col-4 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom">
                <span class="fab fa-aws" aria-hidden="true"></span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'WorkWithUs',
}
</script>