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
              <h2>{{ $t('orderViewer.title') }}</h2>
              <state-badge class="text-center my-4 py-2 px-4 rounded-pill text-uppercase" :state="order.state" :isSimple="false"></state-badge>
            </div>
            <update-order-form v-if="editableOrder.state == 7" :updateableOrder="editableOrder" @updated="updatedOrder"></update-order-form>
            <form v-else>
              <div class="text-start">
                <div class="row mb-3">
                  <select-framework :modelValue="order.framework" :editable="false" :showError="false"></select-framework>
                  <select-version :modelValue="order.version" :framework="order.framework" :editable="false" :showError="false"></select-version>
                </div>
                <operating-system v-if="order.framework == 1" :modelValue="order.specificPlatform" :version="order.specificPlatformVersion" :editable="false" :showError="false"></operating-system>
                <browser-type v-if="order.framework == 0" :modelValue="order.specificPlatform" :version="order.specificPlatformVersion" :editable="false" :showError="false"></browser-type>
                <online-app :modelValue="order.applicationUrl" :editable="false" :showError="false"></online-app>
                <project-sharing v-if="gitAccess" :modelValue="gitAccess.url" :accessMode="gitAccess.accessMode" :visible="false" :showError="false"></project-sharing>
                <div class="row mb-3">
                  <div class="col-12 form-group mb-0">
                    <label>{{ $t('orderViewer.bugDescription') }}*</label>
                    <text-viewer :value="order.bugDescription"></text-viewer>
                  </div>
                </div>
                <third-party-tool :modelValue="order.thirdPartyTool" :editable="false" :showError="false"></third-party-tool>
              </div>
            </form>
            <div class="text-center">
              <button type="button" class="btn bg-gradient-primary my-4 mx-2" @click="$emit('back')">{{ $t('orderViewer.back') }}</button>
              <button type="button" class="btn bg-gradient-primary my-4 mx-2" @click="$router.push(`/checkout/${order.paymentToken}`)" v-if="order.state == 1">{{ $t('orderViewer.pay') }}</button>
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
import UpdateOrderForm from './UpdateOrderForm.vue';
import { useServerError, useUserAuthentication } from "../store";
import StateBadge from './StateBadge.vue';

export default {
  name: 'OrderViewer',
  components: { TextViewer, SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, ProjectSharing, ThirdPartyTool, UpdateOrderForm, StateBadge },
  props: {
    order: Object,
  },
  emits: ['back'],
  setup(props) {
    const { setServerError, resetServerError } = useServerError();
    const { get } = useUserAuthentication();
    const gitAccess = ref(null);
    const editableOrder = ref(props.order);

    setGitAccess();

    async function setGitAccess() {
      let response = await get(`/api/git-accesses/${props.order.gitAccessId}`);
      if(response.status == 200) {
        resetServerError();
        gitAccess.value = await response.json();
      } else {
        setServerError(response.statusText);
      }
    }

    function updatedOrder(state) {
      editableOrder.value.state = state;
    }

    return { gitAccess, editableOrder, updatedOrder }
  }
}
</script>