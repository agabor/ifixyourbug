import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import OrderView from '../views/OrderView.vue';
import FAQ from '../views/FAQ.vue';
import AdminView from '../views/AdminView.vue';
import ContactForm from '../views/ContactForm.vue';
import OrdersView from '../views/OrdersView.vue';
import AuthenticationView from '../views/AuthenticationView.vue';
import AdminAuthenticationView from '../views/AdminAuthenticationView.vue';

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/order',
    name: 'order',
    component: OrderView
  },
  {
    path: '/faq',
    name: 'faq',
    component: FAQ
  },
  {
    path: '/admin',
    name: 'admin',
    component: AdminView
  },
  {
    path: '/contact-form',
    name: 'contact-form',
    component: ContactForm
  },
  {
    path: '/my-orders',
    name: 'my-orders',
    component: OrdersView
  },
  {
    path: '/authentication',
    name: 'authentication',
    component: AuthenticationView
  },
  {
    path: '/admin-authentication',
    name: 'admin-authentication',
    component: AdminAuthenticationView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
