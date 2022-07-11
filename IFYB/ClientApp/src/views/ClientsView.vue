<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height active" width="col-12">
          <client-list :clients="clients"></client-list>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import ClientList from '../components/ClientList.vue';
import { useServerError, useAdminAuthentication } from "../store";

export default {
  name: 'ClientsView',
  components: { CarouselItem, ClientList },
  setup() {
    const { get } = useAdminAuthentication();
    const { setServerError } = useServerError();
    const clients = ref([]);

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

    return { clients }
  }
}
</script>