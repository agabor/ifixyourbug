<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <confirmation-modal v-if="showModal" v-model="stateMessage" :title="$t('confirm.stateChangeTitle')" :description="$t('confirm.stateChangeDescription')" :showError="showError" @confirm="changeOrderState" @cancel="cancelChangeOrderState"></confirmation-modal>
      <div class="carousel-inner">
        <carousel-item class="full-height" :class="{'active': page === 'orders'}" width="col-12">
          <order-list :orders="orders" @openOrder="openOrder"></order-list>
        </carousel-item>
        <admin-order-viewer v-if="selectedOrder !== null" :class="{'active': page === 'selectedOrder'}" :order="selectedOrder" @back="closeSelectedOrder" @changeOrderState="tryChangeOrderState"></admin-order-viewer>
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
import ConfirmationModal from '@/components/ConfirmationModal.vue';
import { useServerError, useAdminAuthentication } from "../store";

export default {
  name: 'AdminView',
  components: { CarouselItem, OrderList, AdminOrderViewer, OrderMessages, ConfirmationModal },
  setup() {
    const { setServerError } = useServerError();
    const { get, post } = useAdminAuthentication();
    const page = ref('');
    const orders = ref([]);
    const messages = ref([]);
    const selectedOrder = ref(null);
    const nextState = ref(null);
    const stateMessage = ref(null);
    const showModal = ref(false);
    const showError = ref(false);

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

    function tryChangeOrderState(state, b) {
      nextState.value = state;
      if(b) {
        stateMessage.value = '';
      } else {
        stateMessage.value = null;
      }
      showModal.value = true;
    }

    async function changeOrderState() {
      console.log('stateMessage', stateMessage.value);
      let response;
      if(stateMessage.value !== null) {
        if(stateMessage.value !== '') {
          showError.value = false;
          response = await post(`/api/admin/orders/${selectedOrder.value.id}/state-with-msg`, { state: nextState.value, message: { clientId: localStorage.getItem('adminId'), text: stateMessage.value }});
          setMessages();
        } else {
          showError.value = true;
        }
      } else {
        response = await post(`/api/admin/orders/${selectedOrder.value.id}/state`, nextState.value);
      }
      if(response.status == 200) {
        setServerError(null);
        selectedOrder.value.state = nextState.value;
        nextState.value = null;
      } else {
        setServerError(response.statusText);
      }
      showModal.value = false;
    }

    function cancelChangeOrderState() {
      nextState.value = null;
      showModal.value = false;
    }

    return { page, orders, selectedOrder, messages, showModal, stateMessage, showError, openOrder, closeSelectedOrder, submitMessage, tryChangeOrderState, changeOrderState, cancelChangeOrderState }
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