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
        <input class="form-check-input" type="radio" name="flexRadioDefault" :id="option.title" :value="option" v-model="selectedOption" @input="updateSelectedOption(option)" :disabled="!visible">
        <label class="form-check-label" :for="option.title">{{ option.title }}</label>
      </div>
    </div>
    <span class="text-danger" v-if="showError"><em><small>{{ inputErrors.accessMode }}</small></em></span>
    <div class="col-12" v-if="selectedOption">
      <span v-html="selectedOption.description"></span>
    </div>
  </div>
  <ssh-key-preview v-if="selectedOption && selectedOption.id === 2"></ssh-key-preview>
</template>

<script>
import { ref, watch } from 'vue'
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
    const sharingOptions = ref([]);
    const selectedOption = ref(null);

    setSharingOptions();
    setInputError('repoUrl', validGitUrl(props.modelValue));
    setInputError('accessMode', required(selectedOption.value, tm('errors.requiredAccessMode')));
    setInviteText();

    watch(props, () => {
      selectedOption.value = sharingOptions.value[props.accessMode];
      setInputError('repoUrl', validGitUrl(props.modelValue));
      setInputError('accessMode', required(selectedOption.value, tm('errors.requiredAccessMode')));
    })

    function setSharingOptions() {
      for(let i = 0; i < optionCount; i++) {
        if(i == 1) {
          let des = tm('projectSharing.description2default');
          if(!validGitUrl(props.modelValue)) {
            des = tm('projectSharing.description2', { 'saas': gitServices.value[i].name, 'git_user': gitServices.value[i].user });
          }
          sharingOptions.value.push({id: i, title: tm(`projectSharing.option${i+1}`), description: des});
        } else {
          sharingOptions.value.push({id: i, title: tm(`projectSharing.option${i+1}`), description: tm(`projectSharing.description${i+1}`)});
        }
        if(i == props.accessMode) {
          selectedOption.value = sharingOptions.value[i]
        }
      }
    }

    function setInviteText() {
      if(!validGitUrl(props.modelValue)) {
        const url = new URL(props.modelValue);
        const domain = url.hostname.replace('www.','');
        for(let i = 0; i < gitServices.value.length; i++){
          if(gitServices.value[i].domain.includes(domain)) {
            sharingOptions.value[1].description = tm('projectSharing.description2', { 'saas': gitServices.value[i].name, 'git_user': gitServices.value[i].user });
            break;
          }
        }
      } else {
        sharingOptions.value[1].description = tm('projectSharing.description2default');
      }
    }

    function updateUrl(value) {
      setInviteText();
      context.emit('update:modelValue', value);
      setInputError('repoUrl', validGitUrl(value));
    }

    function updateSelectedOption(option) {
      selectedOption.value = option;
      context.emit('update:accessMode', selectedOption.value.id);
      setInputError('repoUrl', validGitUrl(props.modelValue));
      setInputError('accessMode', required(selectedOption.value.id, tm('errors.requiredAccessMode')));
    }

    return { optionCount, selectedOption, sharingOptions, inputErrors, gitServices, updateUrl, updateSelectedOption };
  }
}
</script>