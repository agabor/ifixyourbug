<template>
  <div class="container">
    <div class="row">
      <div class="col-12 mx-auto">
        <div class="d-flex align-items-center justify-content-start" v-if="isMyMessage">
          <p class="bg-gradient-primary my-message" data-bs-toggle="tooltip" data-bs-placement="right" :title="$filters.dateTimeFormat(time)">{{ message }}</p>
        </div>
        <div class="d-flex align-items-center justify-content-end" v-else>
          <p class="admin-message" data-bs-toggle="tooltip" data-bs-placement="right" :title="$filters.dateTimeFormat(time)">{{ message }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  name: 'MessageLine',
  props: {
    message: String,
    isMyMessage: Boolean,
    time: String
  },
}
</script>

<style scoped>
.my-message {
  padding: 0.5rem 1rem;
  border-radius: 30px;
  color: white;
}
.admin-message {
  padding: 0.5rem 1rem;
  border: 1px solid #dedede;
  border-radius: 30px;
}
</style>