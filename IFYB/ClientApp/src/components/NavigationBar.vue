<template>
  <nav class="navbar navbar-expand-lg bg-white top-0 z-index-3 shadow position-fixed py-2 start-0 end-0 row px-lg-6 px-4">
    <div class="container px-0">
      <router-link to="/">
        <a class="navbar-brand font-weight-bolder cursor-pointer ms-sm-3" href="/" >
          {{ $t('navigationBar.projectName') }}
        </a>
      </router-link>
      <button class="navbar-toggler shadow-none ms-md-2" type="button" data-bs-toggle="collapse" data-bs-target="#navigation" aria-controls="navigation" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon mt-2">
          <span class="navbar-toggler-bar bar1"></span>
          <span class="navbar-toggler-bar bar2"></span>
          <span class="navbar-toggler-bar bar3"></span>
        </span>
      </button>
      <div class="collapse navbar-collapse w-100 pt-3 pb-2 py-lg-0" id="navigation">
        <ul class="navbar-nav navbar-nav-hover me-auto" v-if="isAdminLoggedIn">
          <li class="nav-item dropdown dropdown-hover mx-2">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" @click="$router.push('/admin')">
              {{ $t('navigationBar.orders') }}
            </a>
          </li>
          <li class="nav-item dropdown dropdown-hover mx-2">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" @click="$router.push('/clients')">
              {{ $t('navigationBar.clients') }}
            </a>
          </li>
          <li class="nav-item dropdown dropdown-hover mx-2">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" @click="$router.push('/stackoverflow-questions')">
              {{ $t('navigationBar.stackoverflow') }}
            </a>
          </li>          
        </ul>
        <ul class="navbar-nav navbar-nav-hover me-auto" v-else>
          <li class="nav-item dropdown dropdown-hover mx-2">
            <router-link to="/faq">
              <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" href="/faq">
                {{ $t('navigationBar.faq') }}
              </a>
            </router-link>
          </li>
          <li class="nav-item dropdown dropdown-hover mx-2">
            <router-link to="/contact-form">
              <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" href="/contact-form">
                {{ $t('navigationBar.contact') }}
              </a>
            </router-link>
          </li>
          <li class="nav-item dropdown dropdown-hover mx-2" v-if="isUserLoggedIn">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" @click="$router.push('/my-orders')">
              {{ $t('navigationBar.myOrders') }}
            </a>
          </li>
          <li class="nav-item dropdown dropdown-hover mx-2 d-lg-none d-block" v-if="isUserLoggedIn">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" @click="logout">
              {{ $t('navigationBar.logout') }}
            </a>
          </li>
          <li class="nav-item dropdown dropdown-hover mx-2 d-lg-none d-block" v-if="!isUserLoggedIn">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" href="/authentication">
              {{ $t('navigationBar.login') }}
            </a>
          </li>
        </ul>
        <ul class="navbar-nav d-block mx-4 text-center" v-if="isUserLoggedIn">
          <li class="nav-item small fw-bold">
            {{name}}
          </li>
          <li class="nav-item small fw-light">
            {{email}}
          </li>
        </ul>
        <ul class="navbar-nav d-lg-block d-none">
          <li class="nav-item">
            <router-link to="/authentication">
              <a class="btn btn-sm bg-gradient-primary btn-round mb-0 me-1 ms-auto" v-if="!isLoggedIn" href="/authentication">{{ $t('navigationBar.login') }}</a>
            </router-link>
            <a class="btn btn-sm bg-gradient-primary btn-round mb-0 me-1 ms-auto" @click="logout" v-if="isLoggedIn">{{ $t('navigationBar.logout') }}</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import router from '../router';
import { computed } from "@vue/reactivity";
import { useUserAuthentication } from "../store";
import { useAdminAuthentication } from "../store/admin";

export default {
  name: 'NavigationBar',
  setup() {
    const userAuth = useUserAuthentication();
    const adminAuth = useAdminAuthentication();
    const isLoggedIn = computed(() => userAuth.isLoggedIn.value || adminAuth.isLoggedIn.value);

    function logout() {
      userAuth.logout();
      adminAuth.logout();
      userAuth.requestedPage.value = null;
      router.push('/');
    }

    return { logout, isLoggedIn, 'isUserLoggedIn': userAuth.isLoggedIn, 'isAdminLoggedIn': adminAuth.isLoggedIn, 'name': userAuth.name, 'email': userAuth.email }
  }
}
</script>


<style scoped>
a:hover {
  color: #f05f3e;
}
</style>