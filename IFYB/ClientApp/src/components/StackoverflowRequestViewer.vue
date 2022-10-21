<template>
  <div class="container mt-6">
    <div class="row">
      <div class="col-12 mx-auto my-4">
        <div class="card">
          <div class="card-body px-lg-5 py-lg-5 text-center">
            <div class="info mb-4">
              <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                <i :class="`ni ni-tag opacity-10 mt-2`"></i>
              </div>
            </div>
            <div class="d-flex flex-column align-items-center justify-content-center">
              <div class="align-items-center justify-content-center">
                <h2>{{ $t('requestViewer.title') }} #{{ request.number }}</h2>
                <p>{{ $filters.dateTimeFormat(request.dateTime) }}</p>
              </div>
              <div class="d-flex align-items-center justify-content-center flex-wrap">
                <div class="text-center mx-1">
                  <button v-if="request.solved" type="button" class="btn my-2 bg-gradient-primary btn-round" :disabled="request.solved">{{ $t('requestViewer.solved') }}</button>
                  <button v-else type="button" class="btn my-2 btn-outline-secondary" @click="$emit('trySolveRequest')">{{ $t('requestViewer.solve') }}</button>
                </div>
              </div>
            </div>
            <div class="text-start">
              <div class="row mb-3">
                <div class="col-md-6">
                  <label>{{ $t('requestViewer.name') }}</label>
                  <div class="py-2 px-4 rounded-pill text-white bg-gradient-primary">{{ request.name }}</div>
                </div>
                <div class="col-md-6 ps-md-2">
                  <label>{{ $t('requestViewer.email') }}</label>
                  <div class="d-flex justify-content-between align-items-center py-2 px-4 rounded-pill text-white bg-gradient-primary">
                    <span class="text-truncate">{{ request.email }}</span>
                    <i class="ni ni-single-copy-04 cursor-pointer" @click="copyToClipboard(request.email)"></i>
                  </div>
                </div>
              </div>
              <div class="row mb-3">
                <div class="col-12">
                  <label>{{ $t('requestViewer.url') }}</label>
                  <div class="d-flex justify-content-between align-items-center py-2 px-4 rounded-pill text-white bg-gradient-primary">
                    <a class="text-truncate text-white" :href="request.url" target="_blank">{{ request.url }}</a>
                    <i class="ni ni-single-copy-04 cursor-pointer" @click="copyToClipboard(request.url)"></i>
                  </div>
                </div>
              </div>
              <div class="row mb-3">
                <div class="col-md-12 mb-3">
                    <label>{{ $t('requestViewer.message') }}</label><br />
                    <span v-html="request.text"></span>
                </div>
              </div>
            </div>
            <div class="text-center">
              <button type="button" class="btn bg-gradient-primary my-4" @click="$emit('back')">{{ $t('requestViewer.back') }}</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'StackoverflowRequestViewer',
  props: {
    request: Object,
  },
  emits: ['back', 'trySolveRequest' ],
  setup() {
    function copyToClipboard(text) {
      navigator.clipboard.writeText(text);
    }
    return { copyToClipboard }
  }
}
</script>