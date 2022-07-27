<template>
  <section class="py-5">
    <div class="container">
      <div class="row text-center">
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-box-2 h3 text-primary"></i>
            <h5 class="mt-2">{{ $t('features.components') }}</h5>
            <p>{{ $t('features.componentsDes') }}</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-album-2 h3 text-primary"></i>
            <h5 class="mt-2">{{ $t('features.design') }}</h5>
            <p>{{ $t('features.designDes') }}</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-atom h3 text-primary"></i>
            <h5 class="mt-2">{{ $t('features.lessCode') }}</h5>
            <p>{{ $t('features.lessCodeDes') }}</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-collection h3 text-primary"></i>
            <h5 class="mt-2">{{ $t('features.fullyResponsive') }}</h5>
            <p>{{ $t('features.fullyResponsiveDes') }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'FeaturesComponent',
}
</script>
