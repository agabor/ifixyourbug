<template>
  <div class="carousel-item active">
    <div class="mx-md-6 mx-3" :class="width ? 'mt-6' : ''">
      <div class="row">
        <div :class="width ? `${width} mx-auto my-4` : 'col-xxl-5 col-lg-6 col-sm-8 col-12 my-4 mx-auto'">
          <div class="card">
            <div class="card-body px-lg-5 py-lg-5 text-center">
              <div class="info mb-4" v-if="icon">
                <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                  <slot name="icon"></slot>
                </div>
              </div>
              <h2 v-if="title">{{ title }}</h2>
              <p class="mb-4" v-if="subTitle"><span v-html="subTitle"></span></p>
              <slot name="content"></slot>
              <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
                {{ error }}
              </div>
              <div class="text-center" v-if="buttonText">
                <button type="button" class="btn bg-gradient-primary my-4 mx-1" @click="$emit('onClickBtn')">{{ buttonText }}</button>
                <button v-if="cancelable" type="button" class="btn btn-outline-secondary my-4 mx-1" @click="$emit('cancel')">Cancel</button>
              </div>
              <div class="progress">
                <div class="progress-bar bg-primary" role="progressbar" :style="`width: ${progress}%`" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
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
  name: 'CarouselItem',
  props: {
    width: String,
    icon: Boolean,
    cancelable: Boolean,
    subTitle: String,
    title: String,
    buttonText: String,
    error: String,
    progress: Number
  },
  emits: [ 'onClickBtn', 'cancel' ],
}
</script>

<style>
  .progress {
    background-color: transparent;
  }
</style>