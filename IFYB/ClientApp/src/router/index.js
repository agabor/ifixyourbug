import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import OrderView from '../views/OrderView.vue';
import FAQ from '../views/FAQ.vue';
import AdminLogin from '../views/AdminLogin.vue';

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
    component: AdminLogin
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
