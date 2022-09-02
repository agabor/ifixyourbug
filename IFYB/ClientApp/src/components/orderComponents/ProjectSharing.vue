<template>
  <div class="row">
    <div class="col-12 mb-3">
      <label>{{ $t('projectSharing.urlLabel') }}</label>
      <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.repoUrl)}" :placeholder="$t('projectSharing.urlPlaceholder')" type="text" v-model="urlText" :disabled="!visible">
      <span class="text-danger" v-if="showError && inputErrors.repoUrl"><em><small>{{ $t(`${inputErrors.repoUrl}`) }}</small></em></span>
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
      <span v-if="mode == 1">{{ inviteText }}</span>
      <span v-else-if="mode > -1">{{ $t(`projectSharing.description${mode+1}`) }}</span>
    </div>
  </div>
  <ssh-key-preview v-if="mode === 2"></ssh-key-preview>
</template>

<script>
import { ref, watch } from 'vue'
import { required, validGitUrl } from '../../utils/Validate';
import { useI18n } from "vue-i18n";
import { useInputError, useGitServices } from "../../store";
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
    const { gitServices } = useGitServices();
    const inviteText = ref(null);

    setInputError('repoUrl', validGitUrl(urlText.value));
    setInputError('accessMode', required(mode.value, tm('errors.requiredGitRepoUrl')));
    setInviteText();

    watch(() => [props.modelValue, props.accessMode], () => {
      urlText.value = props.modelValue;
      mode.value = props.accessMode;
    })
    watch(urlText, () => {
      setInviteText();
      context.emit('update:modelValue', urlText.value);
      setInputError('repoUrl', validGitUrl(urlText.value));
    });
    watch(mode, () => {
      context.emit('update:accessMode', mode.value);
      setInputError('accessMode', required(mode.value, tm('errors.requiredGitRepoUrl')));
    });

    function setInviteText() {
      let domain = (new URL(urlText.value)).hostname.replace('www.','');
      inviteText.value = tm('projectSharing.description2default');
      for(let i = 0; i < gitServices.value.length; i++){
        if(gitServices.value[i].domain == domain) {
          inviteText.value = tm('projectSharing.description2', { 'sass': gitServices.value[i].name, 'git_user': gitServices.value[i].user });
          break;
        }
      }
    }

    return { optionCount, mode, urlText, inputErrors, gitServices, inviteText };
  }
}
</script>