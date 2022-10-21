<template>
  <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 clickable" @click="$emit('sort', title)">
    {{ $t(`thTitle.${title}`) }}
    <i class="ni ni-bold-up opacity-10 hideOrderBy" v-if="orderBy !== title && orderAsc"></i>
    <i class="ni ni-bold-down opacity-10 hideOrderBy" v-if="orderBy !== title && !orderAsc"></i>
    <i class="ni ni-bold-up opacity-10" v-if="orderBy === title && orderAsc"></i>
    <i class="ni ni-bold-down opacity-10" v-if="orderBy === title && !orderAsc"></i>
  </th>
</template>

<script>

export default {
  name: 'SortableTh',
  props: {
    title: String,
    orderBy: String,
    orderAsc: Boolean
  },
  emits: ['sort'],
}
</script>

<style scoped>
  .clickable {
    cursor: pointer;
  }
  .hideOrderBy {
    visibility: hidden;
  }
  .clickable:hover > .hideOrderBy {
    visibility: visible;
    opacity: 0.4;
  }
</style>