<template>
<span class="bg"></span>
<span class="mask bg-gradient-dark opacity-4"></span>
  <section class="pt-7 pb-md-3">
    <div class="container">
      <div class="col-lg-8 col-md-10 mx-auto">
        <div class="card">
          <div class="card-header bg-gradient-primary p-5 position-relative">
            <h3 class="text-white mb-0">{{ $t('design.title') }}</h3>
            <p class="text-white opacity-8 mb-4">{{ $t('design.lastModified', { date: 'July 22, 2022' }) }}</p>
          </div>
          <div class="card-body p-sm-5 pt-0">
            <h4 class="my-4">{{ $t('design.images') }}</h4>
            <a href="https://unsplash.com/photos/Lks7vei-eAg" target="_blank">https://unsplash.com/photos/Lks7vei-eAg</a><br />
            <a href="https://unsplash.com/photos/DuHKoV44prg" target="_blank">https://unsplash.com/photos/DuHKoV44prg</a><br />
            <a href="https://unsplash.com/photos/cBUTPbTeDB0" target="_blank">https://unsplash.com/photos/cBUTPbTeDB0</a><br />
            <a href="https://unsplash.com/photos/Q1p7bh3SHj8" target="_blank">https://unsplash.com/photos/Q1p7bh3SHj8</a>
            <h4 class="my-4">{{ $t('design.webDesign') }}</h4>
            <a href="https://demos.creative-tim.com/now-ui-design-system-pro/pages/case-study.html" target="_blank">https://demos.creative-tim.com/now-ui-design-system-pro/pages/case-study.html</a>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>





<style scoped>
.bg {
  position: absolute;
  background-size: cover;
  background-position: center center;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-image: url('../assets/img/bg3.webp');
  object-fit: cover;
}
</style>