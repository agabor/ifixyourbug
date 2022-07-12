<template>
  <div class="row">
    <div v-if="selectedClient" class="text-start cursor-pointer px-2 py-1 my-1 text-truncate border-primary border border-end-0 border-start-0 border-top-0 border-bottom-1">
      <div class="d-flex">
        <div class="d-flex rounded-pill my-auto p-2 bg-gradient-primary text-center align-items-center justify-content-center">
          <span class="text-white">{{ selectedClient.id }}</span>
        </div>
        <div class="description ps-3">
          <p class="m-0 fw-bolder">{{ selectedClient.name }}</p>
          <span class="text-secondary text-xs font-weight-bold">{{ selectedClient.email }}</span>
        </div>
      </div>
    </div>
    <p v-if="!selectedClient">Select one</p>
    <p v-else-if="messages.length == 0">Have no messages</p>
    <message-line class="col-12"
      :message="message.text"
      :isMyMessage="message.fromClient"
      :time="message.dateTime"
      :prevDateTime="idx < messages.length-1 ? messages[idx+1].dateTime : null"
      :prevIsMyMessage="idx < messages.length-1 ? messages[idx+1].fromClient : null"
      v-for="(message, idx) in messages" :key="message.dateTime">
    </message-line>
  </div>
</template>

<script>
import MessageLine from './MessageLine.vue';

export default {
  name: 'ContactMessages',
  props: {
    selectedClient: Object,
    messages: Array
  },
  components: { MessageLine }
}
</script>
