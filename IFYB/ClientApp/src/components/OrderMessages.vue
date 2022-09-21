<template>
  <div class="container">
    <div class="row">
      <div class="col-12 mx-auto my-4">
        <div class="card">
          <div class="card-body px-lg-5 py-lg-5 text-center">
            <div class="info mb-4">
              <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                <i :class="`ni ni-chat-round opacity-10 mt-2`"></i>
              </div>
            </div>
            <h2>{{ $t('orderMessages.title') }}</h2>
            <div class="d-flex align-items-center mb-4">
              <textarea id="messageInput" class="form-control" :rows="rows" type="text"
                single-line
                v-model="newMessage"
                :placeholder="$t('orderMessages.newMessagePlaceholder')"
                @keyup.enter.shift.exact.prevent="addLine()"
                @keyup.enter.exact.prevent="addLine()">
              </textarea>
              <i class="ni ni-send opacity-10 fs-4 mx-2 cursor-pointer fg-gradient-primary send-btn" @click="trySubmitMessage()"></i>
            </div>
            <message-line
              :message="message.text"
              :isMyMessage="message.fromClient"
              :time="message.dateTime"
              :prevDateTime="idx < messages.length-1 ? messages[idx+1].dateTime : null"
              :prevIsMyMessage="idx < messages.length-1 ? messages[idx+1].fromClient : null"
              v-for="(message, idx) in messages" :key="message.dateTime"></message-line>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import MessageLine from './MessageLine.vue';
import { required } from '../utils/Validate';
import { useMessages } from "../store";

export default {
  name: 'OrderMessages',
  props: {
    messages: Array
  },
  emits: ['submitMessage'],
  components: { MessageLine },
  setup(props, context) {
    const { tm } = useMessages();
    const newMessage = ref('');
    const rows = ref(1);


    async function trySubmitMessage() {
      let err = required(newMessage.value, tm('errors.requiredNewMessage'), 'messageInput');
      if(!err) {
        context.emit('submitMessage', newMessage.value);
      }
      newMessage.value = '';
      rows.value = 1;
    }

    function addLine() {
      rows.value++;
    }

    return { newMessage, rows, trySubmitMessage, addLine }
  }
}
</script>

<style scoped>
.fg-gradient-primary {
  background-image: linear-gradient(90deg, #ed3269 0%, #f05f3e 100%);
  background-clip: text;
  color: transparent;
}
.fg-gradient-primary:hover {
  background-image: linear-gradient(90deg, #e0245a 0%, #e34e30 100%);
}
</style>