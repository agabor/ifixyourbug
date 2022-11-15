<template>
  <search-bar v-model:modelValue="filteredOrders" :data="orders" :properties="properties"></search-bar>
  <div class="table-responsive">
    <table class="table align-items-center mb-0">
      <thead>
        <tr>
          <sortable-th title="number" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="creationTime" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="name" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="email" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="applicationUrl" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="flag" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
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
            <span class="text-secondary text-xs font-weight-bold">{{ $filters.dateTimeFormat(order.creationTime) }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.name }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.email }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ order.applicationUrl }}</span>
          </td>
          <td>
            <flag-component :flag="order.flag"></flag-component>
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
</template>

<script>
import { ref } from 'vue';
import SearchBar from '../components/SearchBar.vue';
import FlagComponent from '../components/FlagComponent.vue';
import SortableTh from './SortableTh.vue';
import StateBadge from './StateBadge.vue';

export default {
  name: 'AdminOrderList',
  components: { SearchBar, FlagComponent, SortableTh, StateBadge },
  props: {
    orders: Array
  },
  emits: ['openOrder'],
  setup(props) {
    const filteredOrders = ref(props.orders);
    const properties = ['number', 'applicationUrl', 'state', 'name', 'email'];
    const orderBy = ref('');
    const orderAsc = ref(false);

    sort('number');

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
          if(a[ordBy] === null)
            return 1;
          if(b[ordBy] === null)
            return -1;
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