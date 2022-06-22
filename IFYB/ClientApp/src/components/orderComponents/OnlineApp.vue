<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="available-app-input" v-model="isChecked" :disabled="!editable">
      <label class="form-check-label" for="available-app-input">{{ $t('onlineApp.isAvailable') }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isChecked">
    <div class="form-group mb-0">
      <label>{{ $t('onlineApp.appUrl') }}*</label>
      <input id="app-url-input" class="form-control" :placeholder="$t('onlineApp.appUrl')" type="text" v-model="text" :disabled="!editable">
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'

export default {
  name: 'OnlineApp',
  emits:['update:modelValue'],
  props: {
    modelValue: String,
    editable: Boolean
  },
  setup(props, context){
    const isChecked = ref(props.modelValue !== null);
    const text = ref(props.modelValue ?? '');
    watch(isChecked, () => {
      if (isChecked.value) {
        context.emit('update:modelValue', text.value);
      } else {
        context.emit('update:modelValue', null);
      }
    });
    watch(text, () => {
        context.emit('update:modelValue', text.value);
    });
    return { isChecked, text }
  }
}
</script>