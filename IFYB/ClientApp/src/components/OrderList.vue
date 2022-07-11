<template>
  <table class="table align-items-center mb-0">
    <thead>
      <tr>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('orderList.number') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('orderList.framework') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">{{ $t('orderList.version') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">{{ $t('orderList.applicationUrl') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">{{ $t('orderList.specificPlatform') }}</th>
        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('orderList.thirdPartyTool') }}</th>
        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('orderList.state') }}</th>
        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"></th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(order, idx) in orders" :key="idx">
        <td>
          <span class="text-secondary text-xs font-weight-bold">{{ order.number }}</span>
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
        </td>
        <td class="align-middle text-center cursor-pointer" @click="$emit('openOrder', order)">
          <span class="text-secondary text-xs font-weight-bold">
            <i class="ni ni-bold-right opacity-10"></i>
          </span>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>

export default {
  name: 'OrderList',
  props: {
    orders: Array
  },
  emits: ['openOrder']
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>