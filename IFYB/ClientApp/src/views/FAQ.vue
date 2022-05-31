<template>
  <section class="pt-3 pt-md-5 pt-lg-7 pb-md-5 pb-lg-7">
    <div class="container">
      <div class="col-lg-8 col-md-10 mx-auto">
        <div class="card">
          <div class="card-header bg-gradient-primary p-5 position-relative">
            <h3 class="text-white mb-0">{{ $t('faqCard.title') }}</h3>
            <p class="text-white opacity-8 mb-4">{{ $t('faqCard.lastModified', { date: 'Feb 01 2021' }) }}</p>
          </div>
          <div class="card-body p-sm-5 pt-0">
            <h4 class="my-4 ps-3">{{ $t('faqBasics.title') }}</h4>
            <div class="accordion" id="accordionFaq">
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingOne">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    {{ $t('faqBasics.basicTitle1') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionFaq">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqBasics.basicDes1') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingTwo">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                    {{ $t('faqBasics.basicTitle2') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionFaq">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqBasics.basicDes2') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingThree">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                    {{ $t('faqBasics.basicTitle3') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionFaq">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqBasics.basicDes3') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingFour">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                    {{ $t('faqBasics.basicTitle4') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseFour" class="accordion-collapse collapse" aria-labelledby="headingFour" data-bs-parent="#accordionFaq">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqBasics.basicDes4') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingFifth">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFifth" aria-expanded="false" aria-controls="collapseFifth">
                    {{ $t('faqBasics.basicTitle5') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseFifth" class="accordion-collapse collapse" aria-labelledby="headingFifth" data-bs-parent="#accordionFaq">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqBasics.basicDes5') }}</div>
                </div>
              </div>
            </div>
            <h4 class="mt-5 mb-4 ps-3">{{ $t('faqSettings.title') }}</h4>
            <div class="accordion" id="accordionFaq2">
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingOne1">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne1" aria-expanded="false" aria-controls="collapseOne1">
                    {{ $t('faqSettings.settingTitle1') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseOne1" class="accordion-collapse collapse" aria-labelledby="headingOne1" data-bs-parent="#accordionFaq2">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqSettings.settingDes1') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingTwo1">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo1" aria-expanded="false" aria-controls="collapseTwo1">
                    {{ $t('faqSettings.settingTitle2') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseTwo1" class="accordion-collapse collapse" aria-labelledby="headingTwo1" data-bs-parent="#accordionFaq2">
                  <div class="accordion-body text-sm opacity-8" v-html="$t('faqSettings.settingDes2')"></div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingThree1">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree1" aria-expanded="false" aria-controls="collapseThree1">
                    {{ $t('faqSettings.settingTitle3') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseThree1" class="accordion-collapse collapse" aria-labelledby="headingThree1" data-bs-parent="#accordionFaq2">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqSettings.settingDes3') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingFour1">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour1" aria-expanded="false" aria-controls="collapseFour1">
                   {{ $t('faqSettings.settingTitle4') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseFour1" class="accordion-collapse collapse" aria-labelledby="headingFour1" data-bs-parent="#accordionFaq2">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqSettings.settingDes4') }}</div>
                </div>
              </div>
            </div>
            <h4 class="mt-5 mb-4 ps-3">{{ $t('faqLicenses.title') }}</h4>
            <div class="accordion" id="accordionFaq3">
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingOne2">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne2" aria-expanded="false" aria-controls="collapseOne2">
                    {{ $t('faqLicenses.licenseTitle1') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseOne2" class="accordion-collapse collapse" aria-labelledby="headingOne2" data-bs-parent="#accordionFaq3">
                  <div class="accordion-body text-sm opacity-8">
                    {{ $t('faqLicenses.licenseDes1') }}
                  </div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingTwo2">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo2" aria-expanded="false" aria-controls="collapseTwo2">
                    {{ $t('faqLicenses.licenseTitle2') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseTwo2" class="accordion-collapse collapse" aria-labelledby="headingTwo2" data-bs-parent="#accordionFaq3">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqLicenses.licenseDes2') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingThree2">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree2" aria-expanded="false" aria-controls="collapseThree2">
                    {{ $t('faqLicenses.licenseTitle3') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseThree2" class="accordion-collapse collapse" aria-labelledby="headingThree2" data-bs-parent="#accordionFaq3">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqLicenses.licenseDes3') }}</div>
                </div>
              </div>
              <div class="accordion-item mb-3">
                <h6 class="accordion-header" id="headingFour2">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour2" aria-expanded="false" aria-controls="collapseFour2">
                    {{ $t('faqLicenses.licenseTitle4') }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div id="collapseFour2" class="accordion-collapse collapse" aria-labelledby="headingFour2" data-bs-parent="#accordionFaq3">
                  <div class="accordion-body text-sm opacity-8">{{ $t('faqLicenses.licenseDes4') }}</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- -------- START Features FAQ w/ 2 cols w/ white icon on colored background & title & text -------- -->
  <section class="mt-sm-0 mt-5">
    <div class="container">
      <div class="row justify-content-center text-center">
        <div class="col-md-4 ms-auto">
          <div class="p-3">
            <div class="icon icon-shape bg-gradient-primary text-center">
              <i class="ni ni-world opacity-10"></i>
            </div>
            <h5 class="mt-3">{{ $t('faqFeatures.detailedDocumentation') }}</h5>
            <p>{{ $t('faqFeatures.detailedDocumentationDes') }}</p>
          </div>
        </div>
        <div class="col-md-4 me-auto">
          <div class="p-3">
            <div class="icon icon-shape bg-gradient-primary text-center">
              <i class="ni ni-email-83 opacity-10"></i>
            </div>
            <h5 class="mt-3">{{ $t('faqFeatures.awesomeSupport') }}</h5>
            <p>{{ $t('faqFeatures.awesomeSupportDes') }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>