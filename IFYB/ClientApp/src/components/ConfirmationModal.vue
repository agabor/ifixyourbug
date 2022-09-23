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
            <textarea id="message-input" class="form-control" :rows="rows" type="text" :class="{'is-invalid': (showError && !!inputErrors.confirmMessage)}"
                single-line
                v-model="text"
                :placeholder="$t('confirm.message')"
                @keyup.enter.shift.exact.prevent="addLine()"
                @keyup.enter.exact.prevent="addLine()">
              </textarea>
            <span class="text-danger" v-if="showError && inputErrors.confirmMessage"><em><small>{{ inputErrors.confirmMessage }}</small></em></span>
          </div>
        </div>
        <div class="modal-footer justify-content-between">
          <one-click-btn v-model:active="activeBtn" :text="$t('confirm.cancel')" class="bg-gradient-dark" @click="$emit('cancel')"></one-click-btn>
          <one-click-btn v-model:active="activeBtn" :text="$t('confirm.confirm')" class="bg-gradient-primary" @click="confirm"></one-click-btn>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue';
import { required } from '../utils/Validate';
import { useInputError, useMessages } from "../store";
import OneClickBtn from './OneClickBtn.vue';

export default {
  name: 'ConfirmationModal',
  components: { OneClickBtn },
  props: {
      modelValue: String,
      title: String,
      description: String,
      showError: Boolean
  },
  emits: ['update:modelValue', 'confirm', 'cancel'],
  setup(props, context) {
    const text = ref(props.modelValue ?? '');
    const { tm } = useMessages();
    const { inputErrors, setInputError } = useInputError();
    const activeBtn = ref(true);
    const rows = ref(1);

    setInputError('confirmMessage', required(text.value, tm('errors.requiredMessage')));

    watch(text, () => {
      context.emit('update:modelValue', text.value);
      setInputError('confirmMessage', required(text.value, tm('errors.requiredMessage')));
    });

    function addLine() {
      rows.value++;
    }

    function confirm() {
      context.emit('confirm');
      activeBtn.value = inputErrors.value.confirmMessage != null;
    }
    
    return { text, inputErrors, activeBtn, rows, addLine, confirm };
  }
}
</script>