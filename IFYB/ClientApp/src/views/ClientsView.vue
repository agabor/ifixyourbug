<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height" width="col-12">
          <template v-slot:content>
            <div class="row">
              <div class="col-3 border-primary border border-end-1 border-start-0 border-top-0 border-bottom-0">
                <search-bar v-model:modelValue="filteredClients" :data="clients" :properties="properties"></search-bar>
                <client-list :clients="filteredClients" :selectedClient="selectedClient" @selectClient="selectClient"></client-list>
                <p class="m-2" v-if="filteredClients.length == 0">{{ $t('errors.noResult') }}</p>
              </div>
              <div class="col-9">
                <contact-messages :messages="clientMessages" :selectedClient="selectedClient"></contact-messages>
              </div>
            </div>
          </template>
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
import SearchBar from '../components/SearchBar.vue';
import { useServerError } from "../store";
import { useAdminAuthentication } from "../store/admin";

export default {
  name: 'ClientsView',
  components: { CarouselItem, ClientList, ContactMessages, SearchBar },
  setup() {
    const { get } = useAdminAuthentication();
    const { setServerError, resetServerError } = useServerError();
    const clients = ref([]);
    const selectedClient = ref(null);
    const clientMessages = ref([]);
    const filteredClients = ref([]);
    const properties = ['name', 'email' ];

    setClients();

    async function setClients() {
      let response = await get('/api/admin/clients');
      if(response.status == 200) {
        resetServerError();
        clients.value = await response.json();
        filteredClients.value = clients.value;
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
        resetServerError();
        clientMessages.value = await response.json();
      } else {
        setServerError(response.statusText);
      }       
    }

    return { clients, filteredClients, properties, selectedClient, clientMessages, selectClient }
  }
}
</script>