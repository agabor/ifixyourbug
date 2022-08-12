<template>
  <header-component></header-component>
  <features-component></features-component>
  <name-card></name-card>
  <pricing-component></pricing-component>
  <work-with-us></work-with-us>
  <workflow-components></workflow-components>
  <footer-component></footer-component>
</template>

<script>
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
}
</script>