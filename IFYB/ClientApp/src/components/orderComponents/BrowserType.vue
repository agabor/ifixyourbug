<template>
  <div class="row">
    <div class="col-12">
      <div class="form-check form-switch">
        <input class="form-check-input" type="checkbox" id="specific-browser-input" v-model="isChecked" :disabled="!editable">
        <label class="form-check-label" for="specific-browser-input">{{ $t('browserType.isSpecific') }}</label>
      </div>
    </div>
    <label v-if="isChecked">{{ $t('browserType.label') }}</label>
    <div class="col-12 d-flex flex-wrap" v-if="isChecked">
      <div class="form-check me-3" v-for="n in optionCount" :key="n">
        <input class="form-check-input" type="radio" name="browserRadio" :id="`browserRadio${n}`" :value="$t(`browserType.option${n}`)" v-model="browser" :disabled="!editable">
        <label class="form-check-label" :for="`browserRadio${n}`">{{ $t(`browserType.option${n}`) }}</label>
      </div>
    </div>
    <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.specificPlatform }}</small></em></span>
    <div class="col-12 mb-3" v-if="isChecked">
      <div class="form-group mb-0">
        <label>{{ $t('browserType.version') }}*</label>
        <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.specificPlatformVersion)}" :placeholder="$t('browserType.version')" type="text" v-model="browserVersion" :disabled="!editable">
      </div>
      <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.specificPlatformVersion }}</small></em></span>
    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";

export default {
  name: 'browserType',
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
    const browser = ref(props.modelValue);
    const browserVersion = ref(props.version);
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();

    if(isChecked.value) {
      setInputError('specificPlatform', required(browser.value, tm('errors.requiredBrowserType')));
      setInputError('specificPlatformVersion', required(browserVersion.value, tm('errors.requiredBrowserVersion')));
    }

    watch(isChecked, () => {
      if (isChecked.value) {
        context.emit('update:modelValue', browser.value);
        setInputError('specificPlatform', required(browser.value, tm('errors.requiredBrowserType')));
        setInputError('specificPlatformVersion', required(browserVersion.value, tm('errors.requiredBrowserVersion')));
      } else {
        context.emit('update:modelValue', null);
        setInputError('specificPlatform', null);
        setInputError('specificPlatformVersion', null);
      }
    });
    watch(browser, () => {
      context.emit('update:modelValue', browser.value);
      setInputError('specificPlatform', required(browser.value, tm('errors.requiredBrowserType')));
    });
    watch(browserVersion, () => {
      context.emit('update:version', browserVersion.value);
      setInputError('specificPlatformVersion', required(browserVersion.value, tm('errors.requiredBrowserVersion')));
    });
    
    return { isChecked, optionCount, browser, browserVersion, inputErrors };
  }
}
</script>