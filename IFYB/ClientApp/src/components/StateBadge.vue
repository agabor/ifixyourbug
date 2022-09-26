<template>
  <span :class="{'badge-warning': isSimple, 'text-white': !isSimple, 'bg-warning': !isSimple}" v-if="state == 0">{{ $t('orderState.submitted') }}</span>
  <span :class="{'badge-info': isSimple, 'text-white': !isSimple, 'bg-info': !isSimple}"  v-else-if="state == 1">{{ $t('orderState.confirmed') }}</span>
  <span :class="{'badge-light': isSimple, 'bg-light': !isSimple}" v-else-if="state == 2">{{ $t('orderState.rejected') }}</span>
  <span :class="{'badge-light': isSimple, 'bg-light': !isSimple}" v-else-if="state == 3">{{ $t('orderState.payed') }}</span>
  <span :class="{'badge-success': isSimple, 'text-white': !isSimple, 'bg-success': !isSimple}" v-else-if="state == 4">{{ $t('orderState.completed') }}</span>
  <span :class="{'badge-danger': isSimple, 'text-white': !isSimple, 'bg-danger': !isSimple}" v-else-if="state == 5">{{ $t('orderState.refundable') }}</span>
  <span :class="{'badge-light': isSimple, 'bg-light': !isSimple}" v-else-if="state == 6">{{ $t('orderState.canceled') }}</span>
  <span :class="{'badge-dark': isSimple, 'text-white': !isSimple, 'bg-dark': !isSimple}" v-else-if="state == 7">{{ $t('orderState.editable') }}</span>
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