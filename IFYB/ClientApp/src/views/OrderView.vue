<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner" v-if="selectedOrder !== null">
          <order-viewer class="active" :modelValue="selectedOrder"></order-viewer>
          <order-messages class="active" :messages="messages" @submitMessage="submitMessage"></order-messages>
        </div>
      </div>
      <footer-component></footer-component>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { useTinyMce } from "../store";
import { useUserAuthentication } from "../store/authentication";
import { setServerError, resetServerError } from "../store/serverError";
import OrderViewer from '../components/OrderViewer.vue';
import OrderMessages from '../components/OrderMessages.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import router from '@/router';

export default {
  name: 'OrderView',
  components: { OrderViewer, OrderMessages, FooterComponent },
  setup() {
    const { get, post } = useUserAuthentication();
    const { loadTinymce } = useTinyMce();
    const messages = ref([]);
    const selectedOrder = ref(null);
    const route = useRoute();

    setSelectedOrder();

    async function setSelectedOrder() {
      let response = await get(`/api/orders/${route.params.number}`);
      if(response.status === 200) {
        resetServerError();
        selectedOrder.value = await response.json();
        if(selectedOrder.value.state === 7){
          loadTinymce();
        }
        setMessages();
      } else if(response.status === 404) {
        resetServerError();
        router.push('/not-found')
      } else {
        setServerError(response.statusText);
      }
    }

    async function setMessages() {
      let response = await get(`/api/orders/${selectedOrder.value.number}`);
      if(response.status === 200) {
        resetServerError();
        let order = await response.json();
        messages.value = order.messages.reverse();
      } else {
        setServerError(response.statusText);
      }
    }

    async function submitMessage(message) {
      let response = await post(`/api/orders/${selectedOrder.value.number}`, { text: message });
      if(response.status === 200) {
        resetServerError();
        let newMessage = await response.json();
        messages.value.unshift(newMessage);
      } else {
        setServerError(response.statusText);
      }
    }

    return { messages, selectedOrder, submitMessage }
  }
}
</script>