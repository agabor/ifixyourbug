<template>
  <section>
    <div class="container">
      <div class="row">
        <div class="card py-5 px-4">
          <div class="row justify-content-center">
            <div class="row">
              <div class="col-lg-10 mx-auto text-center px-md-2 px-sm-5 mt-sm-0 mt-4">
                <h3>{{ $t('workWithUs.title') }}</h3>
                <p><small>{{ $t('workWithUs.subTitle') }}</small></p>
                <p class="text-justify text-lg">{{ $t('workWithUs.carouselDes1') }}</p>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 col-12 d-flex justify-content-md-end justify-content-center px-4 mt-5">
                <img class="opacity-7" height="150" src="../../assets/img/logos/dotnet-bot.svg">
              </div>
              <div class="col-md-6 col-12 d-flex justify-content-md-start justify-content-center px-4 mt-5">
                <img class="opacity-7" height="150" src="../../assets/img/logos/vuejs-bg.webp">
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
  name: 'WorkWithUs'
}
</script>