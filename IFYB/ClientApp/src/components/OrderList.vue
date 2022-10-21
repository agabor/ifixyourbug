<template>
  <button type="button" class="btn bg-gradient-primary mt-4" @click="$router.push('/new-order')">
    {{ $t('orderList.addNewOrder') }}
  </button>
  <div class="row">
    <div class="col-12">
      <div class="card mt-3" v-for="(order, idx) in sortedOrders" :key="idx">
        <div class="row">
          <div class="col-lg-9 col-md-8 col-12 ps-lg-0 my-auto">
            <div class="card-body text-start">
              <h5 class="mb-0">Order number: #{{ order.number }}</h5>
              <p class="m-0">{{ $filters.dateTimeFormat(order.creationTime) }}</p>
              <div class="d-flex">
                <h6 class="text-info mb-0">{{ order.framework == 0 ? 'Vue.js' : 'ASP.NET Core' }} {{ order.version }}</h6>
                <p class="mx-1 mb-0" v-if="order.specificPlatform">| {{ order.specificPlatform }}</p>
              </div>
              <p class="mb-0" v-if="order.applicationUrl">{{ $t('orderList.applicationUrl') }}: <a class="text-decoration-underline" :href="order.applicationUrl" >{{ order.applicationUrl }}</a></p>
              <p class="mb-0" v-if="order.thirdPartyTool">{{ $t('orderList.thirdPartyTool') }}: {{ order.thirdPartyTool }}</p>
              <state-badge class="badge badge-sm mt-3" :state="order.state" view="list" :isSimple="true"></state-badge>
            </div>
          </div>
          <div class="col-lg-3 col-md-4 col-12 pe-4">
            <div class="d-flex justify-content-end flex-wrap">
              <button type="button" class="btn btn-outline-secondary my-2 d-flex align-items-center" @click="$router.push(`/checkout/${order.paymentToken}`)" v-if="order.paymentToken && order.state == 1">
                {{ $t('orderList.pay') }}
                <span class="text-secondary text-xs font-weight-bold ms-2">
                  <i class="ni ni-cart opacity-10"></i>
                </span>
              </button>
              <button type="button" class="btn bg-gradient-primary my-2 d-flex align-items-center ms-2" @click="$emit('openOrder', order)">
                {{ $t('orderList.details') }}
                <span class="text-white text-xs font-weight-bold ms-2">
                  <i class="ni ni-bold-right opacity-10"></i>
                </span>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <p class="m-2" v-if="sortedOrders.length == 0">{{ $t('errors.noResult') }}</p>
</template>

<script>
import { ref, watch } from 'vue';
import StateBadge from './StateBadge.vue';

export default {
  name: 'OrderList',
  components: { StateBadge },
  props: {
    orders: Array
  },
  emits: ['openOrder'],
  setup(props) {
    const sortedOrders = ref(props.orders ?? []);
    const properties = ['number', 'framework', 'version', 'applicationUrl', 'specificPlatform', 'thirdPartyTool', 'state'];
    
    watch(props, () => {
      sortedOrders.value = props.orders;
      sort('number', false);
    });

    function sort(propName, asc) {
      if (propName !== '')
        sortedOrders.value.sort((a, b) => {
          if (a[propName] < b[propName] ^ asc)
            return 1;
          if (a[propName] > b[propName] ^ asc)
            return -1;
          return 0
        });
    }

    return { sortedOrders, properties }
  }
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>