<template>
  <div class="row">
    <div class="col-12">
      <div class="form-check form-switch">
        <input class="form-check-input" type="checkbox" id="available-app-input" v-model="isChecked" :disabled="!editable">
        <label class="form-check-label" for="available-app-input">{{ $t('onlineApp.isAvailable') }}</label>
      </div>
    </div>
    <div class="col-12 mb-3" v-if="isChecked">
      <div class="form-group mb-0">
        <label>{{ $t('onlineApp.appUrl') }}*</label>
        <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.applicationUrl)}" :placeholder="$t('onlineApp.appUrl')" type="text" v-model="text" :disabled="!editable">
      </div>
      <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.applicationUrl }}</small></em></span>
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { required } from '../../utils/Validate';
import { useInputError, useMessages } from "../../store";

export default {
  name: 'OnlineApp',
  emits:['update:modelValue'],
  props: {
    modelValue: String,
    editable: Boolean,
    showError: Boolean,
  },
  setup(props, context){
    const isChecked = ref(props.modelValue !== null);
    const text = ref(props.modelValue ?? '');
    const { tm } = useMessages();
    const { inputErrors, setInputError } = useInputError();

    if(isChecked.value) {
      setInputError('applicationUrl', required(text.value, tm('errors.requiredAppUrl')));
    }

    watch(isChecked, () => {
      if (isChecked.value) {
        context.emit('update:modelValue', text.value);
        setInputError('applicationUrl', required(text.value, tm('errors.requiredAppUrl')));
      } else {
        context.emit('update:modelValue', null);
        setInputError('applicationUrl', null);
      }
    });
    watch(text, () => {
      context.emit('update:modelValue', text.value);
      setInputError('applicationUrl', required(text.value, tm('errors.requiredAppUrl')));
    });
    return { isChecked, text, inputErrors }
  }
}
</script>