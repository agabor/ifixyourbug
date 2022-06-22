<template>
  <form>
    <div class="row text-start">
      <div class="col-md-12 d-flex pe-2 mb-3">
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
    </div>
  </form>
  <div class="d-flex justify-content-center my-4">
    <div class="text-center">
      <button type="button" class="btn bg-gradient-primary mx-2" @click="trySubmitOrder">{{ $t('newOrder.submit') }}</button>
    </div>
    <div class="text-center">
      <button type="button" class="btn btn-outline-secondary mx-2" @click="cancelSubmit">{{ $t('newOrder.cancel') }}</button>
    </div>
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
import { useServerError, useInputError } from "../store";
import router from '../router'

export default {
  name: 'NewOrderForm',
  components: { SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, GitAccessSelector, ProjectSharing, BugDescription, ThirdPartyTool },
  props: {
    gitAccesses: Array
  },
  emits: ['toSuccessPage'],
  setup(props, context) {
    const { setServerError } = useServerError();
    const { hasInputError } = useInputError();
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
    const error = ref(null);
    const selectedAccess = ref({});
    const showErrors = ref(false);

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
      localStorage.setItem('order', JSON.stringify(order))
    })
    
    function cancelSubmit() {
      router.push('/');
    }

    function trySubmitOrder() {
      console.log('trySubmitOrder', hasInputError())
      if(hasInputError()) {
        showErrors.value = true;
      } else {
        submitOrder();
      }
    }

    async function submitOrder() {
      let orderResponse = await fetch('/api/orders', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${localStorage.getItem('jwt')}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          'framework': order.framework,
          'version': order.version,
          'applicationUrl': order.applicationUrl,
          'specificPlatform': order.specificPlatform,
          'specificPlatformVersion': order.specificPlatformVersion,
          'thirdPartyTool': order.thirdPartyTool,
          'bugDescription': order.bugDescription,
          'gitAccessId': await getGitAccessId()
        })
      });
      if(orderResponse.status == 200) {
        setServerError(null);
        localStorage.removeItem('order');
        context.emit('toSuccessPage');
        error.value = null;
      } else {
        setServerError(orderResponse.statusText);
      }
    }

    async function getGitAccessId() {
      let gitAccessId;
      if(selectedAccess.value.id != undefined){
        gitAccessId = selectedAccess.value.id;
      } else {
        let response = await fetch('/api/git-accesses', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'url': order.repoUrl, 'accessMode': order.accessMode})
        });
        if(response.status == 200) {
          setServerError(null);
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

    return { showErrors, order, selectedAccess, trySubmitOrder, cancelSubmit }
  }
}
</script>