<template>
  <carousel-item :icon="true" :title="$t('order.name')" :subTitle="$t('order.nameDes')">
    <template v-slot:icon>
      <i :class="`ni ni-badge opacity-10 mt-2`"></i>
    </template>
    <template v-slot:content>
      <div class="row mb-4">
        <input id="nameInput" class="form-control" ref="userNameInput" placeholder="Your Name" type="text" @keyup.enter="trySetName()" v-model="name" :disabled="!activeBtn">
      </div>
      <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error ? error: validationError">
        {{ error ? error: validationError }}
      </div>
      <div class="d-flex justify-content-center">
        <one-click-btn v-model:active="activeBtn" :text="$t('order.save')" class="bg-gradient-primary mx-2" @click="trySetName()"></one-click-btn>
      </div>
    </template>
  </carousel-item>
</template>

<script>
import { ref, watch, onMounted } from 'vue';
import { required } from '../utils/Validate';
import CarouselItem from '../components/CarouselItem.vue';
import OneClickBtn from '../components/OneClickBtn.vue';
import { useMessages } from "../store";

export default {
  name: 'NameForm',
  components: { CarouselItem, OneClickBtn },
  props: {
    modelValue: String,
    error: String,
    activeButton: Boolean
  },
  emits: [ 'update:modelValue', 'update:activeButton' ],
  setup(props, context) {
    const { tm } = useMessages();
    const name = ref(props.modelValue ?? '');
    const validationError = ref(null);
    const activeBtn = ref(props.activeButton ?? true);
    const userNameInput = ref(null);

    onMounted(() => {
      userNameInput.value.focus();
    })

    watch(props, () => {
      activeBtn.value = props.activeButton;
      name.value = props.modelValue;
    })

    function trySetName() {
      let err = required(name.value, tm('errors.requiredName'), 'nameInput');
      if(err) {
        validationError.value = err;
        activeBtn.value = true;
        userNameInput.value.focus();
      } else {
        validationError.value = null;
        context.emit('update:activeButton', activeBtn.value);
        context.emit('update:modelValue', name.value);
      }
    }

    return { validationError, name, activeBtn, userNameInput, trySetName }
  }
}
</script>