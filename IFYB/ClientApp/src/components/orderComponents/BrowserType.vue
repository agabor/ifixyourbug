<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" :value="isSpecificBrowser" @change="updateIsSpecificBrowser">
      <label class="form-check-label" for="specific-op-system-input">Is the issue specific to an browser?</label>
    </div>
  </div>
  <label v-if="isSpecificBrowser">Browser type*</label>
  <div class="col-md-12 d-flex pe-2" v-if="isSpecificBrowser">
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio0" :value="0" v-model="browserType" @change="$emit('update:browser', browserType)">
        <label class="form-check-label" for="osRadio0">
          Chrome
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio1" :value="1" v-model="browserType" @change="$emit('update:browser', browserType)">
        <label class="form-check-label" for="osRadio1">
          Safari
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio2" :value="2" v-model="browserType" @change="$emit('update:browser', browserType)">
        <label class="form-check-label" for="osRadio2">
          Firefox
        </label>
      </div>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isSpecificBrowser">
    <div class="form-group mb-0">
      <label>Browser version*</label>
      <input id="browser-system-name-input" class="form-control" placeholder="Browser version" type="text" :value="version" @input="$emit('update:version', $event.target.value)">
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
    const browserType = ref(props.operatingSystem);

    function updateIsSpecificBrowser() {
      browserType.value = undefined;
      context.emit('update:browser', undefined);
      context.emit('update:version', undefined);
      context.emit('update:isSpecificBrowser', !props.isSpecificBrowser);
    }
    
    return { browserType, updateIsSpecificBrowser };
  }
}
</script>