<template>
  <form>
    <div class="row text-start">
      <div class="col-md-12 d-flex pe-2 mb-3">
        <select-framework v-model="order.framework" :editable="true"></select-framework>
        <select-version v-model="order.version" :versions="order.framework == 0 ? vueVersions : order.framework == 1 ? aspVersions : undefined" :editable="true"></select-version>
      </div>
      <operating-system v-if="order.framework == 1" v-model:isSpecificOpSystem="order.isSpecificOpSystem" v-model:operatingSystem="order.os" v-model:version="order.opSystemVersion" :editable="true"></operating-system>
      <browser-type v-if="order.framework == 0" v-model:isSpecificBrowser="order.isSpecificBrowser" v-model:browser="order.browser" v-model:version="order.browserVersion" :editable="true"></browser-type>
      <online-app v-model:available="order.isAvailableApp" v-model:url="order.availableAppUrl" :editable="true"></online-app>
      <git-access-selector v-if="gitAccesses.length > 0" :accesses="gitAccesses" v-model:access="selectedAccess"></git-access-selector>
      <project-sharing v-model:accessMode="order.accessMode" v-model:url="order.repoUrl" :visible="selectedAccess.url == undefined"></project-sharing>
      <div class="col-md-12 pe-2 mb-3">
        <div class="form-group mb-0">
          <label>{{ $t('newOrder.bugDescription') }}*</label>
          <text-editor id="bug-description-input" v-model="order.bugDescription" :placeholder="$t('newOrder.bugDescription')"></text-editor>
        </div>
      </div>
      <third-party-tool v-model:isTool="order.isThirdPartyTool" v-model:tool="order.thirdPartyTool" :editable="true"></third-party-tool>
    </div>
  </form>
  <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
    {{ error }}
  </div>
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
import { required } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import TextEditor from '../components/TextEditor.vue';
import SelectFramework from './orderComponents/SelectFramework.vue';
import SelectVersion from './orderComponents/SelectVersion.vue';
import OperatingSystem from './orderComponents/OperatingSystem.vue';
import BrowserType from './orderComponents/BrowserType.vue';
import OnlineApp from './orderComponents/OnlineApp.vue';
import GitAccessSelector from './orderComponents/GitAccessSelector.vue';
import ProjectSharing from './orderComponents/ProjectSharing.vue';
import ThirdPartyTool from './orderComponents/ThirdPartyTool.vue';
import { useServerError } from "../store";
import router from '../router'

export default {
  name: 'NewOrderForm',
  components: { TextEditor, SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, GitAccessSelector, ProjectSharing, ThirdPartyTool },
  props: {
    gitAccesses: Array
  },
  emits: ['toSuccessPage'],
  setup(props, context) {
    const { setServerError } = useServerError();
    const { tm } = useI18n();
    const order = reactive({
      isThirdPartyTool: false,
      isSpecificOpSystem: false,
      framework: null,
      version: null,
      os: null,
      opSystemVersion: null,
      isSpecificBrowser: null,
      browser: null,
      browserVersion: null,
      availableAppUrl: null,
      isAvailableApp: false,
      accessMode: 0,
      repoUrl: null,
      bugDescription: '',
      thirdPartyTool: null,
      selectedAccess: {}
    });
    const error = ref(null);
    const aspVersions = ['3.1', '5.0', '6.0', '7.0'];
    const vueVersions = ['2.6', '2.7', '3.0', '3.1', '3.2'];
    const selectedAccess = ref({});

    watch(selectedAccess, () => {
      if(selectedAccess.value) {
        order.accessMode = selectedAccess.value.accessMode;
        order.repoUrl = selectedAccess.value.url;
      } else {
        order.accessMode = undefined;
        order.repoUrl = undefined;
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
      let err = getOrderFormError();        
      if(err) {
        error.value = err;
      } else {
        submitOrder();
      }
    }

    async function submitOrder() {
      let specificPlatform, specificPlatformVersion;
      if(order.isSpecificOpSystem) {
        specificPlatform = order.os;
        specificPlatformVersion = order.opSystemVersion;
      } else if(order.isSpecificBrowser) {
        specificPlatform = order.browser;
        specificPlatformVersion = order.browserVersion;
      }
      let orderResponse = await fetch('/api/orders', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${localStorage.getItem('jwt')}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          'framework': order.framework,
          'version': order.version,
          'applicationUrl': order.availableAppUrl ? order.availableAppUrl : '',
          'specificPlatform': specificPlatform ? specificPlatform : '',
          'specificPlatformVersion': specificPlatformVersion ? specificPlatformVersion : '',
          'thirdPartyTool': order.isThirdPartyTool ? order.thirdPartyTool : '',
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

    function getOrderFormError() {
      let err =
        required(order.framework, tm('errors.requiredFramework'), 'choices-framework') ||
        required(order.version, tm('errors.requiredVersion'), 'choices-version');
      if(!err && order.isSpecificOpSystem)
        err =
          required(order.os, tm('errors.requiredOS')) ||
          required(order.opSystemVersion, tm('errors.requiredOSVersion'), 'op-system-name-input');
      if(!err && order.isSpecificBrowser)
        err =
          required(order.browser, tm('errors.requiredBrowserType')) ||
          required(order.browserVersion, tm('errors.requiredBrowserVersion'), 'browser-system-name-input');
      if(!err && order.isAvailableApp)
        err = required(order.availableAppUrl, tm('errors.requiredAppUrl'), 'app-url-input');
      if(!err)
        err =
          required(order.repoUrl, tm('errors.requiredGitRepoUrl'), 'repo-url-input') ||
          required(order.accessMode, tm('errors.requiredProjectSharing')) ||
          required(order.bugDescription, tm('errors.requiredBugDes'), 'bug-description-input');
      if(!err && order.isThirdPartyTool)
        err = required(order.thirdPartyTool, tm('errors.requiredThirdPartyTool'), 'third-party-tool-input');
      return err;
    }

    watch(() => order.framework, () => {
      order.version = null;
      order.os = null;
      order.opSystemVersion = null;
      order.isSpecificOpSystem = null;
      order.isSpecificBrowser = null;
      order.browser = null;
      order.browserVersion = null;
    });

    return { error, order, aspVersions, vueVersions, selectedAccess, trySubmitOrder, cancelSubmit }
  }
}
</script>