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
        <div class="col-lg-6 justify-content-center flex-column d-flex my-3">
          <div class="row justify-content-center d-flex">
            <div class="col-12 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom mt-n0 ms-4">
                <span class="fab fa-ubuntu" aria-hidden="true"></span>
              </div>
            </div>
          </div>
          <div class="row justify-content-center d-flex">
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom mt-md-0 mb-md-4 ms-md-4 mt-0 mb-4 ms-n6">
                <span class="fab fa-centos" aria-hidden="true"></span>
              </div>
            </div>
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom mt-md-3 ms-md-4 mt-2 ms-7">
                <span class="fab fa-linux" aria-hidden="true"></span>
              </div>
            </div>
          </div>
          <div class="row justify-content-center d-flex">
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom mt-md-5 ms-md-n5 mt-4 ms-n8">
                <span class="fab fa-vuejs" aria-hidden="true"></span>
              </div>
            </div>
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom mt-md-6 ms-md-6 mt-6 ms-7">
                <span class="fab fa-aws" aria-hidden="true"></span>
              </div>
            </div>
          </div>
          <div class="row justify-content-center d-flex">
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom mt-md-5 ms-md-7 mt-4 ms-0">
                <span class="fab fa-chrome" aria-hidden="true"></span>
              </div>
            </div>
            <div class="col-6 d-flex justify-content-center">
              <div class="d-flex align-items-center justify-content-center bg-white avatar avatar-lg rounded-circle fadeIn3 fadeInBottom mt-md-6 ms-md-n8 mt-5 ms-n6">
                <span class="fab fa-microsoft" aria-hidden="true"></span>
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