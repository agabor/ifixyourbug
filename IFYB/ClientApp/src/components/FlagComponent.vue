<template>
  <button type="button" class="btn py-1 px-3 ms-2 my-0" :class="{'bg-gradient-dark': flag === 0, 'bg-gradient-secondary': flag !== 0}">
    <span>{{ flag === 0 ? $t('flag.bugfix') : $t('flag.codeReview') }}</span>
  </button>
</template>
  
<script>
export default {
  name: 'FlagComponent',
  props: {
    flag: Number
  },
}
</script>