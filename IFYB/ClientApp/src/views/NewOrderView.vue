<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item v-if="page" class="full-height" :class="{'active': page === 'data'}" width="col-lg-10 col-12" icon="spaceship" :title="$t('order.orderData')" :subTitle="$t('order.orderDataDes')">
          <new-order-form @toSuccessPage="page = 'success'"></new-order-form>
        </carousel-item>
        <carousel-item :class="{'active': page === 'success'}" icon="send" :title="$t('order.successfulOrder')" :subTitle="$t('order.successfulOrderDes')" :buttonText="$t('order.backToHome')" @onClickBtn="$router.push('/')"></carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import NewOrderForm from '../components/NewOrderForm.vue';
import { useUserAuthentication } from "../store";
import router from '../router';

export default {
  name: 'NewOrderView',
  components: { CarouselItem, NewOrderForm },
  setup() {
    const page = ref(null);
    const { jwt } = useUserAuthentication();

    if(jwt.value) {
      page.value = 'data';
    } else {
      router.push('/authentication');
    }

    return { page }
  }
}
</script>

<style>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/bg2.webp');
  object-fit: cover;
}
.full-height{
  overflow: auto;
  height: 100vh;
}
</style>