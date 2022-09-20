<template>
  <carousel-item :icon="true" :title="$t('authfailed.title')" :subTitle="$t('authfailed.subTitle')">
    <template v-slot:icon>
      <i :class="`ni ni-fat-remove opacity-10 mt-2`"></i>
    </template>
    <template v-slot:content>
      <div class="text-center d-flex justify-content-center">
        <router-link to="/">
          <button type="button" class="btn btn-rounded bg-gradient-primary">{{$t('authfailed.buttonText')}}</button>
        </router-link>
      </div>
    </template>
  </carousel-item>
</template>

<script>
import CarouselItem from './CarouselItem.vue';

export default {
  name: 'AuthenticationFailed',
  components: { CarouselItem }
}
</script>