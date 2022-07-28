<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="active" width="col-lg-10 col-12">
          <order-list :orders="orders" @openOrder="openOrder"></order-list>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import OrderList from '../components/OrderList.vue';
import { useServerError, useUserAuthentication } from "../store";
import router from '../router'

export default {
  name: 'OrdersView',
  components: { CarouselItem, OrderList },
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { get } = useUserAuthentication();
    const orders = ref([]);

    setOrders();

    async function setOrders() {
      let orderResponse = await get('/api/orders');
      if(orderResponse.status == 200) {
        resetServerError();
        orders.value = await orderResponse.json();
      } else {
        setServerError(orderResponse.statusText);
      }
    }

    function openOrder(order) {
      router.push(`/my-orders/${order.number.substring(1)}`)
    }

    return { orders, openOrder }
  }
}
</script>

<style scoped>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/bg3.webp');
  object-fit: cover;
}
.carousel-inner {
  height: 100%;
  overflow: auto;
}
</style>