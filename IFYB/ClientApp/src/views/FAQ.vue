<template>
<span class="bg"></span>
<span class="mask bg-gradient-dark opacity-4"></span>
  <section class="pt-7 pb-md-3">
    <div class="container">
      <div class="col-lg-8 col-md-10 mx-auto">
        <div class="card">
          <div class="card-header bg-gradient-primary p-5 position-relative">
            <h3 class="text-white mb-0">{{ $t('faqCard.title') }}</h3>
            <p class="text-white opacity-8 mb-4">{{ $t('faqCard.lastModified', { date: 'July 22, 2022' }) }}</p>
          </div>
          <div class="card-body p-sm-5 pt-0">
            <h4 class="my-4 ps-3">{{ $t('faqSecurity.title') }}</h4>
            <div class="accordion" id="accordionFaq">
              <div class="accordion-item" v-for="n in faqSecurityCount" :key="n">
                <h6 class="accordion-header" :id="`headingBasics${n}`">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseBasics${n}`" aria-expanded="false" :aria-controls="`collapseBasics${n}`">
                    {{ $t(`faqSecurity.question${n}`) }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div :id="`collapseBasics${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingBasics${n}`" data-bs-parent="#accordionFaq">
                  <div class="accordion-body text-sm opacity-8">{{ $t(`faqSecurity.answer${n}`) }}</div>
                </div>
              </div>
            </div>
            <h4 class="mt-5 mb-4 ps-3">{{ $t('faqOrders.title') }}</h4>
            <div class="accordion" id="accordionFaq2">
              <div class="accordion-item" v-for="n in faqOrders" :key="n">
                <h6 class="accordion-header" :id="`headingSettings${n}`">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseSettings${n}`" aria-expanded="false" :aria-controls="`collapseSettings${n}`">
                    {{ $t(`faqOrders.question${n}`) }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div :id="`collapseSettings${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingSettings${n}`" data-bs-parent="#accordionFaq2">
                  <div class="accordion-body text-sm opacity-8">{{ $t(`faqOrders.answer${n}`) }}</div>
                </div>
              </div>
            </div>
            <h4 class="mt-5 mb-4 ps-3">{{ $t('faqRefunds.title') }}</h4>
            <div class="accordion" id="accordionFaq3">
              <div class="accordion-item" v-for="n in faqRefunds" :key="n">
                <h6 class="accordion-header" :id="`headingLicenses${n}`">
                  <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseLicenses${n}`" aria-expanded="false" :aria-controls="`collapseLicenses${n}`">
                    {{ $t(`faqRefunds.question${n}`) }}
                    <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                  </button>
                </h6>
                <div :id="`collapseLicenses${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingLicenses${n}`" data-bs-parent="#accordionFaq3">
                  <div class="accordion-body text-sm opacity-8">{{ $t(`faqRefunds.answer${n}`) }}</div>
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
export default {
  setup() {
    const faqSecurityCount = 2;
    const faqOrders = 3;
    const faqRefunds = 1;
    return { faqSecurityCount, faqOrders, faqRefunds };
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
  background-image: url('../assets/img/bg3.webp');
  object-fit: cover;
}
</style>