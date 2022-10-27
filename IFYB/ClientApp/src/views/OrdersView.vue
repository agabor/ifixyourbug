<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner">
          <carousel-item width="col-lg-10 col-12">
            <template v-slot:content>
              <button type="button" class="btn bg-gradient-primary mt-4" @click="$router.push('/new-order')">
                {{ $t('orderList.addNewOrder') }}
              </button>
              <order-list v-if="orders.length > 0" :orders="orders" @openOrder="openOrder"></order-list>
              <p class="m-2" v-else>{{ $t('errors.noResult') }}</p>
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
import OrderList from '../components/OrderList.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import { useClientAuthentication } from "../store/client";
import router from '../router';

export default {
  name: 'OrdersView',
  components: { CarouselItem, OrderList, FooterComponent },
  setup() {
    const { get } = useClientAuthentication();
    const orders = ref([]);

    async function setOrders() {
      let orderResponse = await get('/api/orders');
      if(orderResponse.status === 200) {
        orders.value = await orderResponse.json();
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