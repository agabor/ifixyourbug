<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner" v-if="order || waitForOrder">
          <carousel-item width="col-lg-10 col-12" :icon="true" :title="($t('checkout.order') + (order ? ' #' + order.number : ''))" :progress="progress">
            <template v-slot:icon>
              <i :class="`ni ni-cart opacity-10 mt-2`"></i>
            </template>
            <template v-slot:content>
              <div class="d-flex justify-content-center align-items-center mb-3" v-if="waitForOrder">
                <p class="mb-0 me-2">{{ $t('checkout.loading') }}</p>
                <div class="spinner-border text-primary spinner-border-sm pl-3" role="status">
                  <span class="sr-only"></span>
                </div>
              </div>
              <div>
                <p v-if="!waitForOrder">{{ $t('checkout.payDescription') }}</p>
                <div class="d-flex justify-content-center" v-if="order">
                  <one-click-btn :active="progress === 0" :text="`${$t('checkout.pay')} ${ order.currency === 'eur' ? '€' : '$' }${parseFloat(order.price).toFixed(2)}`" class="bg-gradient-primary mx-2" @click="pay()"></one-click-btn>
                </div>
                <div class="d-flex justify-content-center" v-else>
                  <one-click-btn :text="`${$t('checkout.pay')}`" class="bg-gradient-primary mx-2" disabled></one-click-btn>
                </div>
                <p>{{$t('pricing.excludeVat')}}</p>
              </div>
              <p v-if="!order && !waitForOrder">{{ $t('checkout.checkoutLink') }}</p>
              <div class="row mt-5 mb-1 mx-1">
                  <div class="col-md-5 me-auto my-auto ms-auto">
                    <img class="mw-100" src="@/assets/img/stripe-badge-grey.png" alt="stripe">
                  </div>
              </div>
            </template>
          </carousel-item>
        </div>
        <div class="carousel-inner" v-else>
          <carousel-item width="col-12" :icon="true" :title="$t('checkout.notfound')">
            <template v-slot:icon>
              <i :class="`ni ni-cart opacity-10 mt-2`"></i>
            </template>
            <template v-slot:content>
              <a class="btn btn bg-gradient-primary btn-round mx-2" @click="$router.go(-1)">{{ $t('checkout.back') }}</a>
              <div class="row mt-5 mb-1 mx-1">
                  <div class="col-md-5 me-auto my-auto ms-auto">
                    <img class="mw-100" src="@/assets/img/stripe-badge-grey.png" alt="stripe">
                  </div>
              </div>
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
import { useRoute } from 'vue-router';
import { usePayment } from "@/store/payment";
import { fetchGet, fetchPost } from '@/store/web';
import CarouselItem from '@/components/CarouselItem.vue';
import OneClickBtn from '@/components/OneClickBtn.vue';
import FooterComponent from '@/components/homeComponents/FooterComponent.vue';

export default {
  components: { CarouselItem, OneClickBtn, FooterComponent },
  setup() {
    const order = ref(null);
    const waitForOrder = ref(true);
    const route = useRoute();
    const payment = usePayment();
    const progress = ref(0);

    function resetState() {
      progress.value = 0;
      removeEventListener('pageshow', resetState);
    }

    fetchGet(`/api/pay/${route.params.token}`).then(resp => {
      if (resp.status === 200) {
        resp.json().then(data => {
          order.value = data;
          waitForOrder.value = false;
        });
      } else if(resp.status === 404) {
        waitForOrder.value = false;
      }
    });    

    function pay() {
      progress.value = 30;
      fetchPost(`/api/pay/${route.params.token}}`).then(resp => {
        payment.setPaymentToken(route.params.token)
        resp.json().then(data => {
          window.location.href = data.url
        });
        progress.value = 100;
        addEventListener('pageshow', resetState);
      });
    }
    return { order, waitForOrder, route, progress, pay };
  }
}
</script>