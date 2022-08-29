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
import { useRoute } from 'vue-router';
import { useServerError, useUserAuthentication } from "../store";
import OrderViewer from '../components/OrderViewer.vue';
import OrderMessages from '../components/OrderMessages.vue';
import router from '@/router';
import { event } from 'vue-gtag';

export default {
  name: 'OrderView',
  components: { OrderViewer, OrderMessages },
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { get, post } = useUserAuthentication();
    const orders = ref([]);
    const messages = ref([]);
    const selectedOrder = ref(null);
    const route = useRoute();

    setSelectedOrder();

    async function setSelectedOrder() {
      let response = await get(`/api/orders/${route.params.number}`);
      event('set-selected-order', { 'value': response.status });
      if(response.status == 200) {
        resetServerError();
        selectedOrder.value = await response.json();
        setMessages();
      } else {
        setServerError(response.statusText);
      }
    }

    async function setMessages() {
      let response = await get(`/api/orders/${selectedOrder.value.number}`);
      if(response.status == 200) {
        resetServerError();
        let order = await response.json();
        messages.value = order.messages.reverse();
      } else {
        setServerError(response.statusText);
      }
    }

    function closeSelectedOrder() {
      event('close-selected-order');
      selectedOrder.value = null;
      messages.value = [];
      router.push('/my-orders');
    }

    async function submitMessage(message) {
      let response = await post(`/api/orders/${selectedOrder.value.number}`, { text: message });
      event('submit-message', { 'value': response.status });
      if(response.status == 200) {
        resetServerError();
        let newMessage = await response.json();
        messages.value.unshift(newMessage);
      } else {
        setServerError(response.statusText);
      }
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
  background-image: url('../assets/img/bg2.webp');
  object-fit: cover;
}
.carousel-inner {
  height: 100%;
  overflow: auto;
}
</style>