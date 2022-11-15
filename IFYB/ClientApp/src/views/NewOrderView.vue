<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner">
          <carousel-item v-if="page === 'data'" width="col-lg-10 col-12" :icon="true" :title="selectedFlag === 1 ? $t('order.sendBug') : $t('order.sendCode')" :subTitle="selectedFlag === 1 ? $t('order.sendBugDes') : $t('order.sendCodeDes')">
            <template v-slot:icon>
              <i :class="`ni ni-spaceship opacity-10 mt-2`"></i>
            </template>
            <template v-slot:content>
              <new-order-form v-if="loadedTinymce" @toSuccessPage="page = 'success'" v-model:flag="selectedFlag"></new-order-form>
            </template>
          </carousel-item>
          <carousel-item v-if="page === 'success'" :icon="true" :title="$t('order.successfulOrder')" :subTitle="$t('order.successfulOrderDes')" :buttonText="$t('order.backToHome')" @onClickBtn="$router.push('/')">
            <template v-slot:icon>
              <i :class="`ni ni-send opacity-10 mt-2`"></i>
            </template>
          </carousel-item>
        </div>
      </div>
      <footer-component></footer-component>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import NewOrderForm from '../components/NewOrderForm.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import { useTinyMce } from '@/store';

export default {
  name: 'NewOrderView',
  components: { CarouselItem, NewOrderForm, FooterComponent },
  setup() {
    const page = ref('data');
    const { loadedTinymce } = useTinyMce();
    const selectedFlag = ref(1);

    return { page, loadedTinymce, selectedFlag }
  }
}
</script>
