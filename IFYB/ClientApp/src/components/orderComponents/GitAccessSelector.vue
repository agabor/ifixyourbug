<template>
  <div class="col-md-12 pe-2 mb-3">
    <label class="">Previous accesses</label>
    <select class="form-control text-black-50" name="choices-git-access" id="choices-git-access" v-model="selectedAccess" @change="$emit('update:access', selectedAccess)">
      <option :value="{}" selected>Select a previous access</option>
      <option :value="access" v-for="(access, idx) in accesses" :key="idx">{{ access.accessMode == 0 ? 'Public repo' : access.accessMode == 1 ? 'Invite' : 'User account' }} - {{ access.url }}</option>
    </select>
  </div>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'GitAccessSelector',
  emits:['update:access'],
  props: {
    accesses: Array,
    access: Object
  },
  setup(props) {
    const selectedAccess = ref(props.access);

    return { selectedAccess };
  }
}
</script>