<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" v-model="isChecked" :disabled="!editable">
      <label class="form-check-label" for="specific-op-system-input">{{ $t('operatingSystem.isSpecific') }}</label>
    </div>
  </div>
  <label v-if="isChecked">{{ $t('operatingSystem.label') }}</label>
  <div class="col-md-12 d-flex pe-2" v-if="isChecked">
    <div class="form-check me-3" v-for="n in optionCount" :key="n">
      <input class="form-check-input" type="radio" name="osRadio" :id="`osRadio${n}`" :value="$t(`operatingSystem.option${n}`)" v-model="os" :disabled="!editable">
      <label class="form-check-label" :for="`osRadio${n}`">{{ $t(`operatingSystem.option${n}`) }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isChecked">
    <div class="form-group mb-0">
      <label>{{ $t('operatingSystem.version') }}*</label>
      <input id="op-system-name-input" class="form-control" :placeholder="$t('operatingSystem.version')" type="text" v-model="osVersion" :disabled="!editable">
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
export default {
  name: 'OperatingSystem',
  emits:['update:modelValue', 'update:version'],
  props: {
    modelValue: String,
    version: String,
    editable: Boolean
  },
  setup(props, context) {
    const isChecked = ref(props.modelValue !== null);
    const optionCount = 3;
    const os = ref(props.modelValue);
    const osVersion = ref(props.version);

    watch(isChecked, () => {
      if (isChecked.value) {
        context.emit('update:modelValue', os.value);
      } else {
        context.emit('update:modelValue', null);
      }
    });
    watch(os, () => {
        context.emit('update:modelValue', os.value);
    });
    watch(osVersion, () => {
        context.emit('update:version', osVersion.value);
    });
    
    return { isChecked, optionCount, os, osVersion };
  }
}
</script>