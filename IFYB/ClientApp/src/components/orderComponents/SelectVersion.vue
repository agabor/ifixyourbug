<template>
  <div class="col-md-6 col-12 ps-md-1">
    <label>{{ $t('frameworkVersion.label') }}</label>
    <select v-if="versions" class="form-control" :class="{'text-black-50': version == undefined}" name="choices-version" id="choices-version" v-model="version" :disabled="!editable">
      <option :value="null" selected hidden>{{ $t('frameworkVersion.placeholder') }}</option>
      <option :value="version" v-for="version in versions" :key="version">{{ version }}</option>
    </select>
    <input v-else type="text" :placeholder="$t('frameworkVersion.frameworkFirst')" class="form-control" disabled>
    <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.version }}</small></em></span>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { computed } from "@vue/reactivity";
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";

export default {
  name: 'SelectVersion',
  emits:['update:modelValue'],
  props: {
    modelValue: String,
    framework: Number,
    editable: Boolean,
    showError: Boolean,
  },
  setup(props, context) {
    const version = ref(props.modelValue);
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();
    const aspVersions = ['3.1', '5.0', '6.0', '7.0'];
    const vueVersions = ['2.6', '2.7', '3.0', '3.1', '3.2'];
    const versions = computed(() => props.framework == 0 ? vueVersions : props.framework == 1 ? aspVersions : null);

    setInputError('version', required(version.value, tm('errors.requiredVersion')));

    watch(() => [props.framework], () => {
      version.value = null;
    })

    watch(version, () => {
      context.emit('update:modelValue', version.value);
      setInputError('version', required(version.value, tm('errors.requiredVersion')));
    });

    return { version, versions, inputErrors };
  }
}
</script>