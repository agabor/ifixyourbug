<template>
  <section>
    <div class="min-vh-100 d-flex flex-column">
      <div class="page-header justify-content-center">
        <span class="mask bg-gradient-dark opacity-4"></span>
        <div class="carousel-inner row mt-6">
          <div class="col-lg-10 col-12 mx-auto my-4">
            <div class="card">
              <div class="card-header bg-gradient-primary p-5 position-relative">
                <h3 class="text-white mb-0">{{ $t('faqCard.title') }}</h3>
                <p class="text-white opacity-8 mb-0">{{ $t('faqCard.lastModified', { date: 'July 22, 2022' }) }}</p>
              </div>
              <div class="card-body p-sm-5 pt-0">
                <h4 class="my-4 ps-3">{{ $t('faqOrders.title') }}</h4>
                <div class="accordion" id="accordionFaq2">
                  <div class="accordion-item" v-for="n in faqOrders" :key="n">
                    <h6 class="accordion-header" :id="`headingSettings${n}`">
                      <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseSettings${n}`" aria-expanded="false" :aria-controls="`collapseSettings${n}`">
                        {{ $t(`faqOrders.question${n}`) }}
                        <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                      </button>
                    </h6>
                    <div :id="`collapseSettings${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingSettings${n}`" data-bs-parent="#accordionFaq2">
                      <div class="accordion-body text-sm opacity-8">{{ $t(`faqOrders.answer${n}`, { workdays }) }}</div>
                    </div>
                  </div>
                </div>
                <h4 class="my-4 ps-3">{{ $t('faqRepos.title') }}</h4>
                <div class="accordion" id="accordionFaq3">
                  <div v-for="service in gitServices" :key="service.name">
                    <div class="accordion-item" v-if="service.domain !== 'bitbucket.com'">
                      <h6 class="accordion-header" id="headingSettings1">
                        <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseRepos${service.name}`" aria-expanded="false" :aria-controls="`collapseRepos${gitServices.name}`">
                          {{ $t('faqRepos.question1', {saas: service.name}) }}
                          <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                        </button>
                      </h6>
                      <div :id="`collapseRepos${service.name}`" class="accordion-collapse collapse" :aria-labelledby="`headingSettings${service.name}`" data-bs-parent="#accordionFaq3">
                        <div class="accordion-body text-sm opacity-8">
                          <span>{{$t('faqRepos.answer1')}}</span>
                          <a :href="service.user" target="_blank">here.</a>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="accordion-item" >
                    <h6 class="accordion-header" id="headingSettings2">
                      <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" data-bs-target="#collapseRepos2" aria-expanded="false" aria-controls="collapseRepos2">
                        {{ $t('faqRepos.question2') }}
                        <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                      </button>
                    </h6>
                    <div id="collapseRepos2" class="accordion-collapse collapse" aria-labelledby="headingSettings2" data-bs-parent="#accordionFaq3">
                      <div class="accordion-body text-sm opacity-8">
                        <span>{{$t(`faqRepos.answer2`)}}</span>
                        <ssh-key-preview></ssh-key-preview>
                      </div>
                    </div>
                  </div>
                </div>
                <h4 class="my-4 ps-3">{{ $t('faqRefunds.title') }}</h4>
                <div class="accordion" id="accordionFaq4">
                  <div class="accordion-item" v-for="n in faqRefunds" :key="n">
                    <h6 class="accordion-header" :id="`headingLicenses${n}`">
                      <button class="accordion-button border-bottom font-weight-bold text-start" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseLicenses${n}`" aria-expanded="false" :aria-controls="`collapseLicenses${n}`">
                        {{ $t(`faqRefunds.question${n}`) }}
                        <i class="collapse-rotate fas fa-chevron-down text-xs text-primary pt-1 position-absolute end-0 me-3"></i>
                      </button>
                    </h6>
                    <div :id="`collapseLicenses${n}`" class="accordion-collapse collapse" :aria-labelledby="`headingLicenses${n}`" data-bs-parent="#accordionFaq4">
                      <div class="accordion-body text-sm opacity-8">{{ $t(`faqRefunds.answer${n}`, { workdays }) }}</div>
                    </div>
                  </div>
                </div>
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
              </div>
            </div>
          </div>
        </div>
      </div>
      <footer-component></footer-component>
    </div>
  </section>
</template>

<script>
import SshKeyPreview from '@/components/SshKeyPreview.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import { useGitServices, useSettings } from '@/store';
import { onMounted } from 'vue'

export default {
  name: 'FaqView',
  components: { SshKeyPreview, FooterComponent },
  setup() {
    const faqSecurityCount = 2;
    const faqOrders = 3;
    const faqRefunds = 1;

    const { gitServices } = useGitServices();
    const { workdays } = useSettings();

    onMounted(() => {
      window.rdt('track', 'ViewContent');
    })

    return { faqSecurityCount, faqOrders, faqRefunds, gitServices, workdays };
  }
}
</script>