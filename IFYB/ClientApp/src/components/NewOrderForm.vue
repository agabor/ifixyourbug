<template>
  <form>
    <div class="text-start">
      <online-app v-model="order.applicationUrl" :editable="true" :showError="showErrors"></online-app>
      <git-access-selector v-if="gitAccesses.length > 0" :accesses="gitAccesses" v-model="selectedAccess"></git-access-selector>
      <project-sharing v-model="order.selectedAccess.url" v-model:accessMode="order.selectedAccess.accessMode" :visible="!order.selectedAccess.id" :showError="showErrors"></project-sharing>
      <bug-description v-model="order.bugDescription" :showError="showErrors"></bug-description>
      <accept-terms :showError="showErrors"></accept-terms>
    </div>
  </form>
  <div class="d-flex justify-content-center my-4">
    <one-click-btn :active="progress === 0" :text="$t('newOrder.submit')" class="bg-gradient-primary mx-2" @click="trySubmitOrder"></one-click-btn>
    <div class="text-center">
      <button type="button" class="btn btn-outline-secondary mx-2" @click="cancelSubmit">{{ $t('newOrder.cancel') }}</button>
    </div>
  </div>
  <div class="progress">
    <div class="progress-bar bg-primary" role="progressbar" :style="`width: ${progress}%`" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
  </div>
</template>

<script>
import { ref, watch, reactive } from 'vue';
import { required, validGitUrl } from '../utils/Validate';
import OnlineApp from './orderComponents/OnlineApp.vue';
import GitAccessSelector from './orderComponents/GitAccessSelector.vue';
import ProjectSharing from './orderComponents/ProjectSharing.vue';
import BugDescription from './orderComponents/BugDescription.vue'
import AcceptTerms from './orderComponents/AcceptTerms.vue';
import { useInputError, useGitAccess, useMessages } from "../store";
import { useClientAuthentication } from "../store/client";
import router from '../router';
import OneClickBtn from './OneClickBtn.vue';

export default {
  name: 'NewOrderForm',
  components: { OnlineApp, GitAccessSelector, ProjectSharing, BugDescription, AcceptTerms, OneClickBtn },
  emits: ['toSuccessPage'],
  setup(props, context) {
    const { hasInputError, setInputError } = useInputError();
    const { tm } = useMessages();
    const { post } = useClientAuthentication();
    const { gitAccesses, getGitAccessId } = useGitAccess();
    const order = reactive({
      applicationUrl: null,
      bugDescription: '',
      accessMode: null,
      url: null,
      selectedAccess: {}
    });
    const selectedAccess = ref({});
    const showErrors = ref(false);
    const progress = ref(0);

    watch(selectedAccess, () => {
      order.selectedAccess = selectedAccess.value;
      setInputError('url', validGitUrl(order.selectedAccess.url));
      setInputError('accessMode', required(order.selectedAccess.accessMode, tm('errors.requiredAccessMode')));
    })

    watch(order, () => {
      localStorage.setItem('order', JSON.stringify(order));
    })

    if(localStorage.getItem('order')) {
      let tempOrder = JSON.parse(localStorage.getItem('order'));
      selectedAccess.value = tempOrder.selectedAccess;
      Object.assign(order, tempOrder);
    }

    function cancelSubmit() {
      let prev = router.options.history.state.back;
      if(prev === null) {
        router.push('/my-orders');
      } else {
        router.back();
      }
    }

    async function trySubmitOrder() {
      if(hasInputError()) {
        showErrors.value = true;
      } else {
        progress.value = 30;
        await submitOrder();
        progress.value = 100;
      }
    }

    async function submitOrder() {
      window.rdt('track', 'Purchase');
      let response = await post('/api/orders',
        {
          'applicationUrl': order.applicationUrl,
          'bugDescription': order.bugDescription,
          'gitAccessId': await getGitAccessId(selectedAccess.value.id, order.selectedAccess.url, order.selectedAccess.accessMode)
        }
      );
      if(response.status === 200) {
        localStorage.removeItem('order');
        context.emit('toSuccessPage');
      } else {
        progress.value = 0;
      }
    }

    return { showErrors, order, selectedAccess, gitAccesses, progress, trySubmitOrder, cancelSubmit }
  }
}
</script>