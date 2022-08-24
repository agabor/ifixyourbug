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
    <one-click-btn v-model:active="activeBtn" :text="$t('newOrder.submit')" class="bg-gradient-primary mx-2" @click="trySubmitOrder"></one-click-btn>
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
import { useServerError, useInputError, useUserAuthentication } from "../store";
import router from '../router';
import OneClickBtn from './OneClickBtn.vue';
import { event } from 'vue-gtag';

export default {
  name: 'NewOrderForm',
  components: { SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, GitAccessSelector, ProjectSharing, BugDescription, ThirdPartyTool, AcceptTerms, OneClickBtn },
  emits: ['toSuccessPage'],
  setup(props, context) {
    const { setServerError, resetServerError } = useServerError();
    const { hasInputError } = useInputError();
    const { get, post } = useUserAuthentication();
    const order = reactive({
      framework: null,
      version: null,
      applicationUrl: null,
      specificPlatform: null,
      specificPlatformVersion: null,
      thirdPartyTool: null,
      bugDescription: '',
      accessMode: null,
      repoUrl: null,
      selectedAccess: {}
    });
    const selectedAccess = ref({});
    const showErrors = ref(false);
    const activeBtn = ref(true);
    const progress = ref(0);
    const gitAccesses = ref([]);

    watch(selectedAccess, () => {
      if(selectedAccess.value) {
        order.accessMode = selectedAccess.value.accessMode;
        order.repoUrl = selectedAccess.value.url;
      } else {
        order.accessMode = null;
        order.repoUrl = null;
      }
    })

    if(localStorage.getItem('order')){
      let tempOrder = JSON.parse(localStorage.getItem('order'));
      selectedAccess.value = tempOrder.selectedAccess;
      Object.assign(order, tempOrder);
    }

    watch(order, () => {
      order.selectedAccess = selectedAccess.value;
      localStorage.setItem('order', JSON.stringify(order));
    })

    setGitAccesses();

    async function setGitAccesses() {
      let response = await get('/api/git-accesses');
      if(response.status == 200) {
        resetServerError();
        gitAccesses.value = await response.json();
      } else {
        setServerError(response.statusText);
      }
    }
    
    function cancelSubmit() {
      event('cancel-submit-new-order');
      router.push('/');
    }

    function trySubmitOrder() {
      event('try-submit-new-order');
      if(hasInputError()) {
        showErrors.value = true;
        activeBtn.value = true;
      } else {
        progress.value = 30;
        submitOrder();
        progress.value = 100;
      }
    }

    async function submitOrder() {
      let response = await post('/api/orders',
        {
          'framework': order.framework,
          'version': order.version,
          'applicationUrl': order.applicationUrl,
          'specificPlatform': order.specificPlatform,
          'specificPlatformVersion': order.specificPlatformVersion,
          'thirdPartyTool': order.thirdPartyTool,
          'bugDescription': order.bugDescription,
          'gitAccessId': await getGitAccessId()
        }
      );
      event('submit-new-order', { 'value': response.status });
      if(response.status == 200) {
        resetServerError();
        localStorage.removeItem('order');
        context.emit('toSuccessPage');
      } else {
        progress.value = 0;
        setServerError(response.statusText);
      }
    }

    async function getGitAccessId() {
      let gitAccessId;
      if(selectedAccess.value.id != undefined){
        gitAccessId = selectedAccess.value.id;
      } else {
        let response = await post(`/api/git-accesses`, {'url': order.repoUrl, 'accessMode': order.accessMode});
        if(response.status == 200) {
          resetServerError();
          gitAccessId = (await response.json()).id;
        } else {
          setServerError(response.statusText);
        }
      }
      return gitAccessId;
    }

    watch(() => order.framework, () => {
      order.version = null;
      order.specificPlatform = null;
      order.specificPlatformVersion = null;
    });

    return { showErrors, order, selectedAccess, gitAccesses, activeBtn, progress, trySubmitOrder, cancelSubmit }
  }
}
</script>