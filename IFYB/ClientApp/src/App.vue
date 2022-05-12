<template>
  <nav class="navbar navbar-expand-lg bg-white  top-0 z-index-3 shadow position-fixed py-2 start-0 end-0">
    <div class="container px-0">
      <a class="navbar-brand font-weight-bolder ms-sm-3" href=" https://demos.creative-tim.com/now-ui-design-system-pro/presentation.html " rel="tooltip" title="Designed and Coded by Creative Tim" data-placement="bottom" target="_blank">
        Now UI PRO
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
          <li class="nav-item mx-2">
            <router-link to="/" class="dropdown-item border-radius-md ps-3 d-flex align-items-center justify-content-between mb-1">Home</router-link>
          </li>
          <li class="nav-item mx-2">
            <router-link to="/about" class="dropdown-item border-radius-md ps-3 d-flex align-items-center justify-content-between mb-1">About</router-link>
          </li>
        </ul>
        <ul class="navbar-nav d-lg-block d-none">
          <li class="nav-item">
            <a href="#pricing-soft-ui" class="btn btn-sm  bg-gradient-primary  btn-round mb-0 me-1" onclick="smoothToPricing('pricing-soft-ui')">Buy Now</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
  <router-view/>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>
