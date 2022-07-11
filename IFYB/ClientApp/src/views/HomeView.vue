<template>
  <header-component></header-component>
  <features-component></features-component>
  <name-card></name-card>
  <about-us></about-us>
  <work-with-us></work-with-us>
  <pricing-component></pricing-component>
  <handle-components></handle-components>
  <footer-component></footer-component>
</template>

<script>
import AboutUs from '../components/homeComponents/AboutUs.vue';
import FeaturesComponent from '../components/homeComponents/FeaturesComponent.vue';
import FooterComponent from '../components/homeComponents/FooterComponent.vue';
import HandleComponents from '../components/homeComponents/HandleComponents.vue';
import HeaderComponent from '../components/homeComponents/HeaderComponent.vue';
import NameCard from '../components/homeComponents/NameCard.vue';
import PricingComponent from '../components/homeComponents/PricingComponent.vue';
import WorkWithUs from '../components/homeComponents/WorkWithUs.vue';
import { useServerError } from "../store";

export default {
  name: 'HomeView',
  components: { FooterComponent, HandleComponents, PricingComponent, WorkWithUs, AboutUs, NameCard, FeaturesComponent, HeaderComponent },
  setup() {
    const { setServerError } = useServerError();
    setServerError(null);

    return {};
  }
}
</script>