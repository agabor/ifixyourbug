<template>
  <section class="py-lg-7" id="pricing">
    <div class="container">
      <div class="row">
        <div class="col-md-6 mx-auto text-center mb-5">
          <h2>{{ $t('pricing.mainTitle') }}</h2>
          <p class="lead">{{ $t('pricing.mainSubTitle') }}</p>
        </div>
      </div>
      <div class="row">
        <div class="card">
          <div class="row">
            <div class="col-lg-8">
              <div class="card-body">
                <h3 class="text-info">{{ $t('pricing.title') }}</h3>
                <p>{{ $t('pricing.description') }}</p>
                <div class="row mt-5 mb-2">
                  <div class="col-lg-3 col-12">
                    <h6 class="text-dark tet-uppercase">{{ $t('pricing.whatsincluded') }}</h6>
                  </div>
                  <div class="col-6">
                    <hr class="horizontal dark">
                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-12 ps-0">
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">{{ $t('pricing.include1') }}</span>
                      </div>
                    </div>
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">{{ $t('pricing.include2') }}</span>
                      </div>
                    </div>
                  </div>
                  <div class="col-lg-6 col-md-6 col-12 ps-sm-2 ps-0">
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">{{ $t('pricing.include3') }}</span>
                      </div>
                    </div>
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">{{ $t('pricing.include4') }}</span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-lg-4 my-auto">
              <div class="card-body text-center">
                <h6 class="mt-sm-4 mt-0 mb-0">{{ $t('pricing.payOne') }}</h6>
                <h1 class="mt-0">
                  <small>$</small>399
                </h1>
                <button type="button" class="btn bg-gradient-info btn-lg mt-2" @click="$router.push('/new-order')">{{ $t('pricing.orderNow') }}</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'PricingComponent',
}
</script>