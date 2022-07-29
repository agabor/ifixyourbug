<template>
  <search-bar v-model:modelValue="filteredOrders" :data="orders" :properties="properties"></search-bar>
  <div class="table-responsive">
    <table class="table align-items-center mb-0">
      <thead>
        <tr>
          <sortable-th title="number" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="name" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="email" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="framework" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="version" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="applicationUrl" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="specificPlatform" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="thirdPartyTool" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="state" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(order, idx) in filteredOrders" :key="idx">
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.number }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.name }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.email }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.framework == 0 ? 'Vue.js' : 'ASP.NET Core' }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.version }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.applicationUrl }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.specificPlatform }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.thirdPartyTool }}</span>
          </td>
          <td>
            <span class="badge badge-sm badge-dark" v-if="order.state == 0">{{ $t('orderList.submitted') }}</span>
            <span class="badge badge-sm badge-info" v-else-if="order.state == 1">{{ $t('orderList.accepted') }}</span>
            <span class="badge badge-sm badge-danger" v-else-if="order.state == 2">{{ $t('orderList.rejected') }}</span>
            <span class="badge badge-sm badge-light " v-else-if="order.state == 3">{{ $t('orderList.payed') }}</span>
            <span class="badge badge-sm badge-success" v-else-if="order.state == 4">{{ $t('orderList.completed') }}</span>
            <span class="badge badge-sm badge-warning" v-else-if="order.state == 5">{{ $t('orderList.refundable') }}</span>
            <span class="badge badge-sm badge-light" v-else-if="order.state == 6">{{ $t('orderList.canceled') }}</span>
          </td>
          <td class="align-middle text-center cursor-pointer" @click="$emit('openOrder', order)">
            <span class="text-secondary text-xs font-weight-bold">
              <i class="ni ni-bold-right opacity-10"></i>
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <p class="m-2" v-if="filteredOrders.length == 0">{{ $t('errors.noResult') }}</p>
</template>

<script>
import { ref, watch } from 'vue';
import SearchBar from '../components/SearchBar.vue';
import SortableTh from './SortableTh.vue';

export default {
  name: 'AdminOrderList',
  components: { SearchBar, SortableTh },
  props: {
    orders: Array
  },
  emits: ['openOrder'],
  setup(props) {
    const filteredOrders = ref(props.orders ?? []);
    const properties = ['number', 'framework', 'version', 'applicationUrl', 'specificPlatform', 'thirdPartyTool', 'state', 'name', 'email'];
    const orderBy = ref('');
    const orderAsc = ref(true);

    watch(props, () => {
      filteredOrders.value = props.orders;
    });

    function sort(propName) {
      if (orderBy.value === propName) {
        orderAsc.value = !orderAsc.value;
      } else {
        orderBy.value = propName;
      }
      const ordBy = orderBy.value;
      const asc = orderAsc.value;
      if (orderBy.value !== '')
        filteredOrders.value.sort((a, b) => {
          if (a[ordBy] < b[ordBy] ^ asc)
            return 1;
          if (a[ordBy] > b[ordBy] ^ asc)
            return -1;
          return 0
        });
    }

    return { filteredOrders, properties, orderBy, orderAsc, sort }
  }
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>