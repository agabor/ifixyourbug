<template>
  <section class="py-lg-7 pb-5">
    <div class="bg-gradient-dark position-relative">
      <div class="container pb-lg-9 pb-7 pt-7 postion-relative z-index-2">
        <div class="row">
          <div class="col-md-8 mx-auto text-center">
            <span class="badge bg-info mb-2">{{ $t('otherServices.pricing') }}</span>
            <h3 class="text-white">{{ $t('otherServices.title') }}</h3>
            <p class="text-white">{{ $t('otherServices.subTitle') }}</p>
          </div>
        </div>
      </div>
    </div>
    <div class="mt-sm-n8 mt-n7">
      <div class="container">
        <div class="row">
          <div class="col-lg-3 mb-lg-auto mb-4 my-auto p-md-0 ms-auto">
            <div class="card border-radius-top-end-lg-0 border-radius-bottom-end-lg-0">
              <div class="card-header text-center pt-4 pb-3">
                <h6 class="text-dark opacity-8 text mb-0">{{ $t('otherCard1.title') }}</h6>
                <h1 class="font-weight-bolder">
                  <small>$</small>{{ $t('otherCard1.price') }}
                </h1>
              </div>
              <div class="card-body mx-auto pt-0">
                <div class="justify-content-start d-flex px-2 py-1" v-for="n in 4" :key="n">
                  <div>
                    <i class="fas fa-check text-info text-sm"></i>
                  </div>
                  <div class="ps-2">
                    <span class="text-sm"> {{ $t(`otherCard1.element${n}`) }}</span>
                  </div>
                </div>
              </div>
              <div class="card-footer pt-0">
                <a href="javascript:;" class="btn w-100 bg-gradient-dark mb-0">
                  {{ $t('otherCard1.button') }}
                </a>
              </div>
            </div>
          </div>
          <div class="col-lg-3 p-md-0 mb-lg-auto mb-4 z-index-2">
            <div class="card">
              <div class="card-header text-center pt-4 pb-3">
                <h6 class="text-dark opacity-8 text mb-0">{{ $t('otherCard2.title') }}</h6>
                <h1 class="font-weight-bolder">
                  <small>$</small>{{ $t('otherCard2.price') }}
                </h1>
              </div>
              <div class="card-body mx-auto pt-0">
                <div class="justify-content-start d-flex px-2 py-1" v-for="n in 5" :key="n">
                  <div>
                    <i class="fas fa-check text-info text-sm"></i>
                  </div>
                  <div class="ps-2">
                    <span class="text-sm"> {{ $t(`otherCard2.element${n}`) }}</span>
                  </div>
                </div>
              </div>
              <div class="card-footer pt-0">
                <a href="javascript:;" class="btn btn-icon bg-gradient-info d-block mb-0">
                  {{ $t('otherCard2.button') }}
                  <i class="fas fa-arrow-right ms-1"></i>
                </a>
              </div>
            </div>
          </div>
          <div class="col-lg-3 mb-lg-auto mb-4 my-auto p-md-0 me-auto">
            <div class="card border-radius-top-start-lg-0 border-radius-bottom-start-lg-0">
              <div class="card-header text-center pt-4 pb-2">
                <h6 class="text-dark opacity-8 text mb-0">{{ $t('otherCard3.title') }}</h6>
                <h1 class="font-weight-bolder">
                  <small>$</small>{{ $t('otherCard3.price') }}
                </h1>
              </div>
              <div class="card-body mx-auto pt-0">
                <div class="justify-content-start d-flex px-2 py-1" v-for="n in 4" :key="n">
                  <div>
                    <i class="fas fa-check text-info text-sm"></i>
                  </div>
                  <div class="ps-2">
                    <span class="text-sm"> {{ $t(`otherCard3.element${n}`) }}</span>
                  </div>
                </div>
              </div>
              <div class="card-footer pt-0">
                <a href="javascript:;" class="btn w-100 bg-gradient-dark mb-0">
                  {{ $t('otherCard3.button') }}
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { useServerError } from "../store";

export default {
  name: 'OtherServices',
  setup() {
    const { setServerError } = useServerError();
    setServerError(null);

    return {};
  }
}
</script>


