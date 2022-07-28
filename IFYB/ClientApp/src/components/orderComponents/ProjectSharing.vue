<template>
  <div class="row">
    <div class="col-12 mb-3">
      <label>{{ $t('projectSharing.urlLabel') }}</label>
      <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.repoUrl)}" :placeholder="$t('projectSharing.urlPlaceholder')" type="text" v-model="urlText" :disabled="!visible">
      <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.repoUrl }}</small></em></span>
    </div>
    <label>{{ $t('projectSharing.sharingLabel') }}</label>
    <div class="col-12 d-flex flex-wrap">
      <div class="form-check me-3" v-for="n in optionCount" :key="n">
        <input class="form-check-input" type="radio" name="flexRadioDefault" :id="`flexRadioDefault${n}`" :value="n-1" v-model="mode" :disabled="!visible">
        <label class="form-check-label" :for="`flexRadioDefault${n}`">{{ $t(`projectSharing.option${n}`) }}</label>
      </div>
    </div>
    <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.accessMode }}</small></em></span>
    <div class="col-12" v-if="mode > -1">
      <span>{{ $t(`projectSharing.description${mode+1}`) }}</span>
    </div>
  </div>
  <ssh-key-preview v-if="mode === 2"></ssh-key-preview>
</template>

<script>
import { ref, watch } from 'vue'
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";
import SshKeyPreview from '../SshKeyPreview.vue';

export default {
  name: 'ProjectSharing',
  emits:['update:modelValue', 'update:accessMode'],
  props: {
    modelValue: String,
    accessMode: Number,
    visible: Boolean,
    showError: Boolean,
  },
  components: { SshKeyPreview },
  setup(props, context) {
    const optionCount = 3;
    const mode = ref(props.accessMode);
    const urlText = ref(props.modelValue ?? '');
    const { tm } = useI18n();
    const { inputErrors, setInputError } = useInputError();

    setInputError('repoUrl', required(urlText.value, tm('errors.requiredProjectSharing')));
    setInputError('accessMode', required(mode.value, tm('errors.requiredGitRepoUrl')));

    watch(() => [props.modelValue, props.accessMode], () => {
      urlText.value = props.modelValue;
      mode.value = props.accessMode;
    })
    watch(urlText, () => {
      context.emit('update:modelValue', urlText.value);
      setInputError('repoUrl', required(urlText.value, tm('errors.requiredProjectSharing')));
    });
    watch(mode, () => {
      context.emit('update:accessMode', mode.value);
      setInputError('accessMode', required(mode.value, tm('errors.requiredGitRepoUrl')));
    });

    return { optionCount, mode, urlText, inputErrors };
  }
}
</script>