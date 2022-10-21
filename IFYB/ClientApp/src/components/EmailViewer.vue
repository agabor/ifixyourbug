<template>
  <div>
    <p v-if="emails.length === 0">{{ $t('contactView.haveNoEmails') }}</p>
    <div class="accordion-1">
      <div class="px-sm-4 px-2">
        <div class="row">
          <div class="col-12 mx-auto mt-4">
            <div class="accordion" id="accordionRental">
              <div class="accordion-item mb-3" v-for="(email, idx) in emails" :key="idx">
                <h5 class="accordion-header" :id="`headingEmail${idx}`">
                  <button class="accordion-button border-bottom collapsed pb-1 pt-0" type="button" data-bs-toggle="collapse" :data-bs-target="`#collapseEmail${idx}`" aria-expanded="false" :aria-controls="`collapseEmail${idx}`">
                    <span class="text-sm">{{ email.subject }}</span>
                    <i class="collapse-close ni ni-bold-down text-xs pt-1 position-absolute end-0 me-3" aria-hidden="true"></i>
                    <i class="collapse-open ni ni-bold-up text-xs pt-1 position-absolute end-0 me-3" aria-hidden="true"></i>
                  </button>
                </h5>
                <div :id="`collapseEmail${idx}`" class="accordion-collapse collapse" :aria-labelledby="`headingEmail${idx}`" data-bs-parent="#accordionRental" style="">
                  <div class="accordion-body text-start text-sm opacity-8 py-0">
                    <span v-html="email.text"></span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'EmailViewer',
  props: {
    emails: Array
  },
}
</script>

<style scoped>
.accordion {
  white-space: pre-wrap;
}
</style>