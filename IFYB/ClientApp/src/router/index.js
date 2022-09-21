import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';

const PrivacyPolicy = () => import(/* webpackChunkName: "order" */ '../views/PrivacyPolicy.vue');
const TermsAndConditions = () => import(/* webpackChunkName: "order" */ '../views/TermsAndConditions.vue');
const NewOrderView = () => import(/* webpackChunkName: "order" */ '../views/NewOrderView.vue');
const FAQ = () => import(/* webpackChunkName: "order" */ '../views/FAQ.vue');
const CreditsView = () => import(/* webpackChunkName: "order" */ '../views/CreditsView.vue');
const ContactForm = () => import(/* webpackChunkName: "order" */ '../views/ContactForm.vue');
const OrdersView = () => import(/* webpackChunkName: "order" */ '../views/OrdersView.vue');
const OrderView = () => import(/* webpackChunkName: "order" */ '../views/OrderView.vue');
const ClientsView = () => import(/* webpackChunkName: "order" */ '../views/ClientsView.vue');
const AuthenticationView = () => import(/* webpackChunkName: "order" */ '../views/AuthenticationView.vue');
const CheckoutView = () => import(/* webpackChunkName: "order" */ '../views/CheckoutView.vue');
const CheckoutSuccessView = () => import(/* webpackChunkName: "order" */ '../views/CheckoutSuccessView.vue');
const CheckoutFailureView = () => import(/* webpackChunkName: "order" */ '../views/CheckoutFailureView.vue');
const CheckoutPaidView = () => import(/* webpackChunkName: "order" */ '../views/CheckoutPaidView.vue');

const AdminView  = () => import(/* webpackChunkName: "admin" */  '@/views/AdminView.vue');
const AdminOrderView = () => import(/* webpackChunkName: "admin" */  '@/views/AdminOrderView.vue');
const AdminAuthenticationView = () => import(/* webpackChunkName: "admin" */  '@/views/AdminAuthenticationView.vue');

import { useUserAuthentication, useAdminAuthentication, usePayment, useServerError, useScripts } from '@/store';

const userAuth = useUserAuthentication();
const adminAuth = useAdminAuthentication();
const payment = usePayment();
const { loadTinymce, loadBootstrap } = useScripts();
const { resetServerError } = useServerError();

function paymentGuard(to) {
  if (payment.isPaymentInProgress(to.params.token)) {
    payment.clearPaymentToken();
    return true
  }
  return { path: '/' }
}

function userAuthenticationGuard(to) {
  if (userAuth.isLoggedIn.value && userAuth.name.value) {
    return true
  } else {
    userAuth.requestedPage.value = to;
    return { path: '/authentication' }
  }
}

function adminAuthenticationGuard(to) {
  if (adminAuth.isLoggedIn.value) {
    return true
  } else {
    adminAuth.requestedPage.value = to;
    return { path: '/admin-authentication' }
  }
}

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta: { title: 'Home' },
  },
  {
    path: '/privacy-policy',
    name: 'privacy-policy',
    component: PrivacyPolicy,
    meta: { title: 'Privacy Policy' },
  },
  {
    path: '/terms-and-conditions',
    name: 'terms-and-conditions',
    component: TermsAndConditions,
    meta: { title: 'Terms And Conditions' },
  },
  {
    path: '/new-order',
    name: 'new-order',
    component: NewOrderView,
    meta: { title: 'New Order' },
    beforeEnter: [userAuthenticationGuard, loadTinymce]
  },
  {
    path: '/faq',
    name: 'faq',
    component: FAQ,
    meta: { title: 'FAQ' },
    beforeEnter: loadBootstrap
  },
  {
    path: '/credits',
    name: 'credits',
    component: CreditsView,
    meta: { title: 'Credits' },
  },
  {
    path: '/admin',
    name: 'admin',
    component: AdminView,
    meta: { title: 'Admin' },
    beforeEnter: adminAuthenticationGuard
  },
  {
    path: '/admin/:number',
    name: 'order',
    component: AdminOrderView,
    beforeEnter: adminAuthenticationGuard
  },
  {
    path: '/clients',
    name: 'clients',
    component: ClientsView,
    meta: { title: 'Clients' },
    beforeEnter: adminAuthenticationGuard
  },
  {
    path: '/contact-form',
    name: 'contact-form',
    component: ContactForm,
    meta: { title: 'Contact Form' },
  },
  {
    path: '/my-orders',
    name: 'my-orders',
    component: OrdersView,
    meta: { title: 'My Orders' },
    beforeEnter: userAuthenticationGuard
  },
  {
    path: '/my-orders/:number',
    name: 'my-order',
    component: OrderView,
    beforeEnter: userAuthenticationGuard
  },
  {
    path: '/authentication',
    name: 'authentication',
    component: AuthenticationView,
    meta: { title: 'Authentication' },
  },
  {
    path: '/admin-authentication',
    name: 'admin-authentication',
    component: AdminAuthenticationView,
    meta: { title: 'Admin Authentication' },
  },
  {
    path: '/checkout/:token',
    name: 'checkout',
    component: CheckoutView,
    meta: { title: 'Checkout' },
  },
  {
    path: '/checkout-success/:token',
    name: 'checkout-success',
    component: CheckoutSuccessView,
    meta: { title: 'Checkout Success' },
    beforeEnter: paymentGuard
  },
  {
    path: '/checkout-failed/:token',
    name: 'checkout-failed',
    component: CheckoutFailureView,
    meta: { title: 'Checkout Failure' },
    beforeEnter: paymentGuard
  },
  {
    path: '/checkout-paid/:token',
    name: 'checkout-paid',
    component: CheckoutPaidView,
    meta: { title: 'Checkout Paid' },
    beforeEnter: paymentGuard
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})


router.beforeEach((to, from, next) => {
  resetServerError();
  if(to.params.number)
    document.title = `I Fix Your Bug - #${to.params.number}`;
  else
    document.title = to.meta.title ? `I Fix Your Bug - ${to.meta.title}` : 'I Fix Your Bug';
  next();
})

export default router
