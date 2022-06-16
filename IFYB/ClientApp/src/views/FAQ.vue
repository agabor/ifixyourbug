<template>
<span class="bg"></span>
<span class="mask bg-gradient-dark opacity-4"></span>
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
              <div class="accordion-item" v-for="n in faqBasicsCount" :key="n">
                <h6 class="accordion-header" :id="`headingBasics${n}`">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseBasics${n}`" aria-expanded="false" :aria-controls="`collapseBasics${n}`">
                    {{ $t(`faqBasics.basicTitle${n}`) }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div :id="`collapseBasics${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingBasics${n}`" data-bs-parent="#accordionFaq">
                  <div class="accordion-body text-sm opacity-8">{{ $t(`faqBasics.basicDes${n}`) }}</div>
                </div>
              </div>
            </div>
            <h4 class="mt-5 mb-4 ps-3">{{ $t('faqSettings.title') }}</h4>
            <div class="accordion" id="accordionFaq2">
              <div class="accordion-item" v-for="n in faqSettings" :key="n">
                <h6 class="accordion-header" :id="`headingSettings${n}`">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseSettings${n}`" aria-expanded="false" :aria-controls="`collapseSettings${n}`">
                    {{ $t(`faqSettings.settingTitle${n}`) }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div :id="`collapseSettings${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingSettings${n}`" data-bs-parent="#accordionFaq2">
                  <div class="accordion-body text-sm opacity-8">{{ $t(`faqSettings.settingDes${n}`) }}</div>
                </div>
              </div>
            </div>
            <h4 class="mt-5 mb-4 ps-3">{{ $t('faqLicenses.title') }}</h4>
            <div class="accordion" id="accordionFaq3">
              <div class="accordion-item" v-for="n in faqLicenses" :key="n">
                <h6 class="accordion-header" :id="`headingLicenses${n}`">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseLicenses${n}`" aria-expanded="false" :aria-controls="`collapseLicenses${n}`">
                    {{ $t(`faqLicenses.licenseTitle${n}`) }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div :id="`collapseLicenses${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingLicenses${n}`" data-bs-parent="#accordionFaq3">
                  <div class="accordion-body text-sm opacity-8">{{ $t(`faqLicenses.licenseDes${n}`) }}</div>
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


<script>
export default {
  setup() {
    const faqBasicsCount = 5;
    const faqSettings = 5;
    const faqLicenses = 4;

    return { faqBasicsCount, faqSettings, faqLicenses };
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