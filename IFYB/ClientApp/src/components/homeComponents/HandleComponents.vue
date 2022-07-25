<template>
  <section>
    <div class="container">
      <div class="row">
        <div class="col-md-5 ms-auto me-auto text-center">
          <div class="p-3 info-hover-warning">
            <div class="icon icon-shape bg-gradient-warning icon-shape-circle text-primary">
              <i class="ni ni-chart-bar-32  opacity-10"></i>
            </div>
          </div>
          <h3 class="mb-0 mt-4">{{ $t('handle.title1') }}</h3>
          <p>{{ $t('handle.subTitle') }}</p>
        </div>
      </div>
      <div class="row mt-5">
        <div class="col-md-4 ms-auto my-auto">
          <div class="cursor-pointer">
            <div class="card card-background">
              <img class="full-background fit-cover" src="../../assets/img/bg3.webp" alt="image">
              <div class="card-body pt-7 pb-6 text-center">
                <i class="ni ni-books mb-3 h3 text-white"></i>
                <a href="javascript:;" class="h4 d-block text-white up mb-0 text-decoration-underline-hover">{{ $t('handle.searchAndDiscover') }}</a>
                <p class="lead mt-2 opacity-9">{{ $t('handle.websiteVisitors') }}</p>
                <button type="button" class="btn btn-white shadow-none mt-3">{{ $t('handle.getStarted') }}</button>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-5 me-auto my-auto ms-md-5">
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('handle.workflowTitle1') }}</h5>
              <p>{{ $t('handle.workflowDescription1') }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('handle.workflowTitle2') }}</h5>
              <p>{{ $t('handle.workflowDescription2') }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('handle.workflowTitle3') }}</h5>
              <p>{{ $t('handle.workflowDescription3') }}</p>
            </div>
          </div>
        </div>
      </div>
      <hr class="dark my-md-6 mt-2 mx-7">
      <div class="row my-5">
        <div class="col-md-5 ms-auto my-auto">
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('handle.workflowTitle4') }}</h5>
              <p>{{ $t('handle.workflowDescription4') }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('handle.workflowTitle5') }}</h5>
              <p>{{ $t('handle.workflowDescription5') }}</p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>{{ $t('handle.workflowTitle6') }}</h5>
              <p>{{ $t('handle.workflowDescription6') }}</p>
            </div>
          </div>
        </div>
        <div class="col-md-4 me-auto my-auto ms-md-5">
          <div class="cursor-pointer">
            <div class="card card-background">
              <img class="full-background fit-cover" src="../../assets/img/bg4.webp" alt="image">
              <div class="card-body pt-7 pb-6 text-center">
                <i class="ni ni-planet mb-3 h3 text-white"></i>
                <a class="h4 text-decoration-underline-hover d-block text-white up mb-0">{{ $t('handle.talAndMeet') }}</a>
                <p class="lead mt-2 opacity-9">{{ $t('handle.socialActivities') }}</p>
                <button type="button" class="btn btn-white mt-3">{{ $t('handle.getStarted') }}</button>
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
  name: 'HandleComponents',
}
</script>