<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height" width="col-12">
          <template v-slot:content>
            <admin-order-list v-if="orders.length > 0" :orders="orders" @openOrder="openOrder"></admin-order-list>
            <p class="m-2" v-else>{{ $t('errors.noResult') }}</p>
          </template>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import AdminOrderList from '../components/AdminOrderList.vue';
import { useAdminAuthentication } from "../store/admin";
import router from '@/router';

export default {
  name: 'AdminView',
  components: { CarouselItem, AdminOrderList },
  setup() {
    const { get } = useAdminAuthentication();
    const orders = ref([]);
    const clients = ref([]);

    setClients();
    setOrders();

    async function setClients() {
      let response = await get('/api/admin/clients');
      if(response.status === 200) {
        clients.value = await response.json();
      }    
    }

    async function setOrders() {
      let response = await get('/api/admin/orders');
      if(response.status === 200) {
        let data = await response.json();
        orders.value = data.map(order => ({...order, ...clients.value.find(client => client.id === order.clientId)}));
      }   
    }

    function openOrder(order) {
      router.push(`/admin/${order.number}`)
    }

    return { orders, openOrder  }
  }
}
</script>