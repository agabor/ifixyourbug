<template>
  <section class="bg-dark position-relative mt-7" id="pricing">
    <div class="container py-5">
      <div class="row">
        <div class="col-lg-4 col-md-12 d-flex justify-content-center flex-column text-lg-start text-center ">
          <h3 class="text-white">{{ $t('pricing.title') }}</h3>
          <p class="lead">{{ $t('pricing.description', { workdays: workdays }) }}</p>
        </div>
        <div class="col-lg-8 col-md-12 ms-lg-auto me-lg-auto">
          <div class="row">
            <div class="col-md-6 mt-4">
              <div class="card card-pricing bg-white border-0 text-center h-100">
                <div class="card-header bg-transparent">
                  <h6 class="text-uppercase ls-1">{{ $t('pricing.include1') }}</h6>
                  <svg class="mx-2" width="50" height="50">
                    <image href="@/assets/img/logos/dotnet.svg" height="50" width="50" />
                  </svg>
                  <svg class="mx-2" width="50" height="50">
                    <image href="@/assets/img/logos/vuejs.svg" height="50" width="50" />
                  </svg>
                </div>
                <div class="card-body text-dark py-0">
                  <h6 class="m-0">{{ $t('pricing.payOne') }}</h6>
                  <h3 v-if="isEuropean">
                    € {{ parseFloat(eurPrice).toFixed(2) }}
                  </h3>
                  <h3 v-else>
                    $ {{ parseFloat(usdPrice).toFixed(2) }}
                  </h3>
                  <span class="fst-italic"><small>{{$t('pricing.excludeVat')}}</small></span>
                </div>
                <div class="card-footer bg-transparent pt-2">
                  <button class="btn bg-gradient-primary" @click="toNewOrderView()">{{ $t('pricing.orderNow') }}</button>
                </div>
              </div>
            </div>
            <div class="col-md-6 mt-4">
              <div class="card card-pricing bg-white border-0 text-center h-100">
                <div class="card-header bg-transparent">
                  <h6 class="text-uppercase ls-1">{{ $t('pricing.include2') }}</h6>
                  <img height="50" src="@/assets/img/code_review.webp" alt="code review">
                </div>
                <div class="card-body text-dark py-0">
                  <h6 class="mt-0">{{ $t('pricing.payOne') }}</h6>
                  <h3 v-if="isEuropean">
                    € {{ parseFloat(eurPrice).toFixed(2) }}
                  </h3>
                  <h3 v-else>
                    $ {{ parseFloat(usdPrice).toFixed(2) }}
                  </h3>
                  <span class="fst-italic"><small>{{$t('pricing.excludeVat')}}</small></span>
                </div>
                <div class="card-footer bg-transparent pt-2">
                  <button class="btn bg-gradient-dark" @click="toNewCodeReview()">{{ $t('pricing.orderNow') }}</button>
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
import router from "@/router";
import { useSettings } from "../../store";

export default {
  name: 'PricingComponent',
  setup() {
    const { isEuropean, eurPrice, usdPrice, workdays } = useSettings();
    let order = { applicationUrl: null, bugDescription: "", accessMode: null, flag: 1, selectedAccess: {} };
    let tempOrder = JSON.parse(localStorage.getItem('order'));
    Object.assign(order, tempOrder);

    function toNewOrderView() {
      window.rdt('track', 'AddToCart');
      order.flag = 1;
      localStorage.setItem('order', JSON.stringify(order));
      router.push('new-order');
    }

    function toNewCodeReview() {
      window.rdt('track', 'AddToCart');
      order.flag = 2;
      localStorage.setItem('order', JSON.stringify(order));
      router.push('new-order');
    }

    return { isEuropean, eurPrice, usdPrice, workdays, toNewOrderView, toNewCodeReview }
  }
}
</script>