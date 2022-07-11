<template>
  <table class="table align-items-center mb-0">
    <thead>
      <tr>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('clientList.name') }}</th>
        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('clientList.email') }}</th>
        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">{{ $t('clientList.messages') }}</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(client, idx) in clients" :key="idx">
        <td>
          <span class="text-secondary text-xs font-weight-bold">{{ client.name }}</span>
        </td>
        <td>
          <span class="text-secondary text-xs font-weight-bold">{{ client.email }}</span>
        </td>
        <td class="align-middle text-center cursor-pointer" @click="$emit('openClient', client)">
          <span class="text-secondary text-xs font-weight-bold">
            <i class="ni ni-chat-round opacity-10"></i>
          </span>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>

export default {
  name: 'clientList',
  props: {
    clients: Array
  },
  emits: ['openClient']
}
</script>

<style scoped>
  td > span {
    white-space: pre-line;
  }
</style>