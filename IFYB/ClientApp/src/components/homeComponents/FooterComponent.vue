<template>
  <footer class="footer py-5 bg-dark position-relative overflow-hidden">
    <div class="container position-relative">
      <div class="row">
        <div class="col-lg-4 me-auto my-auto text-lg-start text-center">
          <h6 class="text-white font-weight-bolder text-uppercase mb-2">{{ $t('footer.title') }}</h6>
          <p class="text-white">{{ $t('footer.copyright') }}</p>
        </div>
        <div class="col-lg-2 col-sm-4 col-12 ms-md-auto text-lg-start text-center mb-sm-0 mb-4">
          <div>
            <h6 class="text-white text-sm mb-1">{{ $t('footer.company') }}</h6>
            <ul class="flex-column ms-lg-n3 nav">
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">{{ $t('footer.aboutUs') }}</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">{{ $t('footer.careers') }}</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">{{ $t('footer.press') }}</a>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-lg-2 col-sm-4 col-12 text-lg-start text-center mb-sm-0 mb-4">
          <div>
            <h6 class="text-white text-sm mb-1">{{ $t('footer.pages') }}</h6>
            <ul class="flex-column ms-lg-n3 nav">
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="/">{{ $t('footer.home') }}</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="/order">{{ $t('footer.order') }}</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="/faq">{{ $t('footer.faq') }}</a>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-lg-2 col-sm-4 col-12 text-lg-start text-center mb-sm-0 mb-4">
          <div>
            <h6 class="text-white text-sm mb-1">{{ $t('footer.products') }}</h6>
            <ul class="flex-column ms-lg-n3 nav">
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">{{ $t('footer.free') }}</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">{{ $t('footer.pro') }}</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">{{ $t('footer.latest') }}</a>
              </li>
            </ul>
          </div>
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