<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <admin-authentication v-model:jwt="jwt" @toTargetPage="setOrders"></admin-authentication>
        <carousel-item :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders" @openOrder="openOrder"></order-list>
        </carousel-item>
        <carousel-item class="full-height" v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" width="col-12">
          <order-viewer :order="selectedOrder" :isAdmin="true" @back="closeSelectedOrder"></order-viewer>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import AdminAuthentication from '../components/AdminAuthentication.vue';
import OrderList from '../components/OrderList.vue';
import OrderViewer from '../components/OrderViewer.vue';
import { useServerError } from "../store";

export default {
  name: 'AdminView',
  components: { CarouselItem, AdminAuthentication, OrderList, OrderViewer },
  setup() {
    const { setServerError } = useServerError();
    const page = ref('email');
    const jwt = ref('');
    const orders = ref([]);
    const selectedOrder = ref(null);

    async function setOrders() {
      let response = await fetch('/api/admin/orders', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt.value}`,
          'Content-Type': 'application/json'
        }
      })
      if(response.status == 200) {
        setServerError(null);
        orders.value = await response.json();
        page.value = 'orders'; 
      } else {
        setServerError(response.statusText);
      }       
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

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>