<template>
  <carousel-item icon="atom" :title="$t('twofa.title')" :subTitle="$t('twofa.subTitle')">
    <div class="row mb-4 mx-xl-4">
      <div class="col-2 px-md-2 px-sm-1 px-0" v-for="(i, idx) in authLength" :key="i">
        <input type="text" :ref="(el) => inputs[idx] = el" class="form-control text-lg text-center" :value="auth[idx]" aria-label="2fa" @paste="onPaste($event, idx)" @input="onInputChange($event, idx)">
      </div>
    </div>
    <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="codeError ? codeError : error">
      {{ codeError ? codeError : error }}
    </div>
    <div class="text-center d-flex justify-content-center">
      <one-click-btn v-model:active="activeBtn" :text="$t('twofa.buttonText')" class="bg-gradient-primary mx-2" @click="submitCode()"></one-click-btn>
      <one-click-btn v-model:active="activeBtn" :text="$t('twofa.cancel')" class="btn-outline-secondary mx-2" @click="cancel()"></one-click-btn>
    </div>
  </carousel-item>
</template>

<script>
import { ref, watch } from 'vue';
import CarouselItem from './CarouselItem.vue';
import OneClickBtn from './OneClickBtn.vue';

export default {
  name: '2FA',
  components: { CarouselItem, OneClickBtn },
  props: {
    modelValue: String,
    error: String
  },
  emits:['update:modelValue', 'cancel'],
  setup(props, context) {
    const auth = ref(props.modelValue ? props.modelValue.split('') : []);
    let authLength = 6;
    const codeError = ref(null);
    const inputs = ref([]);
    const activeBtn = ref(false);
    let codeSubmitted = false;

    watch(props, () => {
      auth.value = props.modelValue ? props.modelValue.split('') : [];
    })

    function focus(idx) {
      inputs.value[idx].focus()
    }

    function onPaste(event, idx) {
      let code = event.clipboardData.getData('text').replace('-', '').split('');
      for(let i = 0; i < authLength-idx; i++){
        const char = code[i];
        auth.value[idx+i] = char;
      }
      auth.value[idx] = code[0];
    }

    function onInputChange(event, idx) {
      let newValue = event.target.value.toUpperCase();
      
      if(!newValue){
        auth.value[idx] = '';
        focus(idx-1)
      } else if(newValue.length == 1) {
        auth.value[idx] = newValue;
        if(idx+1 < authLength)
          focus(idx+1)
      } else if(newValue && newValue.length == 2) {
        newValue = newValue.replace(auth.value[idx], '');
        if (newValue !== auth.value[idx]) {
          auth.value[idx] = newValue.replace(auth.value[idx], '')
        } else {
          event.target.value = auth.value[idx]
        }
        if(idx+1 < authLength)
          focus(idx+1)
      } else {
        auth.value[idx] = newValue[0]
      }
      if (fillCount() == authLength) {
        if(!codeSubmitted) {
          submitCode();
          codeSubmitted = true
        } else {
          activeBtn.value = true
        }
      } else {
          activeBtn.value = false
      }
    }

    function fillCount() {
      let count = 0;
      for(let i = 0; i < authLength; i++) {
        if(auth.value[i]) {
          count += 1;
        }
      }
      return count;
    }

    

    function submitCode() {
      activeBtn.value = false;
      context.emit('update:modelValue', auth.value.join(''));
    }

    function cancel() {
      auth.value = [];
      codeError.value = null;
      activeBtn.value = true;
      context.emit('cancel');
    }
    return { auth, codeError, authLength, inputs, activeBtn, submitCode, onPaste, onInputChange, cancel }
  }
}
</script>