<template>
    <carousel-item icon="fat-remove" :title="$t('authfailed.title')" :subTitle="$t('authfailed.subTitle')">
        <div class="text-center d-flex justify-content-center">
        <router-link to="/">
            <button type="button" class="btn btn-rounded bg-gradient-primary">{{$t('authfailed.buttonText')}}</button>
        </router-link>
        </div>
    </carousel-item>
</template>

<script>
import CarouselItem from './CarouselItem.vue';

export default {
  name: 'AuthenticationFailed',
  components: { CarouselItem }
}
</script>