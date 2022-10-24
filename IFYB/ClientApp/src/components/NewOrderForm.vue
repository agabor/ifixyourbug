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
      <project-sharing v-model="order.selectedAccess.url" v-model:accessMode="order.selectedAccess.accessMode" :visible="!order.selectedAccess.id" :showError="showErrors"></project-sharing>
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
import { required, validGitUrl } from '../utils/Validate';
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
import { useInputError, useGitAccess, useMessages } from "../store";
import { useUserAuthentication } from "../store/authentication";
import router from '../router';
import OneClickBtn from './OneClickBtn.vue';

export default {
  name: 'NewOrderForm',
  components: { SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, GitAccessSelector, ProjectSharing, BugDescription, ThirdPartyTool, AcceptTerms, OneClickBtn },
  emits: ['toSuccessPage'],
  setup(props, context) {
    const { hasInputError, setInputError } = useInputError();
    const { tm } = useMessages();
    const { post } = useUserAuthentication();
    const { gitAccesses, getGitAccessId } = useGitAccess();
    const order = reactive({
      framework: null,
      version: null,
      applicationUrl: null,
      specificPlatform: null,
      specificPlatformVersion: null,
      thirdPartyTool: null,
      bugDescription: '',
      accessMode: null,
      url: null,
      selectedAccess: {}
    });
    const selectedAccess = ref({});
    const showErrors = ref(false);
    const activeBtn = ref(true);
    const progress = ref(0);

    watch(selectedAccess, () => {
      order.selectedAccess = selectedAccess.value;
      setInputError('url', validGitUrl(order.selectedAccess.url));
      setInputError('accessMode', required(order.selectedAccess.accessMode, tm('errors.requiredAccessMode')));
    })

    if(localStorage.getItem('order')) {
      let tempOrder = JSON.parse(localStorage.getItem('order'));
      selectedAccess.value = tempOrder.selectedAccess;
      Object.assign(order, tempOrder);
    }

    watch(order, () => {
      localStorage.setItem('order', JSON.stringify(order));
    })

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
        activeBtn.value = true;
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
          'framework': order.framework,
          'version': order.version,
          'applicationUrl': order.applicationUrl,
          'specificPlatform': order.specificPlatform,
          'specificPlatformVersion': order.specificPlatformVersion,
          'thirdPartyTool': order.thirdPartyTool,
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

    watch(() => order.framework, () => {
      order.version = null;
      order.specificPlatform = null;
      order.specificPlatformVersion = null;
    });

    return { showErrors, order, selectedAccess, gitAccesses, activeBtn, progress, trySubmitOrder, cancelSubmit }
  }
}
</script>