<template>
  <search-bar v-model:modelValue="filteredOrders" :data="orders" :properties="properties"></search-bar>
  <button type="button" class="btn bg-gradient-primary mt-4" @click="$router.push('/new-order')">
    {{ $t('orderList.addNewOrder') }}
  </button>
  <div class="row">
    <div class="col-12">
      <div class="card mt-3" v-for="(order, idx) in filteredOrders" :key="idx">
        <div class="row">
          <div class="col-lg-9 col-md-8 col-12 ps-lg-0 my-auto">
            <div class="card-body text-start">
              <h5 class="mb-0">Order number: {{ order.number }}</h5>
              <div class="d-flex">
                <h6 class="text-info mb-0">{{ order.framework == 0 ? 'Vue.js' : 'ASP.NET Core' }} {{ order.version }}</h6>
                <p class="mx-1 mb-0" v-if="order.specificPlatform">| {{ order.specificPlatform }}</p>
              </div>
              <p class="mb-0" v-if="order.applicationUrl">{{ $t('orderList.applicationUrl') }}: <a class="text-decoration-underline" :href="order.applicationUrl" >{{ order.applicationUrl }}</a></p>
              <p class="mb-0" v-if="order.thirdPartyTool">{{ $t('orderList.thirdPartyTool') }}: {{ order.thirdPartyTool }}</p>
              <span class="badge badge-sm badge-dark mt-3" v-if="order.state == 0">{{ $t('orderList.submitted') }}</span>
              <span class="badge badge-sm badge-info mt-3" v-else-if="order.state == 1">{{ $t('orderList.accepted') }}</span>
              <span class="badge badge-sm badge-danger mt-3" v-else-if="order.state == 2">{{ $t('orderList.rejected') }}</span>
              <span class="badge badge-sm badge-light mt-3" v-else-if="order.state == 3">{{ $t('orderList.payed') }}</span>
              <span class="badge badge-sm badge-success mt-3" v-else-if="order.state == 4">{{ $t('orderList.completed') }}</span>
              <span class="badge badge-sm badge-warning mt-3" v-else-if="order.state == 5">{{ $t('orderList.refundable') }}</span>
            </div>
          </div>
          <div class="col-lg-3 col-md-4 col-12 pe-4">
            <div class="d-flex justify-content-end">
              <button type="button" class="btn btn-outline-secondary my-4 d-flex align-items-center" @click="$router.push(`/checkout/${order.paymentToken}`)" v-if="order.paymentToken && order.state == 1">
                {{ $t('orderList.pay') }}
                <span class="text-secondary text-xs font-weight-bold ms-2">
                  <i class="ni ni-cart opacity-10"></i>
                </span>
              </button>
              <button type="button" class="btn bg-gradient-primary my-4 d-flex align-items-center ms-2" @click="$emit('openOrder', order)">
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
  <p class="m-2" v-if="filteredOrders.length == 0">{{ $t('errors.noResult') }}</p>
</template>

<script>
import { ref, watch } from 'vue';
import SearchBar from '../components/SearchBar.vue';

export default {
  name: 'OrderList',
  components: { SearchBar },
  props: {
    orders: Array
  },
  emits: ['openOrder'],
  setup(props) {
    const filteredOrders = ref(props.orders ?? []);
    const properties = ['number', 'framework', 'version', 'applicationUrl', 'specificPlatform', 'thirdPartyTool', 'state'];
    
    watch(props, () => {
      filteredOrders.value = props.orders;
      sort('number', false);
    });

    function sort(propName, asc) {
      if (propName !== '')
        filteredOrders.value.sort((a, b) => {
          if (a[propName] < b[propName] ^ asc)
            return 1;
          if (a[propName] > b[propName] ^ asc)
            return -1;
          return 0
        });
    }

    return { filteredOrders, properties }
  }
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>