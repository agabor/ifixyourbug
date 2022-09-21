<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height" width="col-lg-10 col-12">
          <template v-slot:content>
            <order-list :orders="orders" @openOrder="openOrder"></order-list>
          </template>
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
import router from '../router';

export default {
  name: 'OrdersView',
  components: { CarouselItem, OrderList },
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { get } = useUserAuthentication();
    const orders = ref([]);

    async function setOrders() {
      let orderResponse = await get('/api/orders');
      if(orderResponse.status == 200) {
        resetServerError();
        orders.value = await orderResponse.json();
      } else {
        setServerError(orderResponse.statusText);
      }
    }
    setOrders();

    function openOrder(order) {
      router.push(`/my-orders/${order.number}`)
    }

    return { orders, openOrder }
  }
}
</script>