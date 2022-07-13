<template>
  <section>
        <div id="carousel-testimonials" class="page-header min-vh-100">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner">
                <div class="row">
                    <div class="col-lg-9 col-md-11 mx-auto my-4">
                        <div class="card">
                            <div class="card-body px-lg-5 py-lg-5 text-center">
                                <div v-if="loading">
                                    loading ...
                                </div>
                                <div v-if="order">
                                    Order {{order.number}}
                                    <form :action="`/api/pay/${route.params.token}`" method="post">
                                        <a class="btn btn-sm bg-gradient-primary btn-round" @click="pay">Pay</a>
                                    </form>
                                </div>
                                <div v-if="!order && !loading">
                                    Checkout link expired.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>
<script>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import { usePayment } from '@/store'
export default {
  setup() {
    const loading = ref(true);
    const order = ref(null);
    const route = useRoute();
    const payment = usePayment();
    fetch(`/api/pay/${route.params.token}`).then(resp => {
        resp.json().then(data => {
            order.value = data;
            loading.value = false;
        });
    });
    function pay() {
        fetch(`/api/pay/${route.params.token}`, { method: 'post' }).then(resp => {
            payment.setPaymentToken(route.params.token)
            resp.json().then(data => {
                window.location.href = data.url
            });
        });
    }
    return { loading, order, route, pay };
  }
}
</script>