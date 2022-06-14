<template>
  <div class="d-flex w-100 position-fixed bottom-1 justify-content-center z-index-3">
    <div class="d-flex alert alert-warning text-white font-weight-bold w-80 justify-content-center" role="alert" v-if="serverError">
      {{ serverError }}
    </div>
  </div>
  <router-view/>
</template>

<script>
import { useServerError } from "./store";

export default {
  name: 'AppView',
  setup() {
    const { serverError } = useServerError();
    return { serverError }
  }
}
</script>

<style>
nav a.router-link-exact-active {
  color: #ed3269;
}
</style>
