<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :icon="true" :title="$t('checkout.paidTitle')" :subTitle="$t('checkout.paidSubTitle')" :buttonText="$t('checkout.backToHome')" @onClickBtn="$router.push('/')">
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
  name: 'CheckoutPaidView',
  components: { CarouselItem },
}
</script>