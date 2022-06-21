<template>
  <div class="carousel-item">
    <div class="container" :class="width ? 'mt-6' : ''">
      <div class="row">
        <div :class="width ? `${width} mx-auto my-4` : 'col-lg-7 col-sm-12 my-4 mx-auto'">
          <div class="card">
            <div class="card-body px-lg-5 py-lg-5 text-center">
              <div class="info mb-4" v-if="icon">
                <div class="icon icon-shape icon-xl rounded-circle bg-gradient-primary shadow text-center py-3 mx-auto">
                  <i :class="`ni ni-${icon} opacity-10 mt-2`"></i>
                </div>
              </div>
              <h2 v-if="title">{{ title }}</h2>
              <p class="mb-4" v-if="subTitle">{{ subTitle }}</p>
              <slot></slot>
              <div class="alert alert-warning text-white font-weight-bold" role="alert" v-if="error">
                {{ error }}
              </div>
              <div class="text-center" v-if="buttonText">
                <button type="button" class="btn bg-gradient-primary my-4" @click="$emit('onClickBtn')">{{ buttonText }}</button>
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
    icon: String,
    subTitle: String,
    title: String,
    buttonText: String,
    error: String
  }
}
</script>
