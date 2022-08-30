<template>
  <span :class="`${isSimple ? 'badge' : 'text-white bg'}-warning`" v-if="state == 0">{{ $t('orderState.submitted') }}</span>
  <span :class="`${isSimple ? 'badge' : 'text-white bg'}-info`" v-else-if="state == 1">{{ $t('orderState.accepted') }}</span>
  <span :class="`${isSimple ? 'badge' : 'bg'}-light`" v-else-if="state == 2">{{ $t('orderState.rejected') }}</span>
  <span :class="`${isSimple ? 'badge' : 'bg'}-light`" v-else-if="state == 3">{{ $t('orderState.payed') }}</span>
  <span :class="`${isSimple ? 'badge' : 'text-white bg'}-success`" v-else-if="state == 4">{{ $t('orderState.completed') }}</span>
  <span :class="`${isSimple ? 'badge' : 'text-white bg'}-danger`" v-else-if="state == 5">{{ $t('orderState.refundable') }}</span>
  <span :class="`${isSimple ? 'badge' : 'bg'}-light`" v-else-if="state == 6">{{ $t('orderState.canceled') }}</span>
  <span :class="`${isSimple ? 'badge' : 'text-white bg'}-dark`" v-else-if="state == 7">{{ $t('orderState.editable') }}</span>
</template>
  
<script>
export default {
  name: 'StateBadge',
  props: {
    state: Number,
    isSimple: Boolean
  },
}
</script>