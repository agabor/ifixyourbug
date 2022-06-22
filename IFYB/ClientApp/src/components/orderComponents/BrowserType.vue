<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" v-model="isChecked" :disabled="!editable">
      <label class="form-check-label" for="specific-op-system-input">{{ $t('browserType.isSpecific') }}</label>
    </div>
  </div>
  <label v-if="isChecked">{{ $t('browserType.label') }}</label>
  <div class="col-md-12 d-flex pe-2" v-if="isChecked">
    <div class="form-check me-3" v-for="n in optionCount" :key="n">
      <input class="form-check-input" type="radio" name="browserRadio" :id="`browserRadio${n}`" :value="$t(`browserType.option${n}`)" v-model="browser" :disabled="!editable">
      <label class="form-check-label" :for="`browserRadio${n}`">{{ $t(`browserType.option${n}`) }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isChecked">
    <div class="form-group mb-0">
      <label>{{ $t('browserType.version') }}*</label>
      <input id="browser-system-name-input" class="form-control" :placeholder="$t('browserType.version')" type="text" v-model="browserVersion" :disabled="!editable">
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
export default {
  name: 'browserType',
  emits:['update:modelValue', 'update:version'],
  props: {
    modelValue: String,
    version: String,
    editable: Boolean
  },
  setup(props, context) {
    const isChecked = ref(props.modelValue !== null);
    const optionCount = 3;
    const browser = ref(props.browser);
    const browserVersion = ref(props.version);

    watch(isChecked, () => {
      if (isChecked.value) {
        context.emit('update:modelValue', browser.value);
      } else {
        context.emit('update:modelValue', null);
      }
    });
    watch(browser, () => {
        context.emit('update:modelValue', browser.value);
    });
    watch(browserVersion, () => {
        context.emit('update:version', browserVersion.value);
    });
    
    return { isChecked, optionCount, browser, browserVersion };
  }
}
</script>