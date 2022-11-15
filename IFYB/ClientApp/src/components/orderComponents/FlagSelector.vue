<template>
  <div class="row">
    <div class="col-12 mb-3">
      <label class="">{{ $t('flagSelector.label') }}</label>
      <select class="form-control text-black-50" name="choices-flag" id="choices-flag" :value="selectedIndex" @change="update($event.target.value)">
        <option :value="flag.id" v-for="flag in flags" :key="flag.id" :selected="flag.id === modelValue">{{ flag.name }}</option>
      </select>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue';

export default {
  name: 'FlagSelector',
  emits:['update:modelValue'],
  props: {
    modelValue: Number,
    flags: Array,
    showError: Boolean,
  },
  setup(props, context) {
    const selectedIndex = computed(() => {
      return props.modelValue ? props.modelValue : 1;
    });

    function update(id) {
      context.emit('update:modelValue', parseInt(id));
    }

    return { selectedIndex, update };
  }
}
</script>