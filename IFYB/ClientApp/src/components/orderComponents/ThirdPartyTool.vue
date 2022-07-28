<template>
  <div class="row">
    <div class="col-12">
      <div class="form-check form-switch">
        <input class="form-check-input" type="checkbox" id="is-third-party-tool-input" v-model="isChecked" :disabled="!editable">
        <label class="form-check-label" for="is-third-party-tool-input">{{ $t('thirdPartyTool.isPotentially') }}</label>
      </div>
    </div>
    <div class="col-12 mb-3" v-if="isChecked">
      <div class="form-group mb-0">
        <label>{{ $t('thirdPartyTool.label') }}*</label>
        <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.thirdPartyTool)}" :placeholder="$t('thirdPartyTool.label')" type="text" v-model="text" :disabled="!editable">
      </div>
      <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.thirdPartyTool }}</small></em></span>
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";

export default {
  name: 'ThirdPartyTool',
  emits:['update:modelValue'],
  props: {
    modelValue: String,
    editable: Boolean,
    showError: Boolean,
  },
  setup(props, context){
    const isChecked = ref(props.modelValue !== null);
    const text = ref(props.modelValue ?? '');
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();

    if(isChecked.value) {
      setInputError('thirdPartyTool', required(text.value, tm('errors.requiredThirdPartyTool')));
    }

    watch(isChecked, () => {
      if (isChecked.value) {
        context.emit('update:modelValue', text.value);
        setInputError('thirdPartyTool', required(text.value, tm('errors.requiredThirdPartyTool')));
      } else {
        context.emit('update:modelValue', null);
        setInputError('thirdPartyTool', null);
      }
    });
    watch(text, () => {
      context.emit('update:modelValue', text.value);
      setInputError('thirdPartyTool', required(text.value, tm('errors.requiredThirdPartyTool')));
    });

    return { isChecked, text, inputErrors }
  }
}
</script>