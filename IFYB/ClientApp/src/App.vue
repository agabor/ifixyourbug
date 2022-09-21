<template>
  <navigation-bar></navigation-bar>
  <cookie-modal></cookie-modal>
  <timeout-modal></timeout-modal>
  <div class="d-flex w-100 position-fixed bottom-1 justify-content-center z-index-3">
    <div class="d-flex alert alert-warning text-white font-weight-bold w-80 justify-content-center" role="alert" v-if="serverError">
      {{ serverError }}
    </div>
  </div>
  <router-view/>
</template>

<script>
import { useServerError } from "./store";
import NavigationBar from "./components/NavigationBar.vue";
import CookieModal from "./components/CookieModal.vue";
import TimeoutModal from "./components/TimeoutModal.vue";

export default {
  name: "AppView",
  components: { NavigationBar, CookieModal, TimeoutModal },
  setup() {
    const { serverError } = useServerError();
      
    return { serverError };
  },
}
</script>

<style>
nav a.router-link-exact-active {
  color: #ed3269;
}
.fit-cover {
  object-fit: cover;
}

.full-bg {
  position: absolute;
  background-size: cover;
  background-position: center center;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.full-height {
  overflow: auto;
  height: 100vh;
}

@media (min-width: 576px) {
  .full-bg {
    background-image: url('./assets/img/bg2.webp');
  }
}
@media (max-width: 576px) {
  .full-bg {
    background-image: url('./assets/img/bg2_mobile.webp');
  }
}

@media (min-width: 576px) {
  .page-header {
    background-image: url('./assets/img/bg2.webp');
  }
}
@media (max-width: 576px) {
  .page-header {
    background-image: url('./assets/img/bg2_mobile.webp');
  }
}

</style>
