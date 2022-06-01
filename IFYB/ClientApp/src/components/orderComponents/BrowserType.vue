<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" :value="isSpecificBrowser" @change="updateIsSpecificBrowser">
      <label class="form-check-label" for="specific-op-system-input">{{ $t('browserType.isSpecific') }}</label>
    </div>
  </div>
  <label v-if="isSpecificBrowser">{{ $t('browserType.label') }}</label>
  <div class="col-md-12 d-flex pe-2" v-if="isSpecificBrowser">
    <div class="form-check me-3" v-for="n in optionCount" :key="n">
      <input class="form-check-input" type="radio" name="browserTypeRadio" :id="`browserTypeRadio${n}`" :value="n-1" v-model="browserType" @change="$emit('update:browser', browserType)">
      <label class="form-check-label" :for="`browserTypeRadio${n}`">{{ $t(`browserType.option${n}`) }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isSpecificBrowser">
    <div class="form-group mb-0">
      <label>{{ $t('browserType.version') }}*</label>
      <input id="browser-system-name-input" class="form-control" :placeholder="$t('browserType.version')" type="text" :value="version" @input="$emit('update:version', $event.target.value)">
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'BrowserType',
  emits:['update:browser', 'update:version', 'update:isSpecificBrowser'],
  props: {
    isSpecificBrowser: Boolean,
    browser: Number,
    version: String
  },
  setup(props, context) {
    const optionCount = 3;
    const browserType = ref(props.operatingSystem);

    function updateIsSpecificBrowser() {
      browserType.value = undefined;
      context.emit('update:browser', undefined);
      context.emit('update:version', undefined);
      context.emit('update:isSpecificBrowser', !props.isSpecificBrowser);
    }
    
    return { optionCount, browserType, updateIsSpecificBrowser };
  }
}
</script>