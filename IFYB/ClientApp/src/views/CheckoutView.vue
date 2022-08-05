<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner" v-if="order">
        <carousel-item class="active" width="col-lg-10 col-12" icon="cart" :title="($t('checkout.order') + ' #' + order.number)" :progress="progress">
          <p v-if="loading">{{ $t('checkout.loading') }}</p>
          <div>
            <p>{{ $t('checkout.payDescription') }}</p>
            <div class="d-flex justify-content-center">
              <one-click-btn v-model:active="activeBtn" :text="`${$t('checkout.pay')} $${order.usdPrice}`" class="bg-gradient-primary mx-2" @click="pay(false)"></one-click-btn>
              <one-click-btn v-model:active="activeBtn" :text="`${$t('checkout.pay')} €${order.eurPrice}`" class="bg-gradient-primary mx-2" @click="pay(true)"></one-click-btn>
            </div>
            <p>{{$t('pricing.excludeVat')}}</p>
          </div>
          <p v-if="!order && !loading">{{ $t('checkout.checkoutLink') }}</p>
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
    const loading = ref(true);
    const order = ref(null);
    const route = useRoute();
    const payment = usePayment();
    const progress = ref(0);
    const activeBtn = ref(true);

    fetch(`/api/pay/${route.params.token}`).then(resp => {
      if (resp.status == 200) {
        resp.json().then(data => {
          order.value = data;
          loading.value = false;
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
    return { loading, order, route, progress, activeBtn, pay };
  }
}
</script>