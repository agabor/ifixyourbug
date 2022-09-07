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
            <span class="text-secondary text-xs font-weight-bold">#{{ order.number }}</span>
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
            <state-badge class="badge badge-sm" :state="order.state" :isSimple="true"></state-badge>
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
import StateBadge from './StateBadge.vue';

export default {
  name: 'AdminOrderList',
  components: { SearchBar, SortableTh, StateBadge },
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