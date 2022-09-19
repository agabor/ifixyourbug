<template>
<span class="bg"></span>
<span class="mask bg-gradient-dark opacity-4"></span>
  <section class="pt-7 pb-md-3">
    <div class="container">
      <div class="col-lg-10 col-12 mx-auto">
        <div class="card">
          <div class="card-header bg-gradient-primary p-5 position-relative">
            <h3 class="text-white mb-0">{{ $t('design.title') }}</h3>
            <p class="text-white opacity-8 mb-4">{{ $t('design.lastModified', { date: 'July 22, 2022' }) }}</p>
          </div>
          <div class="card-body p-sm-5 pt-0">
            <h4 class="my-4">{{ $t('design.images') }}</h4>
            <a class="text-bolder pe-2" href="https://unsplash.com/@charlesdeluvio" @click="$event('navigate', {'value': 'https://unsplash.com/@charlesdeluvio'})">@charlesdeluvio</a><a href="https://unsplash.com/photos/Lks7vei-eAg" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/Lks7vei-eAg'})">man using MacBook photo</a><br />
            <a class="text-bolder pe-2" href="https://unsplash.com/@ffstop" @click="$event('navigate', {'value': 'https://unsplash.com/@ffstop'})">@ffstop</a><a href="https://unsplash.com/photos/DuHKoV44prg" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/DuHKoV44prg'})">black computer keyboard photo</a><br />
            <a class="text-bolder pe-2" href="https://unsplash.com/@lazizli" @click="$event('navigate', {'value': 'https://unsplash.com/@lazizli'})">@lazizli</a><a href="https://unsplash.com/photos/cBUTPbTeDB0" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/cBUTPbTeDB0'})">group of people sitting on chair in front of table with macbook photo</a><br />
            <a class="text-bolder pe-2" href="https://unsplash.com/@nasa" @click="$event('navigate', {'value': 'https://unsplash.com/@nasa'})">@nasa</a><a href="https://unsplash.com/photos/Q1p7bh3SHj8" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/Q1p7bh3SHj8'})">photo of outer space photo</a>
            <h4 class="my-4">{{ $t('design.webDesign') }}</h4>
            <a href="https://www.creative-tim.com/product/now-ui-design-system-pro" target="_blank" @click="$event('navigate', {'value': 'https://www.creative-tim.com/product/now-ui-design-system-pro'})">Creative Tim</a>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'DesignView',
}
</script>

<style scoped>
.bg {
  position: absolute;
  background-size: cover;
  background-position: center center;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}
@media (min-width: 576px) {
  .bg {
    background-image: url('../assets/img/bg2.webp');
  }
}
@media (max-width: 576px) {
  .bg {
    background-image: url('../assets/mobile_img/bg2.webp');
  }
}
</style>