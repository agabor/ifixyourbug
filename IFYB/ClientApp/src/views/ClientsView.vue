<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height active" width="col-12">
          <div class="row">
            <div class="col-3 border-primary border border-end-1 border-start-0 border-top-0 border-bottom-0">
              <client-list :clients="clients" :selectedClient="selectedClient" @selectClient="selectClient"></client-list>
            </div>
            <div class="col-9">
              <contact-messages :messages="clientMessages" :selectedClient="selectedClient" @submitMessage="submitMessage"></contact-messages>
            </div>
          </div>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import ClientList from '../components/ClientList.vue';
import ContactMessages from '../components/ContactMessages.vue';
import { useServerError, useAdminAuthentication } from "../store";

export default {
  name: 'ClientsView',
  components: { CarouselItem, ClientList, ContactMessages },
  setup() {
    const { get } = useAdminAuthentication();
    const { setServerError } = useServerError();
    const clients = ref([]);
    const selectedClient = ref(null);
    const clientMessages = ref([]);

    setServerError(null);
    setClients();

    async function setClients() {
      let response = await get('/api/admin/clients');
      if(response.status == 200) {
        setServerError(null);
        clients.value = await response.json();
      } else {
        setServerError(response.statusText);
      }       
    }

    function selectClient(client) {
      selectedClient.value = client;
      setClientsMessages();
    }

    async function setClientsMessages() {
      let response = await get(`/api/admin/contact-messages/${selectedClient.value.id}`);
      if(response.status == 200) {
        setServerError(null);
        clientMessages.value = await response.json();
      } else {
        setServerError(response.statusText);
      }       
    }

    async function submitMessage(message) {
      console.log(message);
      //later implementation
    }

    return { clients, selectedClient, clientMessages, selectClient, submitMessage }
  }
}
</script>