<template>
  <div class="carousel-item">
    <div class="container">
      <div class="row">
        <div class="col-lg-5 col-md-7 mx-auto">
          <div class="card">
            <div class="card-body px-lg-5 py-lg-5 text-center">
              <div class="info mb-4">
                <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                  <i class="ni ni-atom opacity-10 mt-2"></i>
                </div>
              </div>
              <h2>2FA Security</h2>
              <p class="mb-4">Enter 6-digits code from your athenticatior app.</p>
              <div class="row mb-4">
                <div class="col-lg-2 col-md-2 col-2 ps-0 ps-md-2" v-for="(i, idx) in authLength" :key="i">
                  <input type="text" :id="`2fa-${idx}`" class="form-control text-lg text-center" @keyup.enter="checkValidCode" @keyup.delete="deleteFromAuth(idx)" v-model="auth[idx]" aria-label="2fa" @paste="onPaste($event, idx)" @input="onInputChane($event, idx)">
                </div>
              </div>
              <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="codeError">
                {{codeError}}
              </div>
              <div class="alert alert-warning text-white font-weight-bold" role="alert" v-else-if="error">
                {{error}}
              </div>
              <div class="text-center">
                <button type="button" id="2fa-btn" class="btn bg-gradient-primary my-4" @click="checkValidCode">Check</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import { required, min } from '../utils/Validate';

export default {
  name: '2FA',
  props: {
    modelValue: String,
    error: String
  },
  emits:['update:modelValue'],
  setup(props, context) {
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
      let err = required(auth.value, 'Authentication code', '2fa-0')?.message || min(auth.value.join(''), 6);
      if(err) {
        codeError.value = err;
      } else {
        codeError.value = null;
        context.emit('update:modelValue', auth.value.join(''));
      }
    }

    return { auth, codeError, authLength, deleteFromAuth, checkValidCode, onPaste, onInputChane }
  }
}
</script>