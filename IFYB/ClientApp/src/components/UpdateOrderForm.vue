<template>
  <form>
    <div class="text-start">
      <div class="row mb-3">
        <select-framework v-model="order.framework" :editable="true" :showError="showErrors"></select-framework>
        <select-version v-model="order.version" :framework="order.framework" :editable="true" :showError="showErrors"></select-version>
      </div>
      <operating-system v-if="order.framework == 1" v-model="order.specificPlatform" v-model:version="order.specificPlatformVersion" :editable="true" :showError="showErrors"></operating-system>
      <browser-type v-if="order.framework == 0" v-model="order.specificPlatform" v-model:version="order.specificPlatformVersion" :editable="true" :showError="showErrors"></browser-type>
      <online-app v-model="order.applicationUrl" :editable="true" :showError="showErrors"></online-app>
      <git-access-selector v-if="gitAccesses.length > 0" :accesses="gitAccesses" v-model="selectedAccess"></git-access-selector>
      <project-sharing v-model="order.repoUrl" v-model:accessMode="order.accessMode" :visible="!selectedAccess.url" :showError="showErrors"></project-sharing>
      <bug-description v-model="order.bugDescription" :showError="showErrors"></bug-description>
      <third-party-tool v-model="order.thirdPartyTool" :editable="true" :showError="showErrors"></third-party-tool>
      <accept-terms :showError="showErrors"></accept-terms>
    </div>
  </form>
  <div class="d-flex justify-content-center my-4">
    <one-click-btn v-model:active="activeBtn" :text="$t('updateOrder.update')" class="bg-gradient-primary mx-2" @click="tryUpdateOrder"></one-click-btn>
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
import SelectFramework from './orderComponents/SelectFramework.vue';
import SelectVersion from './orderComponents/SelectVersion.vue';
import OperatingSystem from './orderComponents/OperatingSystem.vue';
import BrowserType from './orderComponents/BrowserType.vue';
import OnlineApp from './orderComponents/OnlineApp.vue';
import GitAccessSelector from './orderComponents/GitAccessSelector.vue';
import ProjectSharing from './orderComponents/ProjectSharing.vue';
import BugDescription from './orderComponents/BugDescription.vue'
import ThirdPartyTool from './orderComponents/ThirdPartyTool.vue';
import AcceptTerms from './orderComponents/AcceptTerms.vue';
import { useServerError, useInputError, useUserAuthentication, useGitAccess } from "../store";
import router from '../router';
import OneClickBtn from './OneClickBtn.vue';

export default {
  name: 'NewOrderForm',
  components: { SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, GitAccessSelector, ProjectSharing, BugDescription, ThirdPartyTool, AcceptTerms, OneClickBtn },
  props: {
    updateableOrder: Object,
  },
  emits: ['updated'],
  setup(props, context) {
    const { setServerError, resetServerError } = useServerError();
    const { hasInputError } = useInputError();
    const { get, post } = useUserAuthentication();
    const { gitAccesses, getGitAccessId } = useGitAccess();
    const order = reactive(props.updateableOrder);
    const selectedAccess = ref({});
    const showErrors = ref(false);
    const activeBtn = ref(true);
    const progress = ref(0);

    watch(selectedAccess, () => {
      if(selectedAccess.value) {
        order.accessMode = selectedAccess.value.accessMode;
        order.repoUrl = selectedAccess.value.url;
      } else {
        order.accessMode = null;
        order.repoUrl = null;
      }
    })

    setGitAccess();

    async function setGitAccess() {
      let response = await get(`/api/git-accesses/${props.updateableOrder.gitAccessId}`);
      if(response.status == 200) {
        resetServerError();
        selectedAccess.value = await response.json();
      } else {
        setServerError(response.statusText);
      }
    }
    
    function cancelSubmit() {
      router.push('/my-orders');
    }

    function tryUpdateOrder() {
      if(hasInputError()) {
        showErrors.value = true;
        activeBtn.value = true;
      } else {
        progress.value = 30;
        updateOrder();
        progress.value = 100;
      }
    }

    async function updateOrder() {
      let response = await post(`/api/orders/${order.number}/update`,
        {
          'framework': order.framework,
          'version': order.version,
          'applicationUrl': order.applicationUrl,
          'specificPlatform': order.specificPlatform,
          'specificPlatformVersion': order.specificPlatformVersion,
          'thirdPartyTool': order.thirdPartyTool,
          'bugDescription': order.bugDescription,
          'gitAccessId': await getGitAccessId(selectedAccess.value.id, order.repoUrl, order.accessMode)
        }
      );
      if(response.status == 200) {
        resetServerError();
        context.emit('updated', await response.json())
      } else {
        setServerError(response.statusText);
      }
    }

    watch(() => order.framework, () => {
      order.version = null;
      order.specificPlatform = null;
      order.specificPlatformVersion = null;
    });

    return { showErrors, order, selectedAccess, gitAccesses, activeBtn, progress, tryUpdateOrder, cancelSubmit }
  }
}
</script>