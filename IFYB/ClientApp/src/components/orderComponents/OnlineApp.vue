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
      <span class="text-danger" v-if="showError && inputErrors.applicationUrl"><em><small>{{ $t(inputErrors.applicationUrl) }}</small></em></span>
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { validUrl } from '../../utils/Validate';
import { useInputError } from "../../store";

export default {
  name: 'OnlineApp',
  props: {
    modelValue: String,
    editable: Boolean,
    showError: Boolean,
  },
  emits:['update:modelValue'],
  setup(props, context){
    const isChecked = ref(props.modelValue !== null);
    const text = ref(props.modelValue);
    const { inputErrors, setInputError } = useInputError();

    if(isChecked.value) {
      setInputError('applicationUrl', validUrl(text.value));
    }

    watch(isChecked, () => {
      if (isChecked.value) {
        setInputError('applicationUrl', validUrl(text.value));
        context.emit('update:modelValue', text.value);
      } else {
        setInputError('applicationUrl', null);
        context.emit('update:modelValue', null);
      }
    });
    
    watch(text, () => {
      setInputError('applicationUrl', validUrl(text.value));
      context.emit('update:modelValue', text.value);
    });
    
    return { isChecked, text, inputErrors }
  }
}
</script>