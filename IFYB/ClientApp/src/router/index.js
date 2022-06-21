import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import OrderView from '../views/OrderView.vue';
import FAQ from '../views/FAQ.vue';
import AdminView from '../views/AdminView.vue';
import ContactForm from '../views/ContactForm.vue';
import OrdersView from '../views/OrdersView.vue';
import AuthenticationView from '../views/AuthenticationView.vue';
import AdminAuthenticationView from '../views/AdminAuthenticationView.vue';
import { useUserAuthentication, useAdminAuthentication } from '@/store';

const userAuth = useUserAuthentication();
const adminAuth = useAdminAuthentication();

function userAuthenticationGuard(to) {
  if (userAuth.isLoggedIn.value)
    return true;
  userAuth.requestedPage.value = to;
  return { path: '/authentication' }
}

function adminAuthenticationGuard(to) {
  if (adminAuth.isLoggedIn.value)
    return true;
  adminAuth.requestedPage.value = to;
  return { path: '/admin-authentication' }
}

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/order',
    name: 'order',
    component: OrderView,
    beforeEnter: userAuthenticationGuard
  },
  {
    path: '/faq',
    name: 'faq',
    component: FAQ
  },
  {
    path: '/admin',
    name: 'admin',
    component: AdminView,
    beforeEnter: adminAuthenticationGuard
  },
  {
    path: '/contact-form',
    name: 'contact-form',
    component: ContactForm
  },
  {
    path: '/my-orders',
    name: 'my-orders',
    component: OrdersView,
    beforeEnter: userAuthenticationGuard
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
