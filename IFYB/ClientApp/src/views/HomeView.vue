<template>
  <header-component></header-component>
  <features-component></features-component>
  <name-card v-if="loaded"></name-card>
  <pricing-component v-if="loaded"></pricing-component>
  <work-with-us v-if="loaded"></work-with-us>
  <workflow-components v-if="loaded"></workflow-components>
  <footer-component v-if="loaded"></footer-component>
</template>

<script>
  import { ref } from 'vue';
import FeaturesComponent from '../components/homeComponents/FeaturesComponent.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import WorkflowComponents from '../components/homeComponents/WorkflowComponents.vue';
import HeaderComponent from '../components/homeComponents/HeaderComponent.vue';
import NameCard from '../components/homeComponents/NameCard.vue';
import PricingComponent from '../components/homeComponents/PricingComponent.vue';
import WorkWithUs from '../components/homeComponents/WorkWithUs.vue';

export default {
  name: 'HomeView',
  components: { FooterComponent, WorkflowComponents, PricingComponent, WorkWithUs, NameCard, FeaturesComponent, HeaderComponent },
  setup() {
    const loaded = ref(false);
    window.onload = function() {
      loaded.value = true;
    }
    return { loaded }
  }
}
</script>