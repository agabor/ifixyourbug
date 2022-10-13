<template>
  <search-bar v-model:modelValue="filteredRequests" :data="requests" :properties="properties"></search-bar>
  <div class="table-responsive">
    <table class="table align-items-center mb-0">
      <thead>
        <tr>
          <sortable-th title="number" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="dateTime" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="name" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="email" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="url" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <sortable-th title="solved" :orderBy="orderBy" :orderAsc="orderAsc" @sort="sort"></sortable-th>
          <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(request, idx) in filteredRequests" :key="idx">
          <td>
            <span class="text-secondary text-xs font-weight-bold">#{{ request.number }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ $filters.dateTimeFormat(request.dateTime) }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ request.name }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ request.email }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold">{{ request.url }}</span>
          </td>
          <td>
            <span class="text-secondary text-xs font-weight-bold" :class="{ 'text-danger': !request.solved, 'text-success': request.solved }">{{ request.solved }}</span>
          </td>
          <td class="align-middle text-center cursor-pointer" @click="$emit('openRequest', request)">
            <span class="text-secondary text-xs font-weight-bold">
              <i class="ni ni-bold-right opacity-10"></i>
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <p class="m-2" v-if="filteredRequests.length == 0">{{ $t('errors.noResult') }}</p>
</template>

<script>
import { ref } from 'vue';
import SearchBar from '../components/SearchBar.vue';
import SortableTh from './SortableTh.vue';

export default {
  name: 'AdminOrderList',
  components: { SearchBar, SortableTh },
  props: {
    requests: Array
  },
  emits: ['openRequest'],
  setup(props) {
    const filteredRequests = ref(props.requests);
    const properties = ['number', 'dateTime', 'name', 'email', 'url', 'solved'];
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
        filteredRequests.value.sort((a, b) => {
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

    return { filteredRequests, properties, orderBy, orderAsc, sort }
  }
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>