<template>
  <div class="modal fade show d-block mt-5" id="confirmationModal" tabindex="-1" aria-labelledby="confirmationModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="confirmationModalLabel">{{ title }}</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          {{ description }}
        </div>
        <div class="modal-footer justify-content-between">
          <button type="button" class="btn bg-gradient-dark" data-bs-dismiss="modal" @click="$emit('cancel')">{{ $t('confirm.cancel') }}</button>
          <button type="button" class="btn bg-gradient-primary" @click="$emit('confirm')">{{ $t('confirm.confirm') }}</button>
        </div>
      </div>
    </div>
  </div>
</template>



<script>
export default {
  name: 'ConfirmationModal',
  props: {
    title: String,
    description: String
  },
  emits: ['confirm', 'cancel' ],
}
</script>