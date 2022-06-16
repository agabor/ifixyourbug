<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height" :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders" @openOrder="openOrder"></order-list>
        </carousel-item>
        <order-viewer class="full-height" v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" :order="selectedOrder" @back="closeSelectedOrder"></order-viewer>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import OrderList from '../components/OrderList.vue';
import OrderViewer from '../components/OrderViewer.vue';
import { useServerError, useAuthentication } from "../store";
import router from '../router'

export default {
  name: 'AdminView',
  components: { CarouselItem, OrderList, OrderViewer },
  setup() {
    const { setServerError } = useServerError();
    const { authenticationPage, setAuthenticationPage } = useAuthentication();
    const page = ref('');
    const orders = ref([]);
    const selectedOrder = ref(null);

    if(!authenticationPage.value) {
      setAuthenticationPage('/admin');
      router.push('/admin-authentication');
    } else {
      setAuthenticationPage(null);
      page.value = 'orders';
      setOrders();
    }

    async function setOrders() {
      let response = await fetch('/api/admin/orders', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${localStorage.getItem('jwt')}`,
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

    return { page, orders, selectedOrder, openOrder, closeSelectedOrder }
  }
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>