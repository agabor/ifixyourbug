<template>
<span class="bg"></span>
<span class="mask bg-gradient-dark opacity-4"></span>
  <section class="pt-3 pt-md-5 pt-lg-7 pb-md-5 pb-lg-7">
    <div class="container">
      <div class="col-lg-8 col-md-10 mx-auto">
        <div class="card">
          PrivacyPolicy
        </div>
      </div>
    </div>
  </section>
</template>


<script>
import { useServerError } from "../store";

export default {
  name: 'PrivacyPolicy',
  setup() {
    const { setServerError } = useServerError();
    
    setServerError(null);

    return { };
  }
}
</script>

<style scoped>
.bg {
  position: absolute;
  background-size: cover;
  background-position: center center;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-image: url('../assets/img/pricing3.jpg');
}
</style>