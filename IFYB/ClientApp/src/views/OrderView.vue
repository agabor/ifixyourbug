<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <order-viewer v-if="selectedOrder !== null" class="active" :order="selectedOrder" @back="closeSelectedOrder"></order-viewer>
        <order-messages v-if="selectedOrder !== null" class="active" :messages="messages" @submitMessage="submitMessage"></order-messages>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useRoute } from 'vue-router'
import { useServerError, useUserAuthentication } from "../store";
import OrderViewer from '../components/OrderViewer.vue';
import OrderMessages from '../components/OrderMessages.vue';
import router from '@/router';

export default {
  name: 'OrderView',
  components: { OrderViewer, OrderMessages },
  setup() {
    const { setServerError } = useServerError();
    const { get, post } = useUserAuthentication();
    const orders = ref([]);
    const messages = ref([]);
    const selectedOrder = ref(null);
    const route = useRoute();

    setServerError(null);
    setSelectedOrder();

    async function setSelectedOrder() {
      let orderResponse = await get(`/api/orders/by-number/${route.params.number}`);
      if(orderResponse.status == 200) {
        setServerError(null);
        selectedOrder.value = await orderResponse.json();
        setMessages();
      } else {
        setServerError(orderResponse.statusText);
      }
    }

    async function setMessages() {
      let response = await get(`/api/orders/${selectedOrder.value.id}`);
      let order = await response.json();
      messages.value = order.messages.reverse();
    }

    function closeSelectedOrder() {
      selectedOrder.value = null;
      messages.value = [];
      router.push('/my-orders');
    }

    async function submitMessage(message) {
      let response = await post(`/api/orders/${selectedOrder.value.id}`, { text: message });
      let newMessage = await response.json();
      messages.value.unshift(newMessage);
    }

    return { orders, messages, selectedOrder, closeSelectedOrder, submitMessage }
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