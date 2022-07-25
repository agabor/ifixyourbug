import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import PrivacyPolicy from '../views/PrivacyPolicy.vue';
import TermsAndConditions from '../views/TermsAndConditions.vue';
import OtherServices from '../views/OtherServices.vue';
import NewOrderView from '../views/NewOrderView.vue';
import FAQ from '../views/FAQ.vue';
import DesignView from '../views/DesignView.vue';
import AdminView from '../views/AdminView.vue';
import AdminOrderView from '../views/AdminOrderView.vue';
import ContactForm from '../views/ContactForm.vue';
import OrdersView from '../views/OrdersView.vue';
import OrderView from '../views/OrderView.vue';
import ClientsView from '../views/ClientsView.vue';
import AuthenticationView from '../views/AuthenticationView.vue';
import AdminAuthenticationView from '../views/AdminAuthenticationView.vue';
import CheckoutView from '../views/CheckoutView.vue';
import CheckoutSuccessView from '../views/CheckoutSuccessView.vue';
import CheckoutFailureView from '../views/CheckoutFailureView.vue';
import CheckoutPaidView from '../views/CheckoutPaidView.vue';
import { useUserAuthentication, useAdminAuthentication, usePayment, useServerError } from '@/store';

const userAuth = useUserAuthentication();
const adminAuth = useAdminAuthentication();
const payment = usePayment();
const { resetServerError } = useServerError();

function paymentGuard(to) {
  if (payment.isPaymentInProgress(to.params.token)) {
    payment.clearPaymentToken();
    return true
  }
  return { path: '/' }
}

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
    path: '/other-services',
    name: 'other-services',
    component: OtherServices,
    meta: { title: 'Services' },
  },
  {
    path: '/new-order',
    name: 'new-order',
    component: NewOrderView,
    meta: { title: 'New Order' },
    beforeEnter: userAuthenticationGuard
  },
  {
    path: '/faq',
    name: 'faq',
    component: FAQ,
    meta: { title: 'FAQ' },
  },
  {
    path: '/design-view',
    name: 'design-view',
    component: DesignView,
    meta: { title: 'Design' },
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
    if(to.params[0])
      document.title = `I Fix Your Bug - ${to.params[0]}`;
    else
      document.title = to.meta.title ? `I Fix Your Bug - ${to.meta.title}` : 'I Fix Your Bug';
  next();
})

export default router
