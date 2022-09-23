<template>
  <div class="row">
    <div class="col-12 mb-3">
      <label class="">{{ $t('gitAccessSelector.label') }}</label>
      <select class="form-control text-black-50" name="choices-git-access" id="choices-git-access" :value="selectedIndex" @change="update($event.target.value)">
        <option :value="-1" selected>{{ $t('gitAccessSelector.placeholder') }}</option>
        <option :value="access.id" v-for="(access, idx) in accesses" :key="idx">{{ access.accessMode == 0 ? 'Public repo' : access.accessMode == 1 ? 'Invite' : 'User account' }} - {{ access.url }}</option>
      </select>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
export default {
  name: 'GitAccessSelector',
  emits:['update:modelValue'],
  props: {
    modelValue: Object,
    accesses: Array
  },
  setup(props, context) {
    const selectedIndex = computed(() => {
      return props.modelValue.id ? props.modelValue.id : -1;
    });

    function update(id) {
      id = parseInt(id);
      if(id === -1) {
        context.emit('update:modelValue', {});
      } else {
        context.emit('update:modelValue', props.accesses.find(a => a.id === id));
      }
    }

    return { selectedIndex, update };
  }
}
</script>