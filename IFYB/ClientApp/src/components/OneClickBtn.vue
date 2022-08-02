<template>
  <button type="button" class="btn d-flex align-items-center" :disabled="!active" @click="$emit('update:active', false)">
    {{text}}
    <span class="text-secondary text-xs font-weight-bold ms-2" v-if="icon">
      <i :class="`ni ni-${icon} opacity-10`"></i>
    </span>
  </button>
</template>

<script>
export default {
  name: 'OneClickBtn',
  props: {
    active: Boolean,
    text: String,
    icon: String
  },
  emits: ['update:active']
}
</script>