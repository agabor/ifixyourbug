<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="is-third-party-tool-input" :value="isTool" @change="updateIsTool">
      <label class="form-check-label" for="is-third-party-tool-input">{{ $t('thirdPartyTool.isPotentially') }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="isTool">
    <div class="form-group mb-0">
      <label>{{ $t('thirdPartyTool.label') }}*</label>
      <input id="third-party-tool-input" class="form-control" :placeholder="$t('thirdPartyTool.label')" type="text" :value="tool" @input="$emit('update:tool', $event.target.value)">
    </div>
  </div>
</template>

<script>
export default {
  name: 'ThirdPartyTool',
  emits:['update:isTool', 'update:tool'],
  props: {
    isTool: Boolean,
    tool: String
  },
  setup(props, context){
    function updateIsTool(){
      context.emit('update:tool', '');
      context.emit('update:isTool', !props.isTool);
    }
    return { updateIsTool }
  }
}
</script>