<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="available-app-input" :value="available" @change="updateAvailable" :checked="available" :disabled="!editable">
      <label class="form-check-label" for="available-app-input">{{ $t('onlineApp.isAvailable') }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="available">
    <div class="form-group mb-0">
      <label>{{ $t('onlineApp.appUrl') }}*</label>
      <input id="app-url-input" class="form-control" :placeholder="$t('onlineApp.appUrl')" type="text" :value="url" @input="$emit('update:url', $event.target.value)" :disabled="!editable">
    </div>
  </div>
</template>

<script>
export default {
  name: 'OnlineApp',
  emits:['update:available', 'update:url'],
  props: {
    available: Boolean,
    url: String,
    editable: Boolean
  },
  setup(props, context){
    function updateAvailable(){
      context.emit('update:url', '');
      context.emit('update:available', !props.available);
    }
    return { updateAvailable }
  }
}
</script>