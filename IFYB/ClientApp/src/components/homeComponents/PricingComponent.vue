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
              <div class="card card-pricing bg-white border-0 text-center">
                <div class="card-header bg-transparent">
                  <h6 class="text-uppercase ls-1">{{ $t('pricing.include1') }}</h6>
                  <svg width="50" height="50">
                    <image href="@/assets/img/logos/dotnet.svg" height="50" width="50" />
                  </svg>
                </div>
                <div class="card-body text-dark py-0">
                  <h6 class="m-0">{{ $t('pricing.payOne') }}</h6>
                  <h3 class="mb-0">
                    $ {{ parseFloat(usdPrice).toFixed(2) }}
                  </h3>
                  <h4><small>or</small></h4>
                  <h3>
                    € {{ parseFloat(eurPrice).toFixed(2) }}
                  </h3>
                  <span class="fst-italic"><small>{{$t('pricing.excludeVat')}}</small></span>
                </div>
                <div class="card-footer bg-transparent pt-2">
                  <button class="btn bg-gradient-primary" @click="toNewOrderView()">{{ $t('pricing.orderNow') }}</button>
                </div>
              </div>
            </div>
            <div class="col-md-6 mt-4">
              <div class="card card-pricing bg-white border-0 text-center">
                <div class="card-header bg-transparent">
                  <h6 class="text-uppercase ls-1">{{ $t('pricing.include2') }}</h6>
                  <img width="50" height="50" src="@/assets/img/logos/vuejs.svg" alt="dotnet">
                </div>
                <div class="card-body text-dark py-0">
                  <h6 class="mt-0">{{ $t('pricing.payOne') }}</h6>
                  <h3 class="mb-0">
                    $ {{ parseFloat(usdPrice).toFixed(2) }}
                  </h3>
                  <h4><small>or</small></h4>
                  <h3>
                    € {{ parseFloat(eurPrice).toFixed(2) }}
                  </h3>
                  <span class="fst-italic"><small>{{$t('pricing.excludeVat')}}</small></span>
                </div>
                <div class="card-footer bg-transparent pt-2">
                  <button class="btn bg-gradient-primary" @click="toNewOrderView()">{{ $t('pricing.orderNow') }}</button>
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
    const { eurPrice, usdPrice, workdays } = useSettings();

    function toNewOrderView() {
      window.rdt('track', 'AddToCart');
      router.push('new-order');
    }

    return { eurPrice, usdPrice, workdays, toNewOrderView }
  }
}
</script>