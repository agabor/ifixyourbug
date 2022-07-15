<template>
  <div class="container">
    <div class="row">
      <div class="col-12 mx-auto">
        <div class="d-flex flex-column align-items-start justify-content-start my-1" v-if="isMyMessage">
          <p class="m-0 mx-2 fw-lighter small" v-if="diffMinutes() > 5 || prevIsMyMessage !==isMyMessage || $filters.dateFormat(prevDateTime) !== $filters.dateFormat(time)">{{ $filters.timeFormat(time) }}</p>
          <p class="bg-gradient-primary m-0 my-message" data-bs-toggle="tooltip" data-bs-placement="right" :title="$filters.dateTimeFormat(time)">{{ message }}</p>
        </div>
        <div class="d-flex flex-column align-items-end justify-content-end my-1" v-else>
          <p class="m-0 mx-2 fw-lighter small" v-if="diffMinutes() > 5 || prevIsMyMessage !==isMyMessage || $filters.dateFormat(prevDateTime) !== $filters.dateFormat(time)">{{ $filters.timeFormat(time) }}</p>
          <p class="m-0 admin-message" data-bs-toggle="tooltip" data-bs-placement="right" :title="$filters.dateTimeFormat(time)">{{ message }}</p>
        </div>
      </div>
    </div>
    <p class="fw-lighter" v-if="prevDateTime == null || $filters.dateFormat(prevDateTime) !== $filters.dateFormat(time)">{{ $filters.dateFormat(time) }}</p>
  </div>
</template>

<script>

export default {
  name: 'MessageLine',
  props: {
    message: String,
    isMyMessage: Boolean,
    time: String,
    prevDateTime: String,
    prevIsMyMessage: Boolean
  },
  setup (props) {
    function diffMinutes() {
      var diff = (new Date(props.time).getTime() - new Date(props.prevDateTime).getTime()) / 1000;
      diff /= 60;
      return Math.abs(Math.round(diff));      
    }
    return {diffMinutes}
  }
}
</script>

<style scoped>
.my-message {
  padding: 0.5rem 1rem;
  border-radius: 30px;
  color: white;
  white-space: pre-wrap;
  text-align: left;
}
.admin-message {
  padding: 0.5rem 1rem;
  border: 1px solid #dedede;
  border-radius: 30px;
  white-space: pre-wrap;
  text-align: left;
}
</style>