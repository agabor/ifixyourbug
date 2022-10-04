<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header justify-content-center">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner row mt-6">
          <div class="col-lg-10 col-12 my-4 mx-auto">
            <div class="card">
              <div class="card-header bg-gradient-primary p-5 position-relative">
                <h3 class="text-white mb-0">{{ $t('design.title') }}</h3>
                <p class="text-white opacity-8 mb-0">{{ $t('design.lastModified', { date: 'July 22, 2022' }) }}</p>
              </div>
              <div class="card-body p-sm-5 pt-0">
                <h4 class="my-4">{{ $t('design.images') }}</h4>
                <a class="text-bolder pe-2" href="https://unsplash.com/@charlesdeluvio" @click="$event('navigate', {'value': 'https://unsplash.com/@charlesdeluvio'})">@charlesdeluvio</a><a href="https://unsplash.com/photos/Lks7vei-eAg" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/Lks7vei-eAg'})">man using MacBook photo</a><br />
                <a class="text-bolder pe-2" href="https://unsplash.com/@ffstop" @click="$event('navigate', {'value': 'https://unsplash.com/@ffstop'})">@ffstop</a><a href="https://unsplash.com/photos/DuHKoV44prg" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/DuHKoV44prg'})">black computer keyboard photo</a><br />
                <a class="text-bolder pe-2" href="https://unsplash.com/@lazizli" @click="$event('navigate', {'value': 'https://unsplash.com/@lazizli'})">@lazizli</a><a href="https://unsplash.com/photos/cBUTPbTeDB0" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/cBUTPbTeDB0'})">group of people sitting on chair in front of table with macbook photo</a><br />
                <a class="text-bolder pe-2" href="https://unsplash.com/@nasa" @click="$event('navigate', {'value': 'https://unsplash.com/@nasa'})">@nasa</a><a href="https://unsplash.com/photos/Q1p7bh3SHj8" target="_blank" @click="$event('navigate', {'value': 'https://unsplash.com/photos/Q1p7bh3SHj8'})">photo of outer space photo</a><br />
                <a class="text-bolder pe-2" href="https://github.com/dotnet/brand/tree/main/dotnet-bot-illustrations" @click="$event('navigate', {'value': 'https://github.com/dotnet/brand/tree/main/dotnet-bot-illustrations'})">@dotnet-bot-illustrations</a><a href="https://github.com/dotnet/brand/blob/main/dotnet-bot-illustrations/dotnet-bot/dotnet-bot_asp.net.svg" target="_blank" @click="$event('navigate', {'value': 'https://github.com/dotnet/brand/blob/main/dotnet-bot-illustrations/dotnet-bot/dotnet-bot_asp.net.svg'})">dotnet-bot asp.net</a>
                <h4 class="my-4">{{ $t('design.webDesign') }}</h4>
                <a href="https://www.creative-tim.com/product/now-ui-design-system-pro" target="_blank" @click="$event('navigate', {'value': 'https://www.creative-tim.com/product/now-ui-design-system-pro'})">Creative Tim</a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <footer-component></footer-component>
    </div>
  </section>
</template>

<script>
import FooterComponent from '../components/homeComponents/FooterComponent.vue';

export default {
  name: 'CreditsView',
  components: { FooterComponent },
}
</script>