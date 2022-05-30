<template>
  <div class="col-md-6">
    <label class="">Framework*</label>
    <select class="form-control" :class="{'text-black-50': framework == undefined}" name="choices-framework" id="choices-framework" v-model="framework" @change="$emit('update:modelvalue', framework)">
      <option :value="undefined" selected hidden>Select a framework</option>
      <option :value="0">Vue.js</option>
      <option :value="1">ASP.NET Core</option>
    </select>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'SelectFramework',
  emits:['update:modelvalue'],
  props: {
    modelvalue: Number
  },
  setup(props) {
    const framework = ref(props.modelvalue);

    return { framework };
  }
}
</script>