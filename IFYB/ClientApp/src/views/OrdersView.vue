<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication v-model:jwt="jwt" :checkName="true" @toTargetPage="setOrders"></authentication>
        <carousel-item :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders" @openOrder="openOrder"></order-list>
        </carousel-item>
        <carousel-item class="full-height" v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" width="col-12">
          <order-viewer :order="selectedOrder" @back="closeSelectedOrder"></order-viewer>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import Authentication from '../components/Authentication.vue';
import OrderList from '../components/OrderList.vue';
import OrderViewer from '../components/OrderViewer.vue';

export default {
  name: 'OrdersView',
  components: { CarouselItem, Authentication, OrderList, OrderViewer },
  setup() {
    const page = ref('email');
    const jwt = ref('');
    const orders = ref([]);
    const selectedOrder = ref(null);

    async function setOrders() {
      let orderResponse = await fetch('/api/orders', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt.value}`
        }
      })
      orders.value = await orderResponse.json();
      page.value = 'orders';
    }

    function openOrder(order) {
      selectedOrder.value = order;
      page.value = 'selectedOrder';
    }

    function closeSelectedOrder() {
      selectedOrder.value = null;
      page.value = 'orders';
    }

    return { page, jwt, orders, selectedOrder, setOrders, openOrder, closeSelectedOrder }
  }
}
</script>

<style>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/pricing3.jpg');
}
.full-height{
  overflow: auto;
  height: 100vh;
}

</style>