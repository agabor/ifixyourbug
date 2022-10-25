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
              <h2>{{ $t('orderViewer.title') }} #{{ modelValue.number }}</h2>
              <p>{{ $filters.dateTimeFormat(modelValue.creationTime) }}</p>
              <state-badge class="text-center mt-2 mb-5 py-2 px-4 rounded-pill text-uppercase" :state="modelValue.state" :isSimple="false"></state-badge>
            </div>
            <update-order-form v-if="modelValue.state === 7" :modelValue="modelValue"></update-order-form>
            <form v-else>
              <div class="text-start">
                <div class="row mb-3">
                  <select-framework :modelValue="modelValue.framework"></select-framework>
                  <select-version :modelValue="modelValue.version" :framework="modelValue.framework"></select-version>
                </div>
                <operating-system v-if="modelValue.framework == 1" :modelValue="modelValue.specificPlatform" :version="modelValue.specificPlatformVersion"></operating-system>
                <browser-type v-if="modelValue.framework == 0" :modelValue="modelValue.specificPlatform" :version="modelValue.specificPlatformVersion"></browser-type>
                <online-app :modelValue="modelValue.applicationUrl"></online-app>
                <project-sharing :modelValue="modelValue.selectedAccess.url" :accessMode="modelValue.selectedAccess.accessMode" :visible="false" :showError="false"></project-sharing>
                <div class="row mb-3">
                  <div class="col-12 form-group mb-0">
                    <label>{{ $t('orderViewer.bugDescription') }}*</label>
                    <span v-html="modelValue.bugDescription"></span>
                  </div>
                </div>
                <third-party-tool :modelValue="modelValue.thirdPartyTool"></third-party-tool>
              </div>
            </form>
            <div class="text-center">
              <button type="button" class="btn bg-gradient-primary my-4 mx-2" @click="backToList">{{ $t('orderViewer.back') }}</button>
              <button type="button" class="btn bg-gradient-primary my-4 mx-2" @click="$router.push(`/checkout/${modelValue.paymentToken}`)" v-if="modelValue.state == 1">{{ $t('orderViewer.pay') }}</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import SelectFramework from './orderComponents/SelectFramework.vue';
import SelectVersion from './orderComponents/SelectVersion.vue';
import OperatingSystem from './orderComponents/OperatingSystem.vue';
import BrowserType from './orderComponents/BrowserType.vue';
import OnlineApp from './orderComponents/OnlineApp.vue';
import ProjectSharing from './orderComponents/ProjectSharing.vue';
import ThirdPartyTool from './orderComponents/ThirdPartyTool.vue';
import UpdateOrderForm from './UpdateOrderForm.vue';
import { useUserAuthentication } from "../store/client";
import StateBadge from './StateBadge.vue';
import router from '@/router';

export default {
  name: 'OrderViewer',
  components: { SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, ProjectSharing, ThirdPartyTool, UpdateOrderForm, StateBadge },
  props: {
    modelValue: Object,
  },
  emits: ['update:modelValue'],
  setup(props, context) {
    const { get } = useUserAuthentication();

    setGitAccess();

    async function setGitAccess() {
      let response = await get(`/api/git-accesses/${props.modelValue.gitAccessId}`);
      if(response.status === 200) {
        let order = props.modelValue;
        order.selectedAccess = await response.json();
        context.emit('update:modelValue', order);
      }
    }

    function backToList() {
      context.emit('update:modelValue', null);
      router.push('/my-orders');
    }

    return { backToList }
  }
}
</script>