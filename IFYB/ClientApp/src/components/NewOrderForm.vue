<template>
  <form>
    <div class="row text-start">
      <div class="col-md-12 d-flex pe-2 mb-3">
        <select-framework v-model:modelvalue="order.framework" @update:modelvalue="changeFramework"></select-framework>
        <select-version v-model:modelvalue="order.version" :versions="order.framework == 0 ? vueVersions : order.framework == 1 ? aspVersions : undefined"></select-version>
      </div>
      <operating-system v-if="order.framework == 1" v-model:isSpecificOpSystem="order.isSpecificOpSystem" v-model:operatingSystem="order.os" v-model:version="order.opSystemVersion"></operating-system>
      <browser-type v-if="order.framework == 0" v-model:isSpecificBrowser="order.isSpecificBrowser" v-model:browser="order.browser" v-model:version="order.browserVersion"></browser-type>
      <online-app v-model:available="order.isAvailableApp" v-model:url="order.availableAppUrl"></online-app>
      <git-access-selector v-if="gitAccesses.length > 0" :accesses="gitAccesses" v-model:access="selectedAccess"></git-access-selector>
      <project-sharing v-model:accessMode="order.accessMode" v-model:url="order.repoUrl" :visible="selectedAccess.url == undefined"></project-sharing>
      <div class="col-md-12 pe-2 mb-3">
        <div class="form-group mb-0">
          <label>{{ $t('newOrder.projectDescription') }}*</label>
          <text-editor id="project-description-input" v-model:modelValue="order.projectDescription" :placeholder="$t('newOrder.projectDescription')"></text-editor>
        </div>
      </div>
      <div class="col-md-12 pe-2 mb-3">
        <div class="form-group mb-0">
          <label>{{ $t('newOrder.bugDescription') }}*</label>
          <text-editor id="bug-description-input" v-model:modelValue="order.bugDescription" :placeholder="$t('newOrder.bugDescription')"></text-editor>
        </div>
      </div>
      <third-party-tool v-model:isTool="order.isThirdPartyTool" v-model:tool="order.thirdPartyTool"></third-party-tool>
    </div>
  </form>
  <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
    {{ error }}
  </div>
  <div class="text-center">
    <button type="button" class="btn bg-gradient-primary my-4" @click="trysubmitOrder">{{ $t('newOrder.submit') }}</button>
  </div>
</template>

<script>
import { ref, watch } from 'vue';
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

export default {
  name: 'NewOrderForm',
  components: { TextEditor, SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, GitAccessSelector, ProjectSharing, ThirdPartyTool },
  props: {
    jwt: String,
    gitAccesses: Array
  },
  emits: ['toSuccessPage'],
  setup(props, context) {
    const { tm } = useI18n();
    const order = ref({});
    const error = ref(null);
    const aspVersions = ['3.1', '5.0', '6.0', '7.0'];
    const vueVersions = ['2.6', '3.0', '3.1', '3.2'];
    const selectedAccess = ref({});
    order.value.isThirdPartyTool = false;
    order.value.isSpecificOpSystem = false;

    watch(selectedAccess, () => {
      if(selectedAccess.value) {
        order.value.accessMode = selectedAccess.value.accessMode;
        order.value.repoUrl = selectedAccess.value.url;
      } else {
        order.value.accessMode = undefined;
        order.value.repoUrl = undefined;
      }
    })

    function trysubmitOrder() {
      let err = getOrderFormError();        
      if(err) {
        error.value = err;
      } else {
        submitOrder();
      }
    }

    async function submitOrder() {
      let specificPlatform, specificPlatformVersion;
      if(order.value.isSpecificOpSystem) {
        specificPlatform = order.value.os;
        specificPlatformVersion = order.value.opSystemVersion;
      } else if(order.value.isSpecificBrowser) {
        specificPlatform = order.value.browser;
        specificPlatformVersion = order.value.browserVersion;
      }
      let orderResponse = await fetch('/api/orders', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${props.jwt}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          'framework': order.value.framework,
          'version': order.value.version,
          'applicationUrl': order.value.availableAppUrl,
          'specificPlatform': specificPlatform,
          'specificPlatformVersion': specificPlatformVersion ? specificPlatformVersion : '',
          'thirdPartyTool': order.value.isThirdPartyTool ? order.value.thirdPartyTool : '',
          'projectDescription': order.value.projectDescription,
          'bugDescription': order.value.bugDescription,
          'gitAccessId': await getGitAccessId()
        })
      });
      if(orderResponse.status == 200) {
        context.emit('toSuccessPage');
        error.value = null;
      } else {
        error.value = `${tm('errors.somethingWrong')} - ${orderResponse.statusText} (${orderResponse.status})`;
      }
    }

    async function getGitAccessId() {
      let gitAccessId;
      if(selectedAccess.value.id != undefined){
        gitAccessId = selectedAccess.value.id;
      } else {
        let gitResponse = await fetch('/api/git-accesses', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${props.jwt}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'url': order.value.repoUrl, 'accessMode': order.value.accessMode})
        });
        gitAccessId = (await gitResponse.json()).id;
      }
      return gitAccessId;
    }

    function getOrderFormError() {
      let err =
        required(order.value.framework, tm('errors.requiredFramework'), 'choices-framework') ||
        required(order.value.version, tm('errors.requiredVersion'), 'choices-version');
      if(!err && order.value.isSpecificOpSystem)
        err =
          required(order.value.os, tm('errors.requiredOS')) ||
          required(order.value.opSystemVersion, tm('errors.requiredOSVersion'), 'op-system-name-input');
      if(!err && order.value.isSpecificBrowser)
        err =
          required(order.value.browser, tm('errors.requiredBrowserType')) ||
          required(order.value.browserVersion, tm('errors.requiredBrowserVersion'), 'browser-system-name-input');
      if(!err && order.value.isAvailableApp)
        err = required(order.value.availableAppUrl, tm('errors.requiredAppUrl'), 'app-url-input');
      if(!err)
        err =
          required(order.value.repoUrl, tm('errors.requiredGitRepoUrl'), 'repo-url-input') ||
          required(order.value.accessMode, tm('errors.requiredProjectSharing')) ||
          required(order.value.projectDescription, tm('errors.requiredProjectDes'), 'project-description-input') ||
          required(order.value.bugDescription, tm('errors.requiredBugDes'), 'bug-description-input');
      if(!err && order.value.isThirdPartyTool)
        err = required(order.value.thirdPartyTool, tm('errors.requiredThirdPartyTool'), 'third-party-tool-input');
      return err;
    }

    function changeFramework(newFramework) {
      order.value.framework = newFramework;
      order.value.version = undefined;
      order.value.os = undefined;
      order.value.opSystemVersion = undefined;
      order.value.isSpecificOpSystem = undefined;
      order.value.isSpecificBrowser = undefined;
      order.value.browser = undefined;
      order.value.browserVersion = undefined;
    }

    return { error, order, aspVersions, vueVersions, selectedAccess, trysubmitOrder, changeFramework }
  }
}
</script>