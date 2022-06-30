<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height" :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders" @openOrder="openOrder"></order-list>
        </carousel-item>
        <admin-order-viewer v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" :order="selectedOrder" @back="closeSelectedOrder" @acceptOrder="acceptOrder" @rejectOrder="rejectOrder" @completedOrder="completedOrder" @refundableOrder="refundableOrder"></admin-order-viewer>
        <order-messages v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" :messages="messages" @submitMessage="submitMessage"></order-messages>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import OrderList from '../components/OrderList.vue';
import AdminOrderViewer from '../components/AdminOrderViewer.vue';
import OrderMessages from '../components/OrderMessages.vue';
import { useServerError, useAdminAuthentication } from "../store";

export default {
  name: 'AdminView',
  components: { CarouselItem, OrderList, AdminOrderViewer, OrderMessages },
  setup() {
    const { setServerError } = useServerError();
    const { get, post } = useAdminAuthentication();
    const page = ref('');
    const orders = ref([]);
    const messages = ref([]);
    const selectedOrder = ref(null);

    page.value = 'orders';
    setOrders();

    async function setOrders() {
      let response = await get('/api/admin/orders');
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
      setMessages();
    }

    async function setMessages() {
      let response = await get(`/api/admin/orders/${selectedOrder.value.id}`);
      if(response.status == 200) {
        setServerError(null);
        let order = await response.json();
        messages.value = order.messages.reverse();
      } else {
        setServerError(response.statusText);
      }
    }

    function closeSelectedOrder() {
      selectedOrder.value = null;
      messages.value = [];
      page.value = 'orders';
    }

    async function submitMessage(message) {
      let response = await post(`/api/admin/orders/${selectedOrder.value.id}`, {
          clientId: localStorage.getItem('adminId'),
          text: message
        });
      if(response.status == 200) {
        setServerError(null);
        let newMessage = await response.json();
        messages.value.unshift(newMessage);
      } else {
        setServerError(response.statusText);
      }
    }

    async function acceptOrder() {
      let response = await post(`/api/admin/orders/${selectedOrder.value.id}/state`, 1);
      if(response.status == 200) {
        setServerError(null);
        selectedOrder.value.state = 1;
      } else {
        setServerError(response.statusText);
      }
    }
    
    async function rejectOrder() {
      let response = await post(`/api/admin/orders/${selectedOrder.value.id}/state`, 2);
      if(response.status == 200) {
        setServerError(null);
        selectedOrder.value.state = 2;
      } else {
        setServerError(response.statusText);
      }
    }
    
    async function completedOrder() {
      let response = await post(`/api/admin/orders/${selectedOrder.value.id}/state`, 4);
      if(response.status == 200) {
        setServerError(null);
        selectedOrder.value.state = 4;
      } else {
        setServerError(response.statusText);
      }
    }
    
    async function refundableOrder() {
      let response = await post(`/api/admin/orders/${selectedOrder.value.id}/state`, 5);
      if(response.status == 200) {
        setServerError(null);
        selectedOrder.value.state = 5;
      } else {
        setServerError(response.statusText);
      }
    }

    return { page, orders, selectedOrder, messages, openOrder, closeSelectedOrder, submitMessage, acceptOrder, rejectOrder, completedOrder, refundableOrder }
  }
}
</script>

<style scoped>
td > span {
  white-space: pre-line;
}
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