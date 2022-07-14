<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height active" width="col-lg-9 col-md-11" icon="cart" :title="($t('checkout.order') + ' ' + order.number)">
          <p v-if="loading">{{ $t('checkout.loading') }}</p>
          <div v-if="order">
            <p>{{ $t('checkout.pay') }}</p>
            <form :action="`/api/pay/${route.params.token}`" method="post">
                <a class="btn btn bg-gradient-primary btn-round" @click="pay">Pay</a>
            </form>
          </div>
          <p v-if="!order && !loading">{{ $t('checkout.checkoutLink') }}</p>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import { usePayment } from '@/store'
import CarouselItem from '../components/CarouselItem.vue';

export default {
  components: { CarouselItem },
  setup() {
    const loading = ref(true);
    const order = ref(null);
    const route = useRoute();
    const payment = usePayment();
    fetch(`/api/pay/${route.params.token}`).then(resp => {
        resp.json().then(data => {
            order.value = data;
            loading.value = false;
        });
    });
    function pay() {
        fetch(`/api/pay/${route.params.token}`, { method: 'post' }).then(resp => {
            payment.setPaymentToken(route.params.token)
            resp.json().then(data => {
                window.location.href = data.url
            });
        });
    }
    return { loading, order, route, pay };
  }
}
</script>