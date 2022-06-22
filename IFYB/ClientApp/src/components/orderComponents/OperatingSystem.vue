<template>
  <div class="col-md-12 pe-2">
    <div class="form-check form-switch">
      <input class="form-check-input" type="checkbox" id="specific-op-system-input" v-model="isChecked" :disabled="!editable">
      <label class="form-check-label" for="specific-op-system-input">{{ $t('operatingSystem.isSpecific') }}</label>
    </div>
  </div>
  <label v-if="isChecked">{{ $t('operatingSystem.label') }}</label>
  <div class="col-md-12 d-flex pe-2" v-if="isChecked">
    <div class="form-check me-3" v-for="n in optionCount" :key="n">
      <input class="form-check-input" type="radio" name="osRadio" :id="`osRadio${n}`" :value="$t(`operatingSystem.option${n}`)" v-model="os" :disabled="!editable">
      <label class="form-check-label" :for="`osRadio${n}`">{{ $t(`operatingSystem.option${n}`) }}</label>
    </div>
  </div>
  <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.specificPlatform }}</small></em></span>
  <div class="col-md-12 pe-2 mb-3" v-if="isChecked">
    <div class="form-group mb-0">
      <label>{{ $t('operatingSystem.version') }}*</label>
      <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.specificPlatformVersion)}" :placeholder="$t('operatingSystem.version')" type="text" v-model="osVersion" :disabled="!editable">
    </div>
    <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.specificPlatformVersion }}</small></em></span>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";

export default {
  name: 'OperatingSystem',
  emits:['update:modelValue', 'update:version'],
  props: {
    modelValue: String,
    version: String,
    editable: Boolean,
    showError: Boolean,
  },
  setup(props, context) {
    const isChecked = ref(props.modelValue !== null);
    const optionCount = 3;
    const os = ref(props.modelValue);
    const osVersion = ref(props.version);
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();

    if(isChecked.value) {
      setInputError('specificPlatform', required(os.value, tm('errors.requiredOS')));
      setInputError('specificPlatformVersion', required(osVersion.value, tm('errors.requiredOSVersion')));
    }

    watch(isChecked, () => {
      if (isChecked.value) {
        context.emit('update:modelValue', os.value);
        setInputError('specificPlatform', required(os.value, tm('errors.requiredOS')));
        setInputError('specificPlatformVersion', required(osVersion.value, tm('errors.requiredOSVersion')));
      } else {
        context.emit('update:modelValue', null);
        setInputError('specificPlatform', null);
        setInputError('specificPlatformVersion', null);
      }
    });
    watch(os, () => {
      context.emit('update:modelValue', os.value);
      setInputError('specificPlatform', required(os.value, tm('errors.requiredOS')));
    });
    watch(osVersion, () => {
      context.emit('update:version', osVersion.value);
      setInputError('specificPlatformVersion', required(osVersion.value, tm('errors.requiredOSVersion')));
    });
    
    return { isChecked, optionCount, os, osVersion, inputErrors };
  }
}
</script>