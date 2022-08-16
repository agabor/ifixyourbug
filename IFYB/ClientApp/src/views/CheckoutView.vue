<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner" v-if="order || waitForOrder">
        <carousel-item class="active" width="col-lg-10 col-12" icon="cart" :title="($t('checkout.order') + (order ? ' #' + order.number : ''))" :progress="progress">
          <div class="d-flex justify-content-center align-items-center mb-3" v-if="waitForOrder">
            <p class="mb-0 me-2">{{ $t('checkout.loading') }}</p>
            <div class="spinner-border text-primary spinner-border-sm pl-3" role="status">
              <span class="sr-only"></span>
            </div>
          </div>
          <div>
            <p v-if="!waitForOrder">{{ $t('checkout.payDescription') }}</p>
            <div class="d-flex justify-content-center" v-if="order">
              <one-click-btn v-model:active="activeBtn" :text="`${$t('checkout.pay')} $${parseFloat(order.usdPrice).toFixed(2)}`" class="bg-gradient-primary mx-2" @click="pay(false)"></one-click-btn>
              <one-click-btn v-model:active="activeBtn" :text="`${$t('checkout.pay')} €${parseFloat(order.eurPrice).toFixed(2)}`" class="bg-gradient-primary mx-2" @click="pay(true)"></one-click-btn>
            </div>
            <div class="d-flex justify-content-center" v-else>
              <one-click-btn :text="`${$t('checkout.pay')} $`" class="bg-gradient-primary mx-2" disabled></one-click-btn>
              <one-click-btn :text="`${$t('checkout.pay')} €`" class="bg-gradient-primary mx-2" disabled></one-click-btn>
            </div>
            <p>{{$t('pricing.excludeVat')}}</p>
          </div>
          <p v-if="!order && !waitForOrder">{{ $t('checkout.checkoutLink') }}</p>
        </carousel-item>
      </div>
      <div class="carousel-inner" v-else>
        <carousel-item class="active" width="col-12" icon="cart" :title="$t('checkout.notfound')">
          <a class="btn btn bg-gradient-primary btn-round mx-2" @click="$router.go(-1)">{{ $t('checkout.back') }}</a>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { usePayment } from '@/store';
import CarouselItem from '../components/CarouselItem.vue';
import OneClickBtn from '@/components/OneClickBtn.vue';

export default {
  components: { CarouselItem, OneClickBtn },
  setup() {
    const order = ref(null);
    const waitForOrder = ref(true);
    const route = useRoute();
    const payment = usePayment();
    const progress = ref(0);
    const activeBtn = ref(true);

    addEventListener('pageshow', () => {
      progress.value = null;
      activeBtn.value = true;
    });

    fetch(`/api/pay/${route.params.token}`).then(resp => {
      if (resp.status == 200) {
        resp.json().then(data => {
          order.value = data;
          waitForOrder.value = false;
        });
      }
    });    

    function pay(isEur) {
      progress.value = 30;
      fetch(`/api/pay/${route.params.token}/${isEur}`, { method: 'post' }).then(resp => {
        payment.setPaymentToken(route.params.token)
        resp.json().then(data => {
          window.location.href = data.url
        });
        progress.value = 100;
      });
    }
    return { order, waitForOrder, route, progress, activeBtn, pay };
  }
}
</script>