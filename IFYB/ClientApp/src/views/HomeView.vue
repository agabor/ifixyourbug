<template>
  <header-component></header-component>
  <features-component></features-component>
  <name-card v-if="loaded"></name-card>
  <work-with-us v-if="loaded"></work-with-us>
  <pricing-component v-if="loaded"></pricing-component>
  <workflow-components v-if="loaded"></workflow-components>
  <footer-component></footer-component>
</template>

<script>
import { useWindowLoad } from '@/store';
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
    const { loaded } = useWindowLoad();
    return { loaded }
  }
}
</script>