<template>
  <div>
    <div v-if="selectedClient" class="text-start cursor-pointer px-2 py-1 my-1 text-truncate border-primary border border-end-0 border-start-0 border-top-0 border-bottom-1">
      <div class="row">
        <div class="col-sm-8 col-12 d-flex">
          <div class="rounded-pill my-auto p-2 bg-gradient-primary text-center">
            <span class="text-white text-truncate">{{ selectedClient.id }}</span>
          </div>
          <div class="description ps-3">
            <p class="m-0 fw-bolder">{{ selectedClient.name }}</p>
            <span class="text-secondary text-xs font-weight-bold text-truncate">{{ selectedClient.email }}</span>
          </div>
        </div>
        <div class="col-sm-4 col-12 d-flex align-items-end justify-content-end">
          <div class="justify-content-end input-group">
            <button class="btn px-3 py-1 mb-0" :class="isMessages ? 'bg-gradient-primary' : 'btn-outline-primary'" type="button" @click="isMessages = true">
              <i :class="`ni ni-chat-round opacity-10 mt-2`"></i>
            </button>
            <button class="btn px-3 py-1 mb-0" :class="isMessages ? 'btn-outline-primary' : 'bg-gradient-primary'" type="button" @click="isMessages = false">
              <i :class="`ni ni-email-83 opacity-10 mt-2`"></i>
            </button>
          </div>
        </div>
      </div>
    </div>
    <div v-if="isMessages">
      <p v-if="!selectedClient">{{ $t('contactView.select') }}</p>
      <p v-else-if="messages.length === 0">{{ $t('contactView.haveNoMessages') }}</p>
      <message-line
        :message="message.text"
        :isMyMessage="message.fromClient"
        :time="message.dateTime"
        :prevDateTime="idx < messages.length-1 ? messages[idx+1].dateTime : null"
        :prevIsMyMessage="idx < messages.length-1 ? messages[idx+1].fromClient : null"
        v-for="(message, idx) in messages" :key="message.dateTime">
      </message-line>
    </div>
    <email-viewer v-else :emails="emails"></email-viewer>
  </div>
</template>

<script>
import { ref } from 'vue';
import MessageLine from './MessageLine.vue';
import EmailViewer from './EmailViewer.vue';

export default {
  name: 'ContactView',
  props: {
    selectedClient: Object,
    messages: Array,
    emails: Array
  },
  components: { MessageLine, EmailViewer },
  setup() {
    const isMessages = ref(true);

    return { isMessages }
  }
}
</script>

<style scoped>
.rounded-pill {
  width: fit-content;
}
</style>