<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header mt-5">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner">
          <authentication :isClient="true"></authentication>
        </div>
      </div>
      <footer-component></footer-component>
    </div>
  </section>
</template>

<script>
import Authentication from '../components/Authentication.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import { useAdminAuthentication } from "../store/admin";

export default {
  name: 'AuthenticationView',
  components: { Authentication, FooterComponent },
  setup() {
    const { logout } = useAdminAuthentication();
    logout();

    return {}
  }
}
</script>