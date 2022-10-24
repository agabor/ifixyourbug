<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height" width="col-12">
          <template v-slot:content>
            <stackoverflow-request-list v-if="requests.length > 0" :requests="requests" @openRequest="openRequest"></stackoverflow-request-list>
          </template>
        </carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import StackoverflowRequestList from '../components/StackoverflowRequestList.vue';
import { setServerError, resetServerError } from "../store/serverError";
import { useAdminAuthentication } from "../store/admin";
import router from '@/router';

export default {
  name: 'StackoverflowRequestsView',
  components: { CarouselItem, StackoverflowRequestList },
  setup() {
    const { get } = useAdminAuthentication();
    const requests = ref([]);
    setRequests();

    async function setRequests() {
      let response = await get('/api/admin/stackoverflow-questions');
      if(response.status == 200) {
        resetServerError();
        requests.value = await response.json();
      } else {
        setServerError(response.statusText);
      }       
    }

    function openRequest(request) {
      router.push(`/stackoverflow-questions/${request.number}`)
    }

    return { requests, openRequest }
  }
}
</script>