<template>
  <nav class="navbar navbar-expand-lg bg-white top-0 z-index-3 shadow position-fixed py-2 start-0 end-0">
    <div class="container px-0">
      <a class="navbar-brand font-weight-bolder cursor-pointer ms-sm-3" @click="$router.push('/')" rel="tooltip" title="Designed and Coded by Creative Tim" data-placement="bottom" target="_blank">
        {{ $t('navigationBar.projectName') }}
      </a>
      <a href="https://www.creative-tim.com/product/now-ui-design-system-pro#pricingCard" class="btn btn-sm  bg-gradient-primary  btn-round mb-0 ms-auto d-lg-none d-block">Buy Now</a>
      <button class="navbar-toggler shadow-none ms-md-2" type="button" data-bs-toggle="collapse" data-bs-target="#navigation" aria-controls="navigation" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon mt-2">
          <span class="navbar-toggler-bar bar1"></span>
          <span class="navbar-toggler-bar bar2"></span>
          <span class="navbar-toggler-bar bar3"></span>
        </span>
      </button>
      <div class="collapse navbar-collapse w-100 pt-3 pb-2 py-lg-0" id="navigation">
        <ul class="navbar-nav navbar-nav-hover mx-auto">
          <li class="nav-item dropdown dropdown-hover mx-2">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" @click="$router.push('/')">
              {{ $t('navigationBar.home') }}
            </a>
          </li>
          <li class="nav-item dropdown dropdown-hover mx-2" v-if="activeClient === 'user'">
            <a role="button" class="nav-link ps-2 d-flex justify-content-between cursor-pointer align-items-center" @click="$router.push('/my-orders')">
              {{ $t('navigationBar.myOrders') }}
            </a>
          </li>
        </ul>
        <ul class="navbar-nav d-lg-block d-none">
          <li class="nav-item">
            <a class="btn btn-sm bg-gradient-primary btn-round mb-0 me-1" @click="$router.push('/authentication')" v-if="activeClient == null">{{ $t('navigationBar.login') }}</a>
            <a class="btn btn-sm bg-gradient-primary btn-round mb-0 me-1" @click="logOut" v-else>{{ $t('navigationBar.logout') }}</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import router from '../router';
import { useAuthentication } from "../store";

export default {
  name: 'NavigationBar',
  setup() {
    const { activeClient, setActiveClient } = useAuthentication();
    
    checkIsLogged();
    async function checkIsLogged() {
      let jwt = localStorage.getItem('jwt');
      if(jwt){
        let client = await checkUserJwt(jwt);
        if(!client) {
          client = await checkAdminJwt(jwt);
        }
      }
    }

    async function checkUserJwt(jwt){
      let response = await fetch('/api/authenticate/check-jwt', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt}`
        }
      })
      console.log('user', response);
      if(response.status == 200) {
        activeClient.value = 'user';
        return true;
      }
      return false;
    }

    async function checkAdminJwt(jwt){
      let response = await fetch('/api/authenticate/admin/check-jwt', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt}`
        }
      })
      console.log('admin', response);
      if(response.status == 200) {
        activeClient.value = 'admin';
        return true;
      }
      return false;
    }

    function logOut() {
      setActiveClient(null);
      localStorage.removeItem('jwt');
      localStorage.removeItem('adminId');
      router.push('/authentication');
    }

    return { activeClient, logOut }
  }
}
</script>
