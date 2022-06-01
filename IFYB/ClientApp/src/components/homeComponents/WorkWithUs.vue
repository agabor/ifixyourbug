<template>
  <section class="bg-dark position-relative overflow-hidden mt-7">
    <div class="container py-lg-8 py-7">
      <div class="row">
        <div class="col-lg-6 d-flex justify-content-center flex-column">
          <div id="carouselExampleIndicator" class="carousel slide py-7" data-bs-ride="carousel">
            <div class="carousel-indicators justify-content-start ms-0">
              <a href="javascript:;">
                <img src="../../assets/img/emily.jpg" alt="..." class="avatar avatar-sm avatar-scale-up shadow border-0 active rounded-circle" data-bs-target="#carouselExampleIndicator" data-bs-slide-to="0">
                <span class="text-white mx-2">&#124;</span>
              </a>
              <a href="javascript:;">
                <img src="../../assets/img/emilyz.jpg" alt="..." class="avatar avatar-sm avatar-scale-up shadow border-0 rounded-circle" data-bs-target="#carouselExampleIndicator" data-bs-slide-to="1">
                <span class="text-white mx-2">&#124;</span>
              </a>
              <a href="javascript:;">
                <img src="../../assets/img/james.jpg" alt="..." class="avatar avatar-sm avatar-scale-up shadow border-0 rounded-circle" data-bs-target="#carouselExampleIndicator" data-bs-slide-to="2">
              </a>
            </div>
            <div class="carousel-inner">
              <h2 class="text-white mb-0">{{ $t('workWithUs.title') }}</h2>
              <p class="text-white opacity-8 mb-1">{{ $t('workWithUs.subTitle') }}</p>
              <hr class="text-white horizontal opacity-6 mb-4 mt-2 w-25 text-start">
              <div class="carousel-item active">
                <h6 class="text-white opacity-8 pe-5">{{ $t('workWithUs.carouselDes1') }}</h6>
                <div class="author mt-4">
                  <div class="name">
                    <span class="text-white">{{ $t('workWithUs.carouselName1') }}</span>
                    <div class="stats">
                      <small class="text-white opacity-6">{{ $t('workWithUs.carouselPosition1') }}</small>
                    </div>
                  </div>
                </div>
              </div>
              <div class="carousel-item">
                <h6 class="text-white opacity-8 pe-5">{{ $t('workWithUs.carouselDes2') }}</h6>
                <div class="author mt-4">
                  <div class="name">
                    <span class="text-white">{{ $t('workWithUs.carouselName2') }}</span>
                    <div class="stats">
                      <small class="text-white opacity-6">{{ $t('workWithUs.carouselPosition2') }}</small>
                    </div>
                  </div>
                </div>
              </div>
              <div class="carousel-item">
                <h6 class="text-white opacity-8 pe-5">{{ $t('workWithUs.carouselDes3') }}</h6>
                <div class="author mt-4">
                  <div class="name">
                    <span class="text-white">{{ $t('workWithUs.carouselName3') }}</span>
                    <div class="stats">
                      <small class="text-white opacity-6">{{ $t('workWithUs.carouselPosition3') }}</small>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-6 justify-content-center flex-column d-sm-none d-md-none d-lg-flex d-xl-flex">
          <div class="row justify-content-center d-none d-md-flex">
            <div class="col-3 mb-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 mt-n4 ms-8 fadeIn2 fadeInBottom">
                <img src="../../assets/img/logos/small-logos/logo-asana.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-4 mb-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-8 mt-n6 fadeIn1 fadeInBottom">
                <img src="../../assets/img/logos/small-logos/logo-slack.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-4 mb-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-6 mt-2 fadeIn3 fadeInBottom">
                <img src="../../assets/img/logos/small-logos/logo-google-drive.svg" alt="Logo Image">
              </div>
            </div>
          </div>
          <div class="row justify-content-end d-none d-md-flex">
            <div class="col-4 my-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-4 fadeIn1 fadeInBottom">
                <img class="avatar-img" src="../../assets/img/logos/small-logos/logo-shopify.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-3 my-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 me-auto fadeIn1 fadeInBottom">
                <img class="avatar-img" src="../../assets/img/logos/small-logos/logo-apple.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-3 my-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 fadeIn3 fadeInBottom ms-3 mt-3">
                <img class="avatar-img" src="../../assets/img/logos/small-logos/logo-invision.svg" alt="Logo Image">
              </div>
            </div>
          </div>
          <div class="row d-none d-md-flex">
            <div class="col-6">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-auto mt-5 fadeIn2 fadeInBottom">
                <img class="avatar-img" src="../../assets/img/logos/small-logos/logo-atlassian.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-6 mt-6">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 mx-auto mt-n3 fadeIn3 fadeInBottom">
                <img class="avatar-img" src="../../assets/img/logos/small-logos/logo-weave.svg" alt="Logo Image">
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