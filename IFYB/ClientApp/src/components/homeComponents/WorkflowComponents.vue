<template>
  <section>
    <div class="container mt-7">
      <div class="row">
        <div class="col-lg-5 ms-auto me-auto text-center">
          <div class="p-3 info-hover-warning d-flex justify-content-center">
            <div class="icon icon-shape bg-gradient-warning icon-shape-circle text-primary">
              <i class="ni ni-bulb-61 opacity-10"></i>
            </div>
          </div>
          <h3 class="mb-0 mt-4">{{ $t('workflow.title1') }}</h3>
          <p v-html="$t('workflow.subTitle')"></p>
        </div>
      </div>
      <div class="row mt-5">
        <div class="col-md-4 ms-auto my-auto">
          <div class="card card-background">
            <picture class="full-background">
              <source 
                media="(min-width: 576px)"
                srcset="../../assets/img/bg2_small.webp">
              <img
                class="full-background fit-cover"
                src="../../assets/img/bg2_small_mobile.webp" 
                alt="image">
            </picture>
            <div class="card-body pt-7 pb-6 text-center">
              <i class="ni ni-send mb-3 h3 text-white"></i>
              <p class="h4 d-block text-white up mb-0 text-decoration-underline-hover">{{ $t('features.easy') }}</p>
              <p class="lead mt-2 opacity-9">{{ $t('features.easyDescription') }}</p>
              <button type="button" class="btn btn-white shadow-none mt-3" @click="toPricing">{{ $t('workflow.getStarted') }}</button>
            </div>
          </div>
        </div>
        <div class="col-md-5 me-auto my-auto ms-md-5">
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('workflow.workflowTitle1') }}</h5>
              <p>{{ $t('workflow.workflowDescription1') }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('workflow.workflowTitle2') }}</h5>
              <p>{{ $t('workflow.workflowDescription2') }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('workflow.workflowTitle3') }}</h5>
              <p v-html="$t('workflow.workflowDescription3', { eurPrice: eurPriceForDisplay, usdPrice: usdPriceForDisplay })"></p>
            </div>
          </div>
        </div>
      </div>
      <hr class="dark my-md-6 mt-2 mx-7">
      <div class="row my-5">
        <div class="col-md-5 ms-auto my-auto">
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('workflow.workflowTitle4') }}</h5>
              <p>{{ $t('workflow.workflowDescription4', { workdays }) }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('workflow.workflowTitle5') }}</h5>
              <p>{{ $t('workflow.workflowDescription5') }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('workflow.workflowTitle6') }}</h5>
              <p>{{ $t('workflow.workflowDescription6') }}</p>
            </div>
          </div>
        </div>
        <div class="col-md-4 me-auto my-auto ms-md-5">
          <div class="card card-background">
            <picture class="full-background">
              <source 
                media="(min-width: 576px)"
                srcset="../../assets/img/bg3.webp">
              <img
                class="full-background fit-cover"
                src="../../assets/img/bg3_mobile.webp" 
                alt="image">
            </picture>
            <div class="card-body pt-7 pb-6 text-center">
              <i class="ni ni-watch-time mb-3 h3 text-white"></i>
              <p class="h4 text-decoration-underline-hover d-block text-white up mb-0">{{ $t('features.fast') }}</p>
              <p class="lead mt-2 opacity-9">{{ $t('features.fastDescription', { workdays }) }}</p>
              <button type="button" class="btn btn-white mt-3" @click="toPricing">{{ $t('workflow.getStarted') }}</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row my-5 mx-1">
        <div class="col-md-5 col-lg-3 me-auto my-auto ms-auto">
          <img class="mw-100" src="@/assets/img/stripe-badge-grey.png" alt="stripe">
        </div>
    </div>
  </section>
</template>

<script>
import { useSettings } from "../../store";
import { computed } from 'vue';

export default {
  name: 'WorkflowComponents',
  setup() {

    const { eurPrice, usdPrice, workdays } = useSettings();

    const eurPriceForDisplay = computed(() => parseFloat(eurPrice.value).toFixed(2));

    const usdPriceForDisplay = computed(() => parseFloat(usdPrice.value).toFixed(2));

    function toPricing() {
      window.rdt('track', 'ViewContent');
      document.getElementById('pricing').scrollIntoView();
    }
    return { toPricing, eurPriceForDisplay, usdPriceForDisplay, workdays }
  }
}
</script>
