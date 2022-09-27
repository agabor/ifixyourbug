<template>
  <email-form v-if="page === 'email'" v-model:modelValue="user.email" v-model:activeButton="activeButton" :error="error" :progress="progress" :showPolicy="showPolicy" v-model:acceptedPolicy="accepted" :showRequired="showRequired" @update:modelValue="submitEmail"></email-form>
  <two-fa v-if="page === 'auth'" v-model:modelValue="user.auth" :email="user.email" :error="error" @update:modelValue="tryAuthentication" @cancel="cancel"></two-fa>
  <name-form v-if="page === 'name'" v-model:modelValue="user.name" v-model:activeButton="activeButton" :error="error" @update:modelValue="setName"></name-form>
  <authentication-failed v-if="page === 'failed'"></authentication-failed>
</template>

<script>
import { ref, watch } from 'vue';
import EmailForm from '../components/EmailForm.vue';
import TwoFa from '../components/2FA.vue';
import NameForm from '../components/NameForm.vue';
import AuthenticationFailed from '../components/AuthenticationFailed.vue';

export default {
  name: 'AuthenticationForm',
  components: { EmailForm, TwoFa, NameForm, AuthenticationFailed },
  props: {
    page: String,
    error: String,
    progress: Number,
    showPolicy: Boolean,
    acceptedPolicy: Boolean,
    showRequired: Boolean,
    activeBtn: Boolean
  },
  emits: [ 'submitEmail', 'changePolicy', 'authentication', 'setName', 'cancel', 'update:activeBtn' ],
  setup(props, context) {
    const user = ref({});
    const accepted = ref(props.acceptedPolicy);
    const activeButton = ref(props.activeBtn ?? true);

    watch(props, () => {
      activeButton.value = props.activeBtn;
      accepted.value = props.acceptedPolicy;
    })

    watch(activeButton, () => {
      context.emit('update:activeBtn', activeButton.value);
    })

    watch(accepted, () => {
      context.emit('changePolicy', accepted.value);
    })

    function submitEmail(email) {
      user.value.email = email;
      context.emit('submitEmail', email);
    }

    async function tryAuthentication(code) {
      user.value.auth = code;
      context.emit('authentication', user.value.auth);
    }

    function setName(name) {
      user.value.name = name;
      context.emit('setName', name);
    }

    function cancel() {
      user.value = {};
      context.emit('cancel');
    }

    return { user, accepted, activeButton, submitEmail, tryAuthentication, setName, cancel }
  }
}
</script>