<template>
  <form>
    <div class="text-start">
      <online-app v-model="order.applicationUrl" :editable="true" :showError="showErrors"></online-app>
      <git-access-selector v-if="gitAccesses.length > 0" :accesses="gitAccesses" v-model="selectedAccess"></git-access-selector>
      <project-sharing v-model="order.selectedAccess.url" v-model:accessMode="order.selectedAccess.accessMode" :visible="!selectedAccess.id" :showError="showErrors"></project-sharing>
      <bug-description v-if="loadedTinymce" v-model="order.bugDescription" :showError="showErrors"></bug-description>
      <accept-terms :showError="showErrors"></accept-terms>
    </div>
  </form>
  <div class="d-flex justify-content-center my-4">
    <one-click-btn :active="progress === 0" :text="$t('updateOrder.update')" class="bg-gradient-primary mx-2" @click="tryUpdateOrder"></one-click-btn>
    <div class="text-center">
      <button type="button" class="btn btn-outline-secondary mx-2" @click="cancelSubmit">{{ $t('updateOrder.cancel') }}</button>
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
import { useInputError, useGitAccess, useTinyMce, useMessages } from "../store";
import { useClientAuthentication } from "../store/client";
import router from '../router';
import OneClickBtn from './OneClickBtn.vue';

export default {
  name: 'NewOrderForm',
  components: { OnlineApp, GitAccessSelector, ProjectSharing, BugDescription, AcceptTerms, OneClickBtn },
  props: {
    modelValue: Object,
  },
  emits: ['update:modelValue'],
  setup(props, context) {
    const { hasInputError, setInputError } = useInputError();
    const { get, post } = useClientAuthentication();
    const { loadedTinymce } = useTinyMce();
    const { gitAccesses, getGitAccessId } = useGitAccess();
    const { tm } = useMessages();
    const order = reactive(props.modelValue);
    const selectedAccess = ref(props.modelValue.selectedAccess ?? {});
    const showErrors = ref(false);
    const progress = ref(0);

    watch(selectedAccess, () => {
      order.selectedAccess = selectedAccess.value;
      setInputError('url', validGitUrl(order.selectedAccess.url));
      setInputError('accessMode', required(order.selectedAccess.accessMode, tm('errors.requiredAccessMode')));
    })

    setGitAccess();

    async function setGitAccess() {
      let response = await get(`/api/git-accesses/${props.modelValue.gitAccessId}`);
      if(response.status === 200) {
        selectedAccess.value = await response.json();
      }
    }
    
    function cancelSubmit() {
      router.push('/my-orders');
    }

    async function tryUpdateOrder() {
      if(hasInputError()) {
        showErrors.value = true;
      } else {
        progress.value = 30;
        await updateOrder();
        progress.value = 100;
      }
    }

    async function updateOrder() {
      let response = await post(`/api/orders/${order.number}/update`,
        {
          'applicationUrl': order.applicationUrl,
          'bugDescription': order.bugDescription,
          'gitAccessId': await getGitAccessId(selectedAccess.value.id, selectedAccess.value.url, selectedAccess.value.accessMode)
        }
      );
      if(response.status === 200) {
        let newOrder = await response.json();
        Object.assign(order, newOrder);
        selectedAccess.value = order.selectedAccess;
        context.emit('update:modelValue', order);
      }
    }

    return { showErrors, order, selectedAccess, gitAccesses, progress, loadedTinymce, tryUpdateOrder, cancelSubmit }
  }
}
</script>