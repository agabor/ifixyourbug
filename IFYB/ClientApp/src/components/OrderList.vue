<template>
  <table class="table align-items-center mb-0">
    <thead>
      <tr>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('orderList.framework') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">{{ $t('orderList.version') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">{{ $t('orderList.applicationUrl') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">{{ $t('orderList.specificPlatform') }}</th>
        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('orderList.thirdPartyTool') }}</th>
        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('orderList.gitAccessId') }}</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(order, idx) in orders" :key="idx">
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
        <td class="align-middle text-center text-sm">
          <span class="badge badge-sm badge-success">{{ order.thirdPartyTool }}</span>
        </td>
        <td class="align-middle text-center">
          <span class="text-secondary text-xs font-weight-bold">{{ order.gitAccessId }}</span>
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
  }
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>