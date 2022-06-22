<template>
  <div class="col-md-6">
    <label>{{ $t('framework.label') }}</label>
    <select class="form-control" :class="{'text-black-50': framework == undefined}" name="choices-framework" id="choices-framework" v-model="framework" :disabled="!editable">
      <option :value="null" selected hidden>{{ $t('framework.placeholder') }}</option>
      <option v-for="n in optionCount" :key="n" :value="n-1">{{ $t(`framework.option${n}`) }}</option>
    </select>
    <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.framework }}</small></em></span>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";

export default {
  name: 'SelectFramework',
  emits:['update:modelValue'],
  props: {
    modelValue: Number,
    editable: Boolean,
    showError: Boolean,
  },
  setup(props, context) {
    const optionCount = 2;
    const framework = ref(props.modelValue);
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();

    setInputError('framework', required(framework.value, tm('errors.requiredFramework')));

    watch(framework, () => {
      context.emit('update:modelValue', framework.value);
      setInputError('framework', required(framework.value, tm('errors.requiredFramework')));
    });

    return { optionCount, framework, inputErrors };
  }
}
</script>