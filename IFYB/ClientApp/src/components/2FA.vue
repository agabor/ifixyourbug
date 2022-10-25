<template>
  <carousel-item :icon="true" :title="$t('authentication.title')" :subTitle="$t('authentication.subTitle', { email })" :progress="progress">
    <template v-slot:icon>
      <i :class="`ni ni-atom opacity-10 mt-2`"></i>
    </template>
    <template v-slot:content>
      <div class="row mb-4 mx-xl-4">
        <div class="col-2 px-md-2 px-sm-1 px-0" v-for="(i, idx) in codeLength" :key="i">
          <input type="text" :ref="(el) => inputs[idx] = el" class="form-control text-lg text-center" :value="code[idx]" aria-label="2fa" @paste="onPaste($event, idx)" @input="onInputChange($event, idx)" :disabled="progress !== 0">
        </div>
        <span class="text-danger mt-2" v-if="authenticationError"><em><small>{{ $t(authenticationError) }}</small></em></span>
      </div>
      <div class="text-center d-flex justify-content-center">
        <one-click-btn :active="progress === 0" :text="$t('authentication.buttonText')" class="bg-gradient-primary mx-2" @click="submitCode()" :disabled="code.length < codeLength"></one-click-btn>
        <one-click-btn :active="progress === 0" :text="$t('authentication.cancel')" class="btn-outline-secondary mx-2" @click="cancelLogin()"></one-click-btn>
      </div>
    </template>
  </carousel-item>
</template>

<script>
import { ref, onMounted } from 'vue';
import CarouselItem from './CarouselItem.vue';
import OneClickBtn from './OneClickBtn.vue';
import { authenticator } from '@/store/authentication';

export default {
  name: '2FA',
  components: { CarouselItem, OneClickBtn },
  props: {
    isClient: Boolean
  },
  setup(props) {
    const auth = authenticator(props.isClient);
    const code = ref([]);
    let codeLength = 6;
    const inputs = ref([]);
    auth.progress.value = 0;
    
    onMounted(() => {
      focus(0);
    })

    function focus(idx) {
      inputs.value[idx].focus();
    }

    function onPaste(event, idx) {
      let splittedCode = event.clipboardData.getData('text').replace('-', '').split('');
      for(let i = 0; i < codeLength-idx; i++){
        const char = splittedCode[i];
        code.value[idx+i] = char;
      }
      code.value[idx] = splittedCode[0];
    }

    function onInputChange(event, idx) {
      let newValue = event.target.value.toUpperCase();      
      if(!newValue){
        code.value[idx] = '';
        focus(idx-1);
      } else if(newValue.length === 1) {
        code.value[idx] = newValue;
        if(idx+1 < codeLength)
          focus(idx+1);
      } else if(newValue && newValue.length === 2) {
        newValue = newValue.replace(code.value[idx], '');
        if (newValue !== code.value[idx]) {
          code.value[idx] = newValue.replace(code.value[idx], '');
        } else {
          event.target.value = auth.value[idx];
        }
        if(idx+1 < codeLength)
          focus(idx+1);
      } else {
        code.value[idx] = newValue[0];
      }
      if (fillCount() === codeLength) {
        submitCode();
      }
    }

    function fillCount() {
      let count = 0;
      for(let i = 0; i < codeLength; i++) {
        if(code.value[i]) {
          count += 1;
        }
      }
      return count;
    }

    function submitCode() {
      auth.authenticateWithCode(code.value.join(''));
    }

    return { code, codeLength, inputs, 'email': auth.email, 'progress': auth.progress, 'authenticationError': auth.authenticationError, submitCode, onPaste, onInputChange, 'cancelLogin': auth.cancelLogin }
  }
}
</script>