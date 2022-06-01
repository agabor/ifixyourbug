<template>
  <carousel-item icon="atom" :title="$t('twofa.title')" :subTitle="$t('twofa.subTitle')" :buttonText="$t('twofa.buttonText')" :error="codeError ? codeError : error" @onClickBtn="checkValidCode()">
    <div class="row mb-4">
      <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2" v-for="(i, idx) in authLength" :key="i">
        <input type="text" :id="`2fa-${idx}`" class="form-control text-lg text-center" @keyup.enter="checkValidCode" @keyup.delete="deleteFromAuth(idx)" v-model="auth[idx]" aria-label="2fa" @paste="onPaste($event, idx)" @input="onInputChane($event, idx)">
      </div>
    </div>
  </carousel-item>
</template>

<script>
import { ref } from 'vue';
import { min } from '../utils/Validate';
import { useI18n } from "vue-i18n";
import CarouselItem from './CarouselItem.vue';

export default {
  name: '2FA',
  components: { CarouselItem },
  props: {
    modelValue: String,
    error: String
  },
  emits:['update:modelValue'],
  setup(props, context) {
    const { tm } = useI18n();
    const auth = ref(props.modelValue.split(''));
    let oldAuth = [];
    let authLength = 6;
    const codeError = ref(null);

    function onPaste(event, idx) {
      let code = event.clipboardData.getData('text').split('');
      for(let i = 0; i < authLength-idx; i++){
        if(code[i] !== '' && code[i] !== undefined){
          auth.value[idx+i] = code[i];
          oldAuth[idx+i] = code[i];
        }
      }
      auth.value[idx] = code[0];
    }

    function onInputChane(event, idx) {
      let newValue = event.target.value;
      if(newValue.length <= 2) {
        if(newValue && newValue.length == 1) {
          auth.value[idx] = newValue;
          oldAuth[idx] = auth.value[idx];
          if(idx+1 < authLength)
            document.getElementById('2fa-' + (idx+1)).focus();
        } else if(newValue && newValue.length == 2) {
          auth.value[idx] = newValue.replace(oldAuth[idx], '');
          oldAuth[idx] = auth.value[idx];
          if(idx+1 < authLength)
            document.getElementById('2fa-' + (idx+1)).focus();
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
        document.getElementById('2fa-' + (idx-1)).focus();
      }
    }

    function checkValidCode() {
      let err = min(auth.value.join(''), authLength);
      if(err) {
        codeError.value = tm(err) + authLength;
      } else {
        context.emit('update:modelValue', auth.value.join(''));
        codeError.value = null;
      }
    }

    return { auth, codeError, authLength, deleteFromAuth, checkValidCode, onPaste, onInputChane }
  }
}
</script>