<template>
  <section class="my-md-6 mb-6">
    <div class="container">
      <div class="row text-center">
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-watch-time h3 text-info"></i>
            <h5 class="mt-2">{{ $t('features.quick') }}</h5>
            <p>{{ $t('features.quickDescription', { workdays }) }}</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-credit-card h3 text-info"></i>
            <h5 class="mt-2">{{ $t('features.fixedPrice') }}</h5>
            <p>{{ $t('features.fixedPriceDescription') }}</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-send h3 text-info"></i>
            <h5 class="mt-2">{{ $t('features.easy') }}</h5>
            <p>{{ $t('features.easyDescription') }}</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-check-bold h3 text-info"></i>
            <h5 class="mt-2">{{ $t('features.refundable') }}</h5>
            <p>{{ $t('features.refundableDescription', { workdays }) }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { useSettings } from '@/store';

export default {
  name: 'FeaturesComponent',
  setup() {
    const { workdays } = useSettings();
    return { workdays }
  }
}
</script>