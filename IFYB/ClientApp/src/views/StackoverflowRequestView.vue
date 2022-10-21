<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <confirmation-modal v-if="showModal" v-model="solveMessage" :title="$t('confirm.requestSolveTitle')" :description="$t('confirm.requestSolveDescription')" :showError="showError" @confirm="changeRequestSolve" @cancel="cancelChangeRequestSolve"></confirmation-modal>
      <div class="carousel-inner">
        <stackoverflow-request-viewer v-if="selectedRequest !== null" class="active" :request="selectedRequest" @back="closeSelectedRequest" @trySolveRequest="showModal = true"></stackoverflow-request-viewer>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { useServerError } from "../store";
import { useAdminAuthentication } from "../store/admin";
import StackoverflowRequestViewer from '../components/StackoverflowRequestViewer.vue';
import ConfirmationModal from '@/components/ConfirmationModal.vue';
import router from '@/router';

export default {
  name: 'StackoverflowRequestView',
  components: { StackoverflowRequestViewer, ConfirmationModal },
  setup() {
    const { setServerError, resetServerError } = useServerError();
    const { get, post } = useAdminAuthentication();
    const selectedRequest = ref(null);
    const solveMessage = ref('');
    const showModal = ref(false);
    const showError = ref(false);
    const route = useRoute();

    setSelectedRequest();

    async function setSelectedRequest() {
      let response = await get(`/api/admin/stackoverflow-questions/${route.params.number}`);
      if(response.status == 200) {
        resetServerError();
        selectedRequest.value = await response.json();
      } else {
        setServerError(response.statusText);
      }
    }

    function closeSelectedRequest() {
      selectedRequest.value = null;
      router.push('/stackoverflow-questions');
    }

    async function changeRequestSolve() {
      let response;
      if(solveMessage.value !== '') {
        showError.value = false;
        response = await post(`/api/admin/stackoverflow-questions/${selectedRequest.value.number}/solved-with-msg`, { solved: true, message: solveMessage.value });
      } else {
        showError.value = true;
      }
      if(response.status == 200) {
        resetServerError();
        selectedRequest.value.solved = true;
      } else {
        setServerError(response.statusText);
      }
      showModal.value = false;
    }

    function cancelChangeRequestSolve() {
      showModal.value = false;
    }

    return { selectedRequest, showModal, solveMessage, showError, closeSelectedRequest, changeRequestSolve, cancelChangeRequestSolve }
  }
}
</script>