<template>
  <div class="row">
    <div class="col-12">
      <div class="form-check">
        <input class="form-check-input" type="checkbox" id="accept-terms" v-model="isChecked">
        <label class="form-check-label" for="accept-terms">{{ $t('policies.iAcceptAndRead') }}<a class="mx-1 text-decoration-underline" @click="toTerms">{{ $t('policies.termsAndConditions') }}</a></label>
      </div>
      <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.acceptTerms }}</small></em></span>
    </div>
  </div>
</template>


<script>
import { ref, watch } from 'vue'
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";
import { event } from 'vue-gtag';

export default {
  name: 'AcceptTerms',
  props: {
    showError: Boolean,
  },
  setup(){
    const isChecked = ref(null);
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();

    setInputError('acceptTerms', isChecked.value ? null : tm('errors.acceptTerms'));

    watch(isChecked, () => {
      event('set-order-accept-terms', { 'value': isChecked });
      if (isChecked.value) {
        setInputError('acceptTerms', null);
      } else {
        setInputError('acceptTerms', isChecked.value ? null : tm('errors.acceptTerms'));
      }
    });

    function toTerms() {
      event('navigate-to-terms-and-conditions');
      window.open('/terms-and-conditions', '_blank');
    }

    return { isChecked, inputErrors, toTerms }
  }
}
</script>