<template>
  <div class="col-md-12 pe-2 mb-3">
    <label>{{ $t('projectSharing.urlLabel') }}</label>
    <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.repoUrl)}" :placeholder="$t('projectSharing.urlPlaceholder')" type="text" v-model="urlText" :disabled="!visible">
    <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.repoUrl }}</small></em></span>
  </div>
  <label>{{ $t('projectSharing.sharingLabel') }}</label>
  <div class="col-md-12 d-flex pe-2">
    <div class="form-check me-3" v-for="n in optionCount" :key="n">
      <input class="form-check-input" type="radio" name="flexRadioDefault" :id="`flexRadioDefault${n}`" :value="n-1" v-model="mode" :disabled="!visible">
      <label class="form-check-label" :for="`flexRadioDefault${n}`">{{ $t(`projectSharing.option${n}`) }}</label>
    </div>
  </div>
  <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.accessMode }}</small></em></span>
  <div class="col-md-12 pe-2">
    <span>{{ $t(`projectSharing.description${mode+1}`) }}</span>
  </div>
   <div class="code" v-if="mode === 2">
    ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABgQDwHzC3IIDEObmM2v/Vo+YcAgwAEfAOmxIY1dfbjzBvbBoHAkL4k6U9Gi2yVoC10DKXqCwwJTIRqIwRKTIYRq18w8/WOoY7JX+DTHXu2W3GvV6AcKQwxHVZWYm2HLTrIQVhHuj0uZi/pIGS98W1TF4OXjM1YZiL+rodIWA3uJsryVryveFeIdH/CU3YLxHfXtldWg9lOtu3eJCogpUq1boSgsxH4DC+2UcVVGhlEJq8c6qiDJ9qqnWUHimypzalC3rnA7I+kPJF9iJpEockOlyo4PYoEA4Mw4wdE6GabZ5Ns5gkE53Kqi2OM8PDBZae/GBq7hVIm5ySq5IX9jwgt5AahpI6E4mLGrG1ImgarJYNNuaiF75VolulWZVxwpTnwEHipagTTPmSU1baIVuqNDkAIk62pvjdenk2O6VoWEorugMYkVrniTkC7WgQ8u1p9NHwXbibGpht/sEzZJmdDq4NMfmQFbI+aQwl/AoDdQ9SS21Z+dqlIIV8HXZbFp4LdCU= agabor@Gabors-Mac-mini.local
   </div>
</template>

<script>
import { ref, watch } from 'vue'
import { required } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError } from "../../store";

export default {
  name: 'ProjectSharing',
  emits:['update:url', 'update:accessMode'],
  props: {
    modelValue: String,
    accessMode: Number,
    visible: Boolean,
    showError: Boolean,
  },
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
<style>
.code {
  font-family: monospace;
  font-size: smaller;
  margin-top: 1rem;
  margin-bottom: 1rem;
}
</style>