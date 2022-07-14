<template>
  <div class="container mt-6">
    <div class="row">
      <div class="col-12 mx-auto my-4">
        <div class="card">
          <div class="card-body px-lg-5 py-lg-5 text-center">
            <div class="info mb-4">
              <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                <i :class="`ni ni-tag opacity-10 mt-2`"></i>
              </div>
            </div>
            <div class="d-flex flex-column align-items-center justify-content-center">
              <div class="d-flex align-items-center justify-content-center">
                <h2>{{ $t('orderViewer.title') }} {{ order.number }}</h2>
              </div>
              <div class="text-center my-4 py-2 px-4 rounded-pill text-white text-uppercase bg-dark" v-if="order.state == 0">{{ $t('orderList.submitted') }}</div>
              <div class="text-center my-4 py-2 px-4 rounded-pill text-white text-uppercase bg-info" v-else-if="order.state == 1">{{ $t('orderList.accepted') }}</div>
              <div class="text-center my-4 py-2 px-4 rounded-pill text-white text-uppercase bg-danger" v-else-if="order.state == 2">{{ $t('orderList.rejected') }}</div>
              <div class="text-center my-4 py-2 px-4 rounded-pill text-uppercase bg-light " v-else-if="order.state == 3">{{ $t('orderList.payed') }}</div>
              <div class="text-center my-4 py-2 px-4 rounded-pill text-white text-uppercase bg-success" v-else-if="order.state == 4">{{ $t('orderList.completed') }}</div>
              <div class="text-center my-4 py-2 px-4 rounded-pill text-white text-uppercase bg-warning" v-else-if="order.state == 5">{{ $t('orderList.refundable') }}</div>
              <div class="d-flex align-items-center justify-content-center" v-if="order.state == 0">
                <div class="text-center ms-4">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 1, false)">{{ $t('orderViewer.accept') }}</button>
                </div>
                <div class="text-center ms-4">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 2, false)">{{ $t('orderViewer.reject') }}</button>
                </div>
                <div class="text-center ms-4">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 2, true)">{{ $t('orderViewer.rejectWithMessage') }}</button>
                </div>
              </div>
              <div class="d-flex align-items-center justify-content-center" v-if="order.state == 3">
                <div class="text-center ms-4">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 4, false)">{{ $t('orderViewer.completed') }}</button>
                </div>
                <div class="text-center ms-4">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 5, false)">{{ $t('orderViewer.refundable') }}</button>
                </div>
              </div>
            </div>
            <form>
              <div class="row text-start">
                <div class="col-md-12 d-flex pe-2 mb-3">
                  <div class="col-md-6">
                    <label>{{ $t('orderViewer.name') }}</label>
                    <div class="py-2 px-4 rounded-pill text-white bg-gradient-primary">{{ order.name }}</div>
                  </div>
                  <div class="col-md-6 ps-md-2">
                    <label>{{ $t('orderViewer.email') }}</label>
                    <div class="d-flex justify-content-between align-items-center py-2 px-4 rounded-pill text-white bg-gradient-primary">
                      <span class="text-truncate">{{ order.email }}{{ order.email }}</span>
                      <i class="ni ni-single-copy-04 cursor-pointer" @click="copyToClipboard(order.email)"></i>
                    </div>
                  </div>
                </div>
                <div class="col-md-12 d-flex pe-2 mb-3">
                  <select-framework :modelValue="order.framework" :editable="false" :showError="false"></select-framework>
                  <select-version :modelValue="order.version" :framework="order.framework" :editable="false" :showError="false"></select-version>
                </div>
                <operating-system v-if="order.framework == 1" :modelValue="order.specificPlatform" :version="order.specificPlatformVersion" :editable="false" :showError="false"></operating-system>
                <browser-type v-if="order.framework == 0" :modelValue="order.specificPlatform" :version="order.specificPlatformVersion" :editable="false" :showError="false"></browser-type>
                <online-app :modelValue="order.applicationUrl" :editable="false" :showError="false"></online-app>
                <project-sharing v-if="gitAccess" :modelValue="gitAccess.url" :accessMode="gitAccess.accessMode" :visible="false" :showError="false"></project-sharing>
                <div class="col-md-12 pe-2 mb-3">
                  <div class="form-group mb-0">
                    <label>{{ $t('newOrder.bugDescription') }}*</label>
                    <text-viewer :value="order.bugDescription"></text-viewer>
                  </div>
                </div>
                <third-party-tool :modelValue="order.thirdPartyTool" :editable="false" :showError="false"></third-party-tool>
              </div>
            </form>
            <div class="text-center">
              <button type="button" class="btn bg-gradient-primary my-4" @click="$emit('back')">{{ $t('orderViewer.back') }}</button>
            </div>
          </div>
        </div>
      </div>
    </div>
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
import { useServerError, useAdminAuthentication } from "../store";

export default {
  name: 'AdminOrderViewer',
  components: { TextViewer, SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, ProjectSharing, ThirdPartyTool },
  props: {
    order: Object,
  },
  emits: ['back', 'changeOrderState' ],
  setup(props) {
    const { setServerError } = useServerError();
    const { get } = useAdminAuthentication();
    const gitAccess = ref(null);

    setGitAccess();

    async function setGitAccess() {
      let response = await get(`/api/admin/git-accesses/${props.order.gitAccessId}`);
      if(response.status == 200) {
        setServerError(null);
        gitAccess.value = await response.json();
      } else {
        setServerError(response.statusText);
      }
    }
    function copyToClipboard(text) {
      navigator.clipboard.writeText(text);
    }
    return { gitAccess, copyToClipboard }
  }
}
</script>

<style scoped>
.cursor-pointer:hover {
  opacity: 0.8;
}
</style>