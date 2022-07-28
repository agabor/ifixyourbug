<template>
  <div class="row">
    <div class="col-12 mb-3">
      <div class="form-group mb-0">
        <label>{{ $t('newOrder.bugDescription') }}*</label>
        <text-editor :modelValue="modelValue" :placeholder="$t('newOrder.bugDescription')" @update:modelValue="updateModelValue"></text-editor>
      </div>
      <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.bugDescription }}</small></em></span>
    </div>
  </div>
</template>

<script>
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";
import TextEditor from '../../components/TextEditor.vue';

export default {
  name: 'BugDescription',
  components: { TextEditor },
  emits:['update:modelValue'],
  props: {
    modelValue: String,
    showError: Boolean,
  },
  setup(props, context){
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();

    setInputError('bugDescription', required(props.modelValue, tm('errors.requiredBugDes')));

    function updateModelValue(text){
      context.emit('update:modelValue', text.value);
      setInputError('bugDescription', required(text.value, tm('errors.requiredBugDes')));
    }

    return { inputErrors, updateModelValue }
  }
}
</script>