<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" :value="isSpecificOpSystem" @change="updateIsSpecificOpSystem">
      <label class="form-check-label" for="specific-op-system-input">{{ $t('operatingSystem.isSpecific') }}</label>
    </div>
  </div>
  <label v-if="isSpecificOpSystem">{{ $t('operatingSystem.label') }}</label>
  <div class="col-md-12 d-flex pe-2" v-if="isSpecificOpSystem">
    <div class="form-check me-3" v-for="n in optionCount" :key="n">
      <input class="form-check-input" type="radio" name="osRadio" :id="`osRadio${n}`" :value="$t(`operatingSystem.option${n}`)" v-model="os" @change="$emit('update:operatingSystem', os)">
      <label class="form-check-label" :for="`osRadio${n}`">{{ $t(`operatingSystem.option${n}`) }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isSpecificOpSystem">
    <div class="form-group mb-0">
      <label>{{ $t('operatingSystem.version') }}*</label>
      <input id="op-system-name-input" class="form-control" :placeholder="$t('operatingSystem.version')" type="text" :valuel="version" @input="$emit('update:version', $event.target.value)">
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'OperatingSystem',
  emits:['update:operatingSystem', 'update:version', 'update:isSpecificOpSystem'],
  props: {
    isSpecificOpSystem: Boolean,
    operatingSystem: Number,
    version: String
  },
  setup(props, context) {
    const optionCount = 3;
    const os = ref(props.operatingSystem);

    function updateIsSpecificOpSystem() {
      os.value = undefined;
      context.emit('update:operatingSystem', undefined);
      context.emit('update:version', undefined);
      context.emit('update:isSpecificOpSystem', !props.isSpecificOpSystem);
    }
    
    return { optionCount, os, updateIsSpecificOpSystem };
  }
}
</script>