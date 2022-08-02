<template>
  <carousel-item icon="atom" :title="$t('twofa.title')" :subTitle="$t('twofa.subTitle')">
    <div class="row mb-4 mx-xl-4">
      <div class="col-2 px-md-2 px-sm-1 px-0" v-for="(i, idx) in authLength" :key="i">
        <input type="text" :ref="(el) => inputs[idx] = el" class="form-control text-lg text-center" @keyup.enter="checkValidCode" @keyup.delete="deleteFromAuth(idx)" v-model="auth[idx]" aria-label="2fa" @paste="onPaste($event, idx)" @input="onInputChange($event, idx)">
      </div>
    </div>
    <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="codeError ? codeError : error">
      {{ codeError ? codeError : error }}
    </div>
    <div class="text-center d-flex justify-content-center">
      <one-click-btn v-model:active="activeBtn" :text="$t('twofa.buttonText')" class="bg-gradient-primary mx-2" @click="checkValidCode()"></one-click-btn>
      <one-click-btn v-model:active="activeBtn" :text="$t('twofa.cancel')" class="btn-outline-secondary mx-2" @click="cancel()"></one-click-btn>
    </div>
  </carousel-item>
</template>

<script>
import { ref } from 'vue';
import { min } from '../utils/Validate';
import { useI18n } from "vue-i18n";
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
    const { tm } = useI18n();
    const auth = ref(props.modelValue ? props.modelValue.split('') : []);
    let oldAuth = [];
    let authLength = 6;
    const codeError = ref(null);
    const inputs = ref([]);
    const activeBtn = ref(true);

    function focus(idx) {
      inputs.value[idx].focus()
    }

    function onPaste(event, idx) {
      let code = event.clipboardData.getData('text').replace('-', '').split('');
      for(let i = 0; i < authLength-idx; i++){
        const char = code[i];
        auth.value[idx+i] = char;
        oldAuth[idx+i] = char;
      }
      auth.value[idx] = code[0];
    }

    function onInputChange(event, idx) {
      let newValue = event.target.value.toUpperCase();
      if(newValue.length <= 2) {
        if(newValue && newValue.length == 1) {
          auth.value[idx] = newValue;
          oldAuth[idx] = auth.value[idx];
          if(idx+1 < authLength)
            focus(idx+1);
        } else if(newValue && newValue.length == 2) {
          auth.value[idx] = newValue.replace(oldAuth[idx], '');
          oldAuth[idx] = auth.value[idx];
          if(idx+1 < authLength)
            focus(idx+1);
        }
        autoCheck();
      } else {
        auth.value[idx] = newValue[0];
        autoCheck();
      }
    }

    function autoCheck() {
      let unfilled = false;
      for(let i = 0; i < authLength; i++) {
        if(auth.value[i] === '' || auth.value[i] === undefined) {
          unfilled = true;
          break;
        }
      }
      if(!unfilled) {
        checkValidCode();
      }
    }
    
    function deleteFromAuth(idx) {
      if(idx - 1 > -1 && (auth.value[idx] === '' || auth.value[idx] === undefined)) { 
        auth.value[idx - 1] = '';
        focus(idx-1);
      }
    }

    function checkValidCode() {
      let err = min(auth.value.join(''), authLength);
      if(err) {
        codeError.value = tm(err) + authLength;
        activeBtn.value = true;
      } else {
        context.emit('update:modelValue', auth.value.join(''));
        codeError.value = null;
      }
    }

    function cancel() {
      auth.value = [];
      oldAuth.value = [];
      codeError.value = null;
      activeBtn.value = true;
      context.emit('cancel');
    }
    return { auth, codeError, authLength, inputs, activeBtn, deleteFromAuth, checkValidCode, onPaste, onInputChange, cancel }
  }
}
</script>