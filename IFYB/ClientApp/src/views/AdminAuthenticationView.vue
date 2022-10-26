<template>
  <section>
    <div class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <authentication :isClient="false"></authentication>
      </div>
    </div>
  </section>
</template>

<script>
import Authentication from '../components/Authentication.vue';
import { useClientAuthentication } from "../store/client";

export default {
  name: 'AdminAuthenticationView',
  components: { Authentication },
  setup() {
    const { logout } = useClientAuthentication();
    logout();
    
    return {}
  }
}
</script>