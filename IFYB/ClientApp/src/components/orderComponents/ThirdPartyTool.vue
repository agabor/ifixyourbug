<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="third-party-tool-input" v-model="isThirdPartyTool" @change="$emit('changeIsThirdPartyTool', isThirdPartyTool)">
      <label class="form-check-label" for="third-party-tool-input">Is the bug potentially related to a third party library?</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isThirdPartyTool">
    <div class="form-group mb-0">
      <label>Third party library name*</label>
      <input id="third-party-tool-input" class="form-control" placeholder="Third party library name" type="text" v-model="thirdPartyTool" @input="$emit('changeThirdPartyTool', thirdPartyTool)">
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'ThirdPartyTool',
  emits:['changeIsThirdPartyTool', 'changeThirdPartyTool'],
  props: {
    isTool: Boolean,
    tool: String
  },
  setup(props) {
    const isThirdPartyTool = ref(props.isTool);
    const thirdPartyTool = ref(props.tool);

    return { isThirdPartyTool, thirdPartyTool };
  }
}
</script>