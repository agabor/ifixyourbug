<template>
  <form>
    <div class="row text-start">
      <div class="col-md-12 d-flex pe-2 mb-3">
        <select-framework :modelvalue="order.framework" :editable="false"></select-framework>
        <select-version :modelvalue="order.version" :versions="[order.version]" :editable="false"></select-version>
      </div>
      <operating-system v-if="order.framework == 1" :isSpecificOpSystem="order.specificPlatform != ''" :operatingSystem="order.specificPlatform" :version="order.specificPlatformVersion" :editable="false"></operating-system>
      <browser-type v-if="order.framework == 0" :isSpecificBrowser="order.specificPlatform != ''" :browser="order.specificPlatform" :version="order.specificPlatformVersion" :editable="false"></browser-type>
      <online-app :available="order.applicationUrl != ''" :url="order.applicationUrl" :editable="false"></online-app>
      <project-sharing v-if="gitAccess" :accessMode="gitAccess.accessMode" :url="gitAccess.url" :visible="false"></project-sharing>
      <div class="col-md-12 pe-2 mb-3">
        <div class="form-group mb-0">
          <label>{{ $t('newOrder.projectDescription') }}*</label>
          <text-viewer :value="order.projectDescription"></text-viewer>
        </div>
      </div>
      <div class="col-md-12 pe-2 mb-3">
        <div class="form-group mb-0">
          <label>{{ $t('newOrder.bugDescription') }}*</label>
          <text-viewer :value="order.bugDescription"></text-viewer>
        </div>
      </div>
      <third-party-tool :isTool="order.thirdPartyTool == '' ? false : true" :tool="order.thirdPartyTool" :editable="false"></third-party-tool>
    </div>
  </form>
  <div class="text-center">
    <button type="button" class="btn bg-gradient-primary my-4" @click="$emit('back')">{{ $t('orderViewer.back') }}</button>
  </div>
</template>

<script>
import { ref } from 'vue';
import TextViewer from './TextViewer.vue';
import SelectFramework from './orderComponents/SelectFramework.vue';
import SelectVersion from './orderComponents/SelectVersion.vue';
import OperatingSystem from './orderComponents/OperatingSystem.vue';
import BrowserType from './orderComponents/BrowserType.vue';
import OnlineApp from './orderComponents/OnlineApp.vue';
import ProjectSharing from './orderComponents/ProjectSharing.vue';
import ThirdPartyTool from './orderComponents/ThirdPartyTool.vue';
import { useServerError } from "../store";

export default {
  name: 'OrderViewer',
  components: { TextViewer, SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, ProjectSharing, ThirdPartyTool },
  props: {
    order: Object,
    isAdmin: Boolean
  },
  emits: ['back'],
  setup(props) {
    const { setServerError } = useServerError();
    const gitAccess = ref(null);
    
    setGitAccess();

    async function setGitAccess() {
      let link = props.isAdmin ? `/api/admin/git-accesses/${props.order.gitAccessId}` : `/api/git-accesses/${props.order.gitAccessId}`;
      let response = await fetch(link, {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${localStorage.getItem('jwt')}`
        }
      })
      if(response.status == 200) {
        setServerError(null);
        gitAccess.value = await response.json();
      } else {
        setServerError(response.statusText);
      }
    }

    return { gitAccess }
  }
}
</script>