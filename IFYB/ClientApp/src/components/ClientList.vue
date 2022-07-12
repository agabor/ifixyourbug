<template>
  <div class="text-start cursor-pointer px-2 py-1 my-1 text-truncate" :class="{'selected': selectedClient == client}" v-for="(client, idx) in clients" :key="idx" @click="$emit('selectClient', client)">
    <p class="m-0 fw-bolder">{{ client.name }}</p>
    <span class="text-secondary text-xs font-weight-bold">{{ client.email }}</span>
  </div>
</template>

<script>

export default {
  name: 'clientList',
  props: {
    clients: Array,
    selectedClient: Object
  },
  emits: ['selectClient']
}
</script>

<style scoped>
  .cursor-pointer:hover {
    background-color:whitesmoke;
    border-radius: 10px;
  }
  .selected {
    background-color:whitesmoke;
    border-radius: 10px;
  }
</style>