<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height active" width="col-12">
          <admin-order-list :orders="orders" @openOrder="openOrder"></admin-order-list>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import AdminOrderList from '../components/AdminOrderList.vue';
import { useServerError, useAdminAuthentication } from "../store";
import router from '@/router';
import { event } from 'vue-gtag';

export default {
  name: 'AdminView',
  components: { CarouselItem, AdminOrderList },
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { get } = useAdminAuthentication();
    const orders = ref([]);
    const clients = ref([]);

    setClients();
    setOrders();

    async function setClients() {
      let response = await get('/api/admin/clients');
      if(response.status == 200) {
        resetServerError();
        clients.value = await response.json();
      } else {
        setServerError(response.statusText);
      }       
    }

    async function setOrders() {
      let response = await get('/api/admin/orders');
      if(response.status == 200) {
        resetServerError();
        let data = await response.json();
        orders.value = data.map(order => ({...order, ...clients.value.find(client => client.id === order.clientId)}));
      } else {
        setServerError(response.statusText);
      }       
    }

    function openOrder(order) {
      event('admin-selected-order', { 'value': order.number });
      router.push(`/admin/${order.number}`)
    }

    return { orders, openOrder  }
  }
}
</script>

<style scoped>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/bg2.webp');
  object-fit: cover;
}
.carousel-inner {
  height: 100%;
  overflow: auto;
}
</style>