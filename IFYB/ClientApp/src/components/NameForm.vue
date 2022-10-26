<template>
  <carousel-item :icon="true" :title="$t('order.name')" :subTitle="$t('order.nameDes')" :progress="progress">
    <template v-slot:icon>
      <i :class="`ni ni-badge opacity-10 mt-2`"></i>
    </template>
    <template v-slot:content>
      <div class="row mb-4">
        <input id="nameInput" class="form-control" ref="nameInput" placeholder="Your Name" type="text" @keyup.enter="trySetName()" v-model="name" :disabled="progress !== 0">
      </div>
      <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="validationError ? authenticationError: authenticationError">
        {{ validationError ? authenticationError: authenticationError }}
      </div>
      <div class="d-flex justify-content-center">
        <one-click-btn :active="progress === 0" :text="$t('order.save')" class="bg-gradient-primary mx-2" @click="trySetName()" :disabled="name === ''"></one-click-btn>
      </div>
    </template>
  </carousel-item>
</template>

<script>
import { ref, onMounted } from 'vue';
import { required } from '../utils/Validate';
import CarouselItem from '../components/CarouselItem.vue';
import OneClickBtn from '../components/OneClickBtn.vue';
import { useMessages } from "../store";
import { useAuthenticator } from '@/store/authentication';

export default {
  name: 'NameForm',
  components: { CarouselItem, OneClickBtn },
  props: {
    isClient: Boolean
  },
  setup(props) {
    const { progress, authenticationError, setName } = useAuthenticator(props.isClient);
    const { tm } = useMessages();
    const name = ref('');
    const validationError = ref(null);
    const nameInput = ref(null);
    progress.value = 0;

    onMounted(() => {
      nameInput.value.focus();
    })

    function trySetName() {
      let err = required(name.value, tm('errors.requiredName'), 'nameInput');
      if(err) {
        validationError.value = err;
        nameInput.value.focus();
      } else {
        validationError.value = null;
        setName(name.value);
      }
    }

    return { validationError, name, progress, authenticationError, nameInput, trySetName }
  }
}
</script>