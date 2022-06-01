<template>
  <div class="col-md-12 pe-2 mb-3">
    <label>{{ $t('projectSharing.urlLabel') }}</label>
    <input class="form-control" id="repo-url-input" :placeholder="$t('projectSharing.urlPlaceholder')" type="text" :value="url" :disabled="!visible" @input="$emit('update:url', $event.target.value)">
  </div>
  <label>{{ $t('projectSharing.sharingLabel') }}</label>
  <div class="col-md-12 d-flex pe-2" :class="{'mb-3': accessMode == undefined}">
    <div class="form-check me-3" v-for="n in optionCount" :key="n">
      <input class="form-check-input" type="radio" name="flexRadioDefault" :id="`flexRadioDefault${n}`" :value="n-1" v-model="mode" :disabled="!visible" @change="$emit('update:accessMode', mode)">
      <label class="form-check-label" :for="`flexRadioDefault${n}`">{{ $t(`projectSharing.option${n}`) }}</label>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="accessMode != undefined">
    <span>{{ $t(`projectSharing.option${accessMode+1}Des`) }}</span>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
export default {
  name: 'ProjectSharing',
  emits:['update:url', 'update:accessMode'],
  props: {
    visible: Boolean,
    url: String,
    accessMode: Number
  },
  setup(props) {
    const optionCount = 3;
    const mode = ref(props.accessMode);

    watch(() => [props.accessMode], () => {
      mode.value = props.accessMode;
    })

    return { optionCount, mode };
  }
}
</script>