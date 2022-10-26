<template>
  <email-form v-if="page === 'email'" :isClient="isClient"></email-form>
  <two-fa v-if="page === 'auth'" :isClient="isClient"></two-fa>
  <name-form v-if="page === 'name'" :isClient="isClient"></name-form>
  <authentication-failed v-if="page === 'failed'" :isClient="isClient"></authentication-failed>
</template>

<script>
import EmailForm from '../components/EmailForm.vue';
import TwoFa from '../components/2FA.vue';
import NameForm from '../components/NameForm.vue';
import AuthenticationFailed from '../components/AuthenticationFailed.vue';
import { useAuthenticator } from '@/store/authentication';

export default {
  name: 'AuthenticationForm',
  components: { EmailForm, TwoFa, NameForm, AuthenticationFailed },
  props: {
    isClient: Boolean
  },
  setup(props) {
    const { page } = useAuthenticator(props.isClient);

    return { page }
  }
}
</script>