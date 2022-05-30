<template>
  <div class="col-md-6 ps-md-2">
    <label class="">Version*</label>
    <select v-if="versions" class="form-control" :class="{'text-black-50': version == undefined}" name="choices-version" id="choices-version" v-model="version" @change="$emit('update:modelvalue', version)">
      <option :value="undefined" selected hidden>Select version</option>
      <option :value="version" v-for="version in versions" :key="version">{{ version }}</option>
    </select>
    <input v-else type="text" placeholder="Please select a framework first" class="form-control" disabled>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
export default {
  name: 'SelectVersion',
  emits:['update:modelvalue'],
  props: {
    modelvalue: String,
    versions: Array
  },
  setup(props) {
    const version = ref(props.modelvalue);

    watch(() => [props.versions], () => {
      version.value = undefined;
    })

    return { version };
  }
}
</script>