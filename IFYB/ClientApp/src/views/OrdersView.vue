<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication v-model:jwt="jwt" :checkName="true" @toTargetPage="setOrders"></authentication>
        <carousel-item :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders"></order-list>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import OrderList from '../components/OrderList.vue';
import Authentication from '../components/Authentication.vue';

export default {
  name: 'OrdersView',
  components: { CarouselItem, OrderList, Authentication },
  setup() {
    const page = ref('email');
    const jwt = ref('');
    const orders = ref([]);

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

    return { page, jwt, orders, setOrders }
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