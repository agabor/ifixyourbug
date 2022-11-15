<template>
  <div class="row">
    <div class="col-12 mb-3">
      <label class="">{{ $t('flagSelector.label') }}</label>
      <select class="form-control text-black-50" name="choices-git-access" id="choices-git-access" :value="selectedIndex" @change="update($event.target.value)">
        <option :value="flag.id" v-for="flag in flags" :key="flag.id" :selected="flag.id === modelValue">{{ flag.name }}</option>
      </select>
      <span class="text-danger" v-if="showError && inputErrors.flagSelector"><em><small>{{ inputErrors.flagSelector }}</small></em></span>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue';
import { required } from '../../utils/Validate';
import { useInputError, useMessages } from "../../store";

export default {
  name: 'FlagSelector',
  emits:['update:modelValue'],
  props: {
    modelValue: Number,
    flags: Array,
    showError: Boolean,
  },
  setup(props, context) {
    const { tm } = useMessages();
    const { inputErrors, setInputError } = useInputError();
    const selectedIndex = computed(() => {
      return props.modelValue ? props.modelValue : 1;
    });

    setInputError('flagSelector', required((props.modelValue !== null && props.modelValue > -1), tm('errors.requiredBugDes')));

    function update(id) {
      id = parseInt(id);
      setInputError('flagSelector', required(id > -1, tm('errors.requiredBugDes')));
      if(id == -1) {
        context.emit('update:modelValue', null);
      } else {
        context.emit('update:modelValue', id);
      }
    }


    return { inputErrors, selectedIndex, update };
  }
}
</script>