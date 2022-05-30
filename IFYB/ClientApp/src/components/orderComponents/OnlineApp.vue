<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="third-party-tool-input" :value="available" @change="$emit('update:available', $event.target.value)">
      <label class="form-check-label" for="third-party-tool-input"> Is there a deployed version of the application available online?</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="available">
    <div class="form-group mb-0">
      <label>App url*</label>
      <input id="app-url-input" class="form-control" placeholder="Application url" type="text" :value="url" @input="$emit('update:url', $event.target.value)">
    </div>
  </div>
</template>

<script>
export default {
  name: 'OnlineApp',
  emits:['update:available', 'update:url'],
  props: {
    available: Boolean,
    url: String
  }
}
</script>