<template>
  <div class="col-md-12 pe-2 mb-3">
    <label class="">{{ $t('gitAccessSelector.label') }}</label>
    <select class="form-control text-black-50" name="choices-git-access" id="choices-git-access" v-model="selectedAccess" @change="$emit('update:modelValue', selectedAccess)">
      <option :value="{}" selected>{{ $t('gitAccessSelector.placeholder') }}</option>
      <option :value="access" v-for="(access, idx) in accesses" :key="idx">{{ access.accessMode == 0 ? 'Public repo' : access.accessMode == 1 ? 'Invite' : 'User account' }} - {{ access.url }}</option>
    </select>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'GitAccessSelector',
  emits:['update:modelValue'],
  props: {
    modelValue: Object,
    accesses: Array
  },
  setup(props) {
    const selectedAccess = ref(props.modelValue);

    return { selectedAccess };
  }
}
</script>