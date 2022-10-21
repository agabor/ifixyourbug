<template>
  <footer class="p-5 bg-dark">
    <div class="row">
      <div class="col-lg-4 me-auto my-auto text-lg-start text-center">
        <h6 class="text-white font-weight-bolder text-uppercase mb-2">{{ $t('footer.title') }}</h6>
        <p class="text-white">{{ $t('footer.copyright') }}</p>
      </div>
      <div class="col-lg-2 col-sm-6 col-12 text-lg-start text-center mb-sm-0 mb-4">
        <div>
          <h6 class="text-white text-sm mb-1">{{ $t('footer.pages') }}</h6>
          <ul class="flex-column ms-lg-n3 nav">
            <li class="nav-item">
              <a class="nav-link text-white opacity-8 text-sm pb-0" href="/">{{ $t('footer.home') }}</a>
            </li>
            <li class="nav-item">
              <a class="nav-link text-white opacity-8 text-sm pb-0" href="/faq">{{ $t('footer.faq') }}</a>
            </li>
            <li class="nav-item">
              <a class="nav-link text-white opacity-8 text-sm pb-0" href="/credits">{{ $t('footer.credits') }}</a>
            </li>
          </ul>
        </div>
      </div>
      <div class="col-lg-2 col-sm-4 col-12 text-lg-start text-center mb-sm-0 mb-4">
        <div>
          <h6 class="text-white text-sm mb-1">{{ $t('footer.legal') }}</h6>
          <ul class="flex-column ms-lg-n3 nav">
            <li class="nav-item">
              <a class="nav-link text-white opacity-8 text-sm pb-0" href="/terms-and-conditions" target="_blank">{{ $t('footer.terms') }}</a>
            </li>
            <li class="nav-item">
              <a class="nav-link text-white opacity-8 text-sm pb-0" href="/privacy-policy" target="_blank">{{ $t('footer.privacyPolicy') }}</a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: 'FooterComponent',
}
</script>