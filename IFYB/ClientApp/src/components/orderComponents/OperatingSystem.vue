<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" v-model="isSpecific" @change="changeIsSpecificOpSystem">
      <label class="form-check-label" for="specific-op-system-input">Is the issue specific to an operating system?</label>
    </div>
  </div>
  <label v-if="isSpecific">Operating system*</label>
  <div class="col-md-12 d-flex pe-2" v-if="isSpecific">
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio0" :value="0" v-model="os" @change="$emit('changeOs', os)">
        <label class="form-check-label" for="osRadio0">
          Windows
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio1" :value="1" v-model="os" @change="$emit('changeOs', os)">
        <label class="form-check-label" for="osRadio1">
          Linux
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio2" :value="2" v-model="os" @change="$emit('changeOs', os)">
        <label class="form-check-label" for="osRadio2">
          macOS
        </label>
      </div>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isSpecific">
    <div class="form-group mb-0">
      <label>Operating system version*</label>
      <input id="op-system-name-input" class="form-control" placeholder="Operating system version" type="text" v-model="version" @input="$emit('changeVersion', version)">
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'OperatingSystem',
  emits:['changeOs', 'changeVersion', 'changeIsSpecificOpSystem'],
  props: {
    isSpecificOpSystem: Boolean,
    operatingSystem: Number,
    operatingVersion: String
  },
  setup(props, context) {
    const isSpecific = ref(props.operatingSystem);
    const os = ref(props.operatingSystem);
    const version = ref(props.operatingVersion);

    function changeIsSpecificOpSystem() {
      os.value = undefined;
      context.emit('changeIsSpecificOpSystem', isSpecific);
    }
    
    return { isSpecific, os, changeIsSpecificOpSystem, version };
  }
}
</script>