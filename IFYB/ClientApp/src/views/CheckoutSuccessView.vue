<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :icon="true" :title="$t('checkout.successTitle')" :subTitle="$t('checkout.successSubTitle')" :buttonText="$t('checkout.backToHome')" @onClickBtn="$router.push('/')">
          <template v-slot:icon>
            <i :class="`ni ni-check-bold opacity-10 mt-2`"></i>
          </template>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import CarouselItem from '../components/CarouselItem.vue';

export default {
  name: 'CheckoutSuccessView',
  components: { CarouselItem },
}
</script>
