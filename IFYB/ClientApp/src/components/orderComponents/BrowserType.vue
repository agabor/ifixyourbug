<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" v-model="isSpecific" @change="changeIsSpecificBrowser">
      <label class="form-check-label" for="specific-op-system-input">Is the issue specific to an browser?</label>
    </div>
  </div>
  <label v-if="isSpecific">Browser type*</label>
  <div class="col-md-12 d-flex pe-2" v-if="isSpecific">
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio0" :value="0" v-model="browserType" @change="$emit('changeBrowser', browserType)">
        <label class="form-check-label" for="osRadio0">
          Chrome
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio1" :value="1" v-model="browserType" @change="$emit('changeBrowser', browserType)">
        <label class="form-check-label" for="osRadio1">
          Safari
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="osRadio" id="osRadio2" :value="2" v-model="browserType" @change="$emit('changeBrowser', browserType)">
        <label class="form-check-label" for="osRadio2">
          Firefox
        </label>
      </div>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isSpecific">
    <div class="form-group mb-0">
      <label>Browser version*</label>
      <input id="browser-system-name-input" class="form-control" placeholder="Browser version" type="text" v-model="version" @input="$emit('changeVersion', version)">
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'BrowserType',
  emits:['changeBrowser', 'changeVersion', 'changeIsSpecificBrowser'],
  props: {
    isSpecificBrowser: Boolean,
    browser: Number,
    browserVersion: String
  },
  setup(props, context) {
    const isSpecific = ref(props.operatingSystem);
    const browserType = ref(props.operatingSystem);
    const version = ref(props.operatingVersion);

    function changeIsSpecificBrowser() {
      browserType.value = undefined;
      version.value = undefined;
      context.emit('changeIsSpecificBrowser', isSpecific);
    }
    
    return { isSpecific, browserType, changeIsSpecificBrowser, version };
  }
}
</script>