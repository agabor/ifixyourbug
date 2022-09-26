<template>
  <div class="row">
    <div class="col-12 mb-3">
      <label>{{ $t('projectSharing.urlLabel') }}</label>
      <input class="form-control" :class="{'is-invalid': (showError && !!inputErrors.repoUrl)}" :placeholder="$t('projectSharing.urlPlaceholder')" type="text" :value="modelValue" @input="updateUrl($event.target.value)" :disabled="!visible">
      <span class="text-danger" v-if="showError && inputErrors.repoUrl"><em><small>{{ $t(`${inputErrors.repoUrl}`) }}</small></em></span>
    </div>
    <label>{{ $t('projectSharing.sharingLabel') }}</label>
    <div class="col-12 d-flex flex-wrap">
      <div class="form-check me-3" v-for="(option, idx) in sharingOptions" :key="idx">
        <input class="form-check-input" type="radio" name="flexRadioDefault" :id="`option${option.id}`" :value="option.id" @input="updateAccessMode(option.id)" :disabled="!visible" :checked="option.id === accessMode">
        <label class="form-check-label" :for="`option${option.id}`">{{ option.title }}</label>
      </div>
    </div>
    <span class="text-danger" v-if="showError && inputErrors.accessMode"><em><small>{{ inputErrors.accessMode }}</small></em></span>
    <div class="col-12" v-if="selectedOption">
      <span v-html="selectedOption.description"></span>
    </div>
  </div>
  <ssh-key-preview v-if="selectedOption && selectedOption.id === 2"></ssh-key-preview>
</template>

<script>
import { computed } from 'vue'
import { required, validGitUrl } from '../../utils/Validate';
import { useInputError, useGitServices, useMessages } from "../../store";
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
    const { tm } = useMessages();
    const { inputErrors, setInputError } = useInputError();
    const { gitServices } = useGitServices();

    const sharingOptions = computed(() => {
      let options = [];
      for(let i = 0; i < optionCount; i++) {
        if(i == 1) {
          options.push({id: i, title: tm(`projectSharing.option${i+1}`), description: inviteText.value});
        } else {
          options.push({id: i, title: tm(`projectSharing.option${i+1}`), description: tm(`projectSharing.description${i+1}`)});
        }
      }
      return options;
    });

    const selectedOption = computed(() => {
      if(null !== props.accessMode) {
        return sharingOptions.value[props.accessMode];
      }
      return null;
    });

    const inviteText = computed(() => {
      if (gitServices.value == null)
        return tm('projectSharing.description2default'); 
      if(!validGitUrl(props.modelValue)) {
        for(let i = 0; i < gitServices.value.length; i++){
          if(props.modelValue.includes(gitServices.value[i].domain)) {
            return tm('projectSharing.description2', { 'saas': gitServices.value[i].name, 'git_user': gitServices.value[i].user });
          }
        }
      }
      return tm('projectSharing.description2default');
    });

    setInputError('repoUrl', validGitUrl(props.modelValue));
    setInputError('accessMode', required(selectedOption.value, tm('errors.requiredAccessMode')));

    function updateUrl(value) {
      setInputError('repoUrl', validGitUrl(value));
      context.emit('update:modelValue', value);
    }

    function updateAccessMode(option) {
      setInputError('repoUrl', validGitUrl(props.modelValue));
      setInputError('accessMode', required(option, tm('errors.requiredAccessMode')));
      context.emit('update:accessMode', option);
    }

    return { optionCount, selectedOption, sharingOptions, inputErrors, gitServices, updateUrl, updateAccessMode };
  }
}
</script>