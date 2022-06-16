<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders" @openOrder="openOrder"></order-list>
        </carousel-item>
        <order-viewer v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" :order="selectedOrder" @back="closeSelectedOrder"></order-viewer>
        <order-messages v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" :messages="messages" @submitMessage="submitMessage"></order-messages>
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
import OrderMessages from '../components/OrderMessages.vue';

export default {
  name: 'OrdersView',
  components: { CarouselItem, OrderList, OrderViewer, OrderMessages },
  setup() {
    const { setServerError } = useServerError();
    const { authenticationPage, setAuthenticationPage } = useAuthentication();
    const page = ref('');
    const orders = ref([]);
    const messages = ref([]);
    const selectedOrder = ref(null);

    if(!authenticationPage.value) {
      setAuthenticationPage('/my-orders');
      router.push('/authentication');
    } else {
      setAuthenticationPage(null);
      page.value = 'orders';
      setOrders();
    }

    async function setOrders() {
      let orderResponse = await fetch('/api/orders', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${localStorage.getItem('jwt')}`
        }
      })
      if(orderResponse.status == 200) {
        setServerError(null);
        orders.value = await orderResponse.json();
        page.value = 'orders';
      } else {
        setServerError(orderResponse.statusText);
      }
    }

    function openOrder(order) {
      selectedOrder.value = order;
      page.value = 'selectedOrder';
      setMessages();
    }

    async function setMessages() {
      let response = await fetch(`/api/orders/${selectedOrder.value.id}`, {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${localStorage.getItem('jwt')}`
        }
      })
      let order = await response.json();
      messages.value = order.messages.reverse();
    }

    function closeSelectedOrder() {
      selectedOrder.value = null;
      page.value = 'orders';
    }

    async function submitMessage(message) {
      let response = await fetch(`/api/orders/${selectedOrder.value.id}`, {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${localStorage.getItem('jwt')}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          text: message
        })
      });
      let newMessage = await response.json();
      messages.value.unshift(newMessage);
    }

    return { page, orders, messages, selectedOrder, openOrder, closeSelectedOrder, submitMessage }
  }
}
</script>

<style scoped>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/pricing3.jpg');
}
.carousel-inner {
  height: 100%;
  overflow: auto;
}
</style>