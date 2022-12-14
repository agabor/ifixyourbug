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
              <div class="align-items-center justify-content-center">
                <div class="d-flex align-items-center justify-content-center">
                  <h2 class="my-0">{{ $t('orderViewer.title') }} #{{ order.number }}</h2>
                  <flag-component :flag="order.flag"></flag-component>
                </div>
                <p>{{ $filters.dateTimeFormat(order.creationTime) }}</p>
              </div>
              <state-badge class="text-center my-4 py-2 px-4 rounded-pill text-uppercase" :state="order.state" :isSimple="false"></state-badge>
              <div class="d-flex align-items-center justify-content-center flex-wrap" v-if="order.state == 0">
                <div class="text-center mx-1">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 1, false)">{{ $t('orderViewer.accept') }}</button>
                </div>
                <div class="text-center mx-1">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 2, true)">{{ $t('orderViewer.rejectWithMessage') }}</button>
                </div>
                <div class="text-center mx-1">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 7, true)">{{ $t('orderState.editable') }}</button>
                </div>
              </div>
              <div class="d-flex align-items-center justify-content-center flex-wrap" v-if="order.state == 1">
                <div class="text-center mx-1">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 6, false)">{{ $t('orderState.canceled') }}</button>
                </div>
              </div>
              <div class="d-flex align-items-center justify-content-center flex-wrap" v-if="order.state == 3">
                <div class="text-center mx-1">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 4, false)">{{ $t('orderState.completed') }}</button>
                </div>
                <div class="text-center mx-1">
                  <button type="button" class="btn btn-outline-secondary my-2" @click="$emit('changeOrderState', 5, false)">{{ $t('orderState.refundable') }}</button>
                </div>
              </div>
            </div>
            <form>
              <div class="text-start">
                <div class="row mb-3">
                  <div class="col-md-6">
                    <label>{{ $t('orderViewer.name') }}</label>
                    <div class="py-2 px-4 rounded-pill text-white bg-gradient-primary">{{ order.name }}</div>
                  </div>
                  <div class="col-md-6 ps-md-2">
                    <label>{{ $t('orderViewer.email') }}</label>
                    <div class="d-flex justify-content-between align-items-center py-2 px-4 rounded-pill text-white bg-gradient-primary">
                      <span class="text-truncate">{{ order.email }}</span>
                      <i class="ni ni-single-copy-04 cursor-pointer" @click="copyToClipboard(order.email)"></i>
                    </div>
                  </div>
                </div>
                <online-app :modelValue="order.applicationUrl" :editable="false" :showError="false"></online-app>
                <project-sharing v-if="gitAccess" :modelValue="gitAccess.url" :accessMode="gitAccess.accessMode" :visible="false" :showError="false"></project-sharing>
                <div class="col-md-12 mb-3">
                  <div class="form-group mb-0">
                    <label>{{ $t('orderViewer.bugDescription') }}*</label>
                    <span v-html="order.bugDescription"></span>
                  </div>
                </div>
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
import OnlineApp from './orderComponents/OnlineApp.vue';
import ProjectSharing from './orderComponents/ProjectSharing.vue';
import { useAdminAuthentication } from "../store/admin";
import StateBadge from './StateBadge.vue';
import FlagComponent from './FlagComponent.vue';

export default {
  name: 'AdminOrderViewer',
  components: { OnlineApp, ProjectSharing, StateBadge, FlagComponent },
  props: {
    order: Object,
  },
  emits: ['back', 'changeOrderState' ],
  setup(props) {
    const { get } = useAdminAuthentication();
    const gitAccess = ref(null);

    setGitAccess();

    async function setGitAccess() {
      let response = await get(`/api/admin/git-accesses/${props.order.gitAccessId}`);
      if(response.status === 200) {
        gitAccess.value = await response.json();
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