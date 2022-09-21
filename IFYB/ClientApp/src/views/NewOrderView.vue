<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item v-if="page === 'data'" class="full-height" width="col-lg-10 col-12" :icon="true" :title="$t('order.orderData')" :subTitle="$t('order.orderDataDes')">
          <template v-slot:icon>
            <i :class="`ni ni-spaceship opacity-10 mt-2`"></i>
          </template>
          <template v-slot:content>
            <new-order-form v-if="loadedTinymce" @toSuccessPage="page = 'success'"></new-order-form>
          </template>
        </carousel-item>
        <carousel-item v-if="page === 'success'" :icon="true" :title="$t('order.successfulOrder')" :subTitle="$t('order.successfulOrderDes')" :buttonText="$t('order.backToHome')" @onClickBtn="$router.push('/')">
          <template v-slot:icon>
            <i :class="`ni ni-send opacity-10 mt-2`"></i>
          </template>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import NewOrderForm from '../components/NewOrderForm.vue';
import { useScripts } from '@/store';

export default {
  name: 'NewOrderView',
  components: { CarouselItem, NewOrderForm },
  setup() {
    const page = ref('data');
    const { loadedTinymce } = useScripts();

    return { page, loadedTinymce }
  }
}
</script>
