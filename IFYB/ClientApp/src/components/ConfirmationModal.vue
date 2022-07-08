<template>
  <div class="modal fade show d-block mt-5" id="confirmationModal" tabindex="-1" aria-labelledby="confirmationModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="confirmationModalLabel">{{ title }}</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          {{ description }}
          <div class="mt-3" v-if="modelValue !== null">
            <label for="message-input">{{ $t('confirm.message') }}*</label>
            <input id="message-input" class="form-control" :class="{'is-invalid': (showError && !!inputErrors.confirmMessage)}" :placeholder="$t('confirm.message')" type="text" v-model="text">
            <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.confirmMessage }}</small></em></span>
          </div>
        </div>
        <div class="modal-footer justify-content-between">
          <button type="button" class="btn bg-gradient-dark" data-bs-dismiss="modal" @click="$emit('cancel')">{{ $t('confirm.cancel') }}</button>
          <button type="button" class="btn bg-gradient-primary" @click="$emit('confirm')">{{ $t('confirm.confirm') }}</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue';
import { required } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../store";

export default {
  name: 'ConfirmationModal',
  props: {
    modelValue: String,
    title: String,
    description: String,
    showError: Boolean
  },
  emits: ['confirm', 'cancel' ],
  setup (props, context) {
    const text = ref(props.modelValue ?? '');
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();
    
    setInputError('confirmMessage', required(text.value, tm('errors.requiredMessage')));

    watch(text, () => {
      context.emit('update:modelValue', text.value);
      setInputError('confirmMessage', required(text.value, tm('errors.requiredMessage')));
    });

    return { text, inputErrors }
  }
}
</script>