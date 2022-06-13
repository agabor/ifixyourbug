<template>
  <div class="col-md-6">
    <label>{{ $t('framework.label') }}</label>
    <select class="form-control" :class="{'text-black-50': framework == undefined}" name="choices-framework" id="choices-framework" v-model="framework" @change="$emit('update:modelvalue', framework)" :disabled="!editable">
      <option :value="undefined" selected hidden>{{ $t('framework.placeholder') }}</option>
      <option v-for="n in optionCount" :key="n" :value="n-1">{{ $t(`framework.option${n}`) }}</option>
    </select>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'SelectFramework',
  emits:['update:modelvalue'],
  props: {
    modelvalue: Number,
    editable: Boolean
  },
  setup(props) {
    const optionCount = 2;
    const framework = ref(props.modelvalue);

    return { optionCount, framework };
  }
}
</script>