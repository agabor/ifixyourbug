<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <confirmation-modal v-if="showModal" v-model="stateMessage" :title="$t('confirm.stateChangeTitle')" :description="$t('confirm.stateChangeDescription')" :showError="showError" @confirm="changeOrderState" @cancel="cancelChangeOrderState"></confirmation-modal>
      <div class="carousel-inner">
        <admin-order-viewer v-if="selectedOrder !== null" class="active" :order="selectedOrder" @back="closeSelectedOrder" @changeOrderState="tryChangeOrderState"></admin-order-viewer>
        <order-messages v-if="selectedOrder !== null" class="active" :messages="messages" @submitMessage="submitMessage"></order-messages>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { useAdminAuthentication } from "../store/admin";
import AdminOrderViewer from '../components/AdminOrderViewer.vue';
import OrderMessages from '../components/OrderMessages.vue';
import ConfirmationModal from '@/components/ConfirmationModal.vue';
import router from '@/router';

export default {
  name: 'AdminOrderView',
  components: { AdminOrderViewer, OrderMessages, ConfirmationModal },
  setup() {
    const { get, post } = useAdminAuthentication();
    const clients = ref([]);
    const messages = ref([]);
    const selectedOrder = ref(null);
    const nextState = ref(null);
    const stateMessage = ref(null);
    const showModal = ref(false);
    const showError = ref(false);
    const route = useRoute();

    setSelectedOrder();

    async function setSelectedOrder() {
      let response = await get(`/api/admin/orders/${route.params.number}`);
      if(response.status === 200) {
        selectedOrder.value = await response.json();
        setClient();
        setMessages();
      }
    }

    async function setClient() {
      let response = await get(`/api/admin/clients/${selectedOrder.value.clientId}`);
      if(response.status === 200) {
        let client = await response.json();
        selectedOrder.value.name = client.name;
        selectedOrder.value.email = client.email;
      }
    }

    async function setMessages() {
      let response = await get(`/api/admin/orders/${selectedOrder.value.number}`);
      if(response.status === 200) {
        let order = await response.json();
        messages.value = order.messages.reverse();
      }
    }

    function closeSelectedOrder() {
      selectedOrder.value = null;
      messages.value = [];
      router.push('/admin');
    }

    async function submitMessage(message) {
      let response = await post(`/api/admin/orders/${selectedOrder.value.number}`, {
          clientId: localStorage.getItem('adminId'),
          text: message
        });
      if(response.status === 200) {
        let newMessage = await response.json();
        messages.value.unshift(newMessage);
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
      let response;
      if(stateMessage.value !== null) {
        if(stateMessage.value !== '') {
          showError.value = false;
          response = await post(`/api/admin/orders/${selectedOrder.value.number}/state-with-msg`, { state: nextState.value, message: { clientId: localStorage.getItem('adminId'), text: stateMessage.value }});
          setMessages();
        } else {
          showError.value = true;
        }
      } else {
        response = await post(`/api/admin/orders/${selectedOrder.value.number}/state`, nextState.value);
      }
      if(response.status === 200) {
        selectedOrder.value.state = nextState.value;
        nextState.value = null;
      }
      showModal.value = false;
    }

    function cancelChangeOrderState() {
      nextState.value = null;
      showModal.value = false;
    }

    return { clients, selectedOrder, messages, showModal, stateMessage, showError, closeSelectedOrder, submitMessage, tryChangeOrderState, changeOrderState, cancelChangeOrderState }
  }
}
</script>