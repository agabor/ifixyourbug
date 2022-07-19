<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item class="full-height" :class="{'active': page === 'data'}" width="col-lg-9 col-md-11" icon="spaceship" :title="$t('order.orderData')" :subTitle="$t('order.orderDataDes')">
          <new-order-form :gitAccesses="gitAccesses" @toSuccessPage="page = 'success'"></new-order-form>
        </carousel-item>
        <carousel-item :class="{'active': page === 'success'}" icon="send" :title="$t('order.successfulOrder')" :subTitle="$t('order.successfulOrderDes')" :buttonText="$t('order.backToHome')" @onClickBtn="$router.push('/')"></carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import CarouselItem from '../components/CarouselItem.vue';
import NewOrderForm from '../components/NewOrderForm.vue';
import { useServerError, useUserAuthentication } from "../store";

export default {
  name: 'NewOrderView',
  components: { CarouselItem, NewOrderForm },
  setup() {
    const { setServerError } = useServerError();
    const { get } = useUserAuthentication();
    const page = ref('');
    const gitAccesses = ref([]);

    page.value = 'data';
    setServerError(null);
    setGitAccesses();
    
    async function setGitAccesses() {
      let response = await get('/api/git-accesses');
      if(response.status == 200) {
        setServerError(null);
        gitAccesses.value = await response.json();
      } else {
        setServerError(response.statusText);
      }
    }

    return { page, gitAccesses }
  }
}
</script>

<style>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/pricing3.jpg');
}
.full-height{
  overflow: auto;
  height: 100vh;
}
</style>