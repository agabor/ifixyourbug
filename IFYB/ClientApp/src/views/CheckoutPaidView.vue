<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header mt-6">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner">
          <carousel-item :icon="true" :title="$t('checkout.paidTitle')" :subTitle="$t('checkout.paidSubTitle')" :buttonText="$t('checkout.backToHome')" @onClickBtn="$router.push('/')">
            <template v-slot:icon>
              <i :class="`ni ni-check-bold opacity-10 mt-2`"></i>
            </template>
          </carousel-item>
        </div>
      </div>
      <footer-component></footer-component>
    </div>
  </section>
</template>

<script>
import CarouselItem from '../components/CarouselItem.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';

export default {
  name: 'CheckoutPaidView',
  components: { CarouselItem, FooterComponent },
}
</script>