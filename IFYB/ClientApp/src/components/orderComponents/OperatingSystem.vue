<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" :value="isSpecificOpSystem" @change="updateIsSpecificOpSystem">
      <label class="form-check-label" for="specific-op-system-input">Is the issue specific to an operating system?</label>
    </div>
  </div>
  <label v-if="isSpecificOpSystem">Operating system*</label>
  <div class="col-md-12 d-flex pe-2" v-if="isSpecificOpSystem">
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio0" :value="0" v-model="os" @change="$emit('update:operatingSystem', os)">
        <label class="form-check-label" for="osRadio0">
          Windows
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio1" :value="1" v-model="os" @change="$emit('update:operatingSystem', os)">
        <label class="form-check-label" for="osRadio1">
          Linux
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio2" :value="2" v-model="os" @change="$emit('update:operatingSystem', os)">
        <label class="form-check-label" for="osRadio2">
          macOS
        </label>
      </div>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isSpecificOpSystem">
    <div class="form-group mb-0">
      <label>Operating system version*</label>
      <input id="op-system-name-input" class="form-control" placeholder="Operating system version" type="text" :valuel="version" @input="$emit('update:version', $event.target.value)">
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
    const os = ref(props.operatingSystem);

    function updateIsSpecificOpSystem() {
      os.value = undefined;
      context.emit('update:operatingSystem', undefined);
      context.emit('update:version', undefined);
      context.emit('update:isSpecificOpSystem', !props.isSpecificOpSystem);
    }
    
    return { os, updateIsSpecificOpSystem };
  }
}
</script>