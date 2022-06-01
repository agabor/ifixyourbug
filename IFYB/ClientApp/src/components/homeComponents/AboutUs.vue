<template>
  <section>
    <div class="container">
      <div class="row">
        <div class="col-md-6 ms-auto me-auto text-center">
          <h2>{{ $t('blog.title') }}</h2>
          <p>{{ $t('blog.titleDes') }}</p>
        </div>
      </div>
      <div class="row mt-5">
        <div class="col-md-6 position-relative">
          <div class="position-relative ms-md-5 me-md-n5">
            <img class="image-left shadow rounded-3 img-fluid position-relative top-0 end-0 bg-cover" src="../../assets/img/bg28.jpg" alt="image">
          </div>
          <p class="blockquote border border-primary rounded w-50 p-3 text-sm text-primary float-md-end mt-4 me-md-n2 mx-auto">
            {{ $t('blog.quote') }}
            <br>
            <br>
            <small>{{ $t('blog.quoteAuthor') }}</small>
          </p>
          <div class="position-absolute bottom-0 mb-5 ms-n2 me-3 start-0 end-2 d-md-block d-none">
            <img class="image-container shadow rounded-3 img-fluid position-relative bg-cover" src="../../assets/img/bg40.jpg" alt="image">
          </div>
        </div>
        <div class="col-md-5">
          <div class="position-relative ms-n4 mb-5 mt-8 d-md-block d-none">
            <img class="image-right shadow rounded-3 img-fluid position-relative bg-cover" src="../../assets/img/bg29.jpg" alt="image">
          </div>
          <h3 class="mb-4">{{ $t('blog.subTitle') }}</h3>
          <p>{{ $t('blog.paragraph1') }}</p>
          <p>{{ $t('blog.paragraph2') }}</p>
          <p>{{ $t('blog.paragraph3') }}</p>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'AboutUs',
}
</script>