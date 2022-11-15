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
                <h2 class="my-0">{{ $t('orderViewer.title') }} #{{ modelValue.number }}</h2>
                <button type="button" class="btn py-1 px-3 ms-2 my-0" :class="{'bg-gradient-dark': modelValue.flag === 0, 'bg-gradient-secondary': modelValue.flag !== 0}">
                  <span>{{ modelValue.flag === 0 ? $t('orderList.bugfix') : $t('orderList.codeReview') }}</span>
                </button>
              </div>
              <p>{{ $filters.dateTimeFormat(modelValue.creationTime) }}</p>
              <state-badge class="text-center mt-2 mb-5 py-2 px-4 rounded-pill text-uppercase" :state="modelValue.state" :isSimple="false"></state-badge>
            </div>
            <update-order-form v-if="modelValue.state === 7" :modelValue="modelValue"></update-order-form>
            <form v-else>
              <div class="text-start">
                <online-app :modelValue="modelValue.applicationUrl"></online-app>
                <project-sharing :modelValue="selectedAccess.url" :accessMode="selectedAccess.accessMode" :visible="false" :showError="false"></project-sharing>
                <div class="row mb-3">
                  <div class="col-12 form-group mb-0">
                    <label>{{ $t('orderViewer.bugDescription') }}*</label>
                    <span v-html="modelValue.bugDescription"></span>
                  </div>
                </div>
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
import { ref } from 'vue';
import OnlineApp from './orderComponents/OnlineApp.vue';
import ProjectSharing from './orderComponents/ProjectSharing.vue';
import UpdateOrderForm from './UpdateOrderForm.vue';
import { useClientAuthentication } from "../store/client";
import StateBadge from './StateBadge.vue';
import router from '@/router';

export default {
  name: 'OrderViewer',
  components: { OnlineApp, ProjectSharing, UpdateOrderForm, StateBadge },
  props: {
    modelValue: Object,
  },
  emits: ['update:modelValue'],
  setup(props, context) {
    const { get } = useClientAuthentication();
    const selectedAccess = ref({});

    setGitAccess();

    async function setGitAccess() {
      let response = await get(`/api/git-accesses/${props.modelValue.gitAccessId}`);
      if(response.status === 200) {
        selectedAccess.value = await response.json();
      }
    }

    function backToList() {
      context.emit('update:modelValue', null);
      router.push('/my-orders');
    }

    return { backToList, selectedAccess }
  }
}
</script>