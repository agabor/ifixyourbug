<template>
  <header>
    <div class="page-header min-vh-85">
      <div>
        <img class="position-absolute fixed-top ms-auto w-50 h-100 z-index-0 d-none d-sm-none d-md-block border-radius-xl border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0" src="../assets/img/pricing3.jpg" alt="image">
      </div>
      <div class="container">
        <div class="row">
          <div class="col-lg-7 d-flex">
            <div class="card card-body text-md-start text-center px-sm-5 shadow mt-sm-5 py-sm-5">
              <h2 class="mb-0">Now UI Design System.</h2>
              <h2 class="text-dark mb-4">Choose the best</h2>
              <p class="lead pe-md-5 me-md-5">
                Find the story of Creative Tim&#39;s most complex design system and
                all the work that make this design available.
              </p>
              <div class="buttons">
                <button type="button" class="btn btn-rounded bg-gradient-primary mt-4" @click="$router.push('/order')">Order</button>
                <button type="button" class="btn btn-rounded btn-outline-secondary mt-4 ms-2" @click="$router.push('/faq')">FAQ</button>
                <button type="button" class="btn btn-rounded btn-outline-secondary mt-4 ms-2" @click="$router.push('/contact-form')">Contact</button>
              </div>
              <div class="row mt-2">
                <div class="col-md-3 col-6 p-0">
                  <img class="w-100" src="../assets/img/logos/gray-logos/logo-pinterest.svg" alt="logo">
                </div>
                <div class="col-md-3 col-6 p-0">
                  <img class="w-100" src="../assets/img/logos/gray-logos/logo-netflix.svg" alt="logo">
                </div>
                <div class="col-md-3 col-6 p-0">
                  <img class="w-100" src="../assets/img/logos/gray-logos/logo-coinbase.svg" alt="logo">
                </div>
                <div class="col-md-3 col-6 p-0">
                  <img class="w-100" src="../assets/img/logos/gray-logos/logo-nasa.svg" alt="logo">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
  <!-- -------- END HEADER 8 w/ card over right bg image ------- -->
  <!-- -------- START Features w/ 4 infos - just colored icons -------- -->
  <section class="py-md-7 pb-5">
    <div class="container">
      <div class="row text-center">
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-box-2 h3 text-primary"></i>
            <h5 class="mt-2">Components</h5>
            <p>Choose the best design system for your next product.</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-album-2 h3 text-primary"></i>
            <h5 class="mt-2">Design</h5>
            <p>Get the latest design ideas and turn it into reality.</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-atom h3 text-primary"></i>
            <h5 class="mt-2">Less Code</h5>
            <p>Make your code easier to maintain using variables.</p>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="info">
            <i class="ni ni-collection h3 text-primary"></i>
            <h5 class="mt-2">Fully Responsive</h5>
            <p>This design system is fully supported on any device.</p>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- -------- END Features w/ 4 infos - just colored icons -------- -->
  <section class="pb-md-7 pb-5">
    <div class="author-header min-vh-25">
      <span class="mask bg-gradient-primary opacity-8"></span>
    </div>
    <div class="container">
      <div class="row bg-white px-4 py-5 mt-n7 position-relative shadow border-radius-lg">
        <div class="col-lg-3 col-md-5 position-relative my-auto">
          <img class="img rounded-circle max-width-200 w-100 position-relative z-index-2" src="../assets/img/james.jpg" alt="bruce">
        </div>
        <div class="col-md-9 z-index-2 position-relative px-md-2 px-sm-5 mt-sm-0 mt-4">
          <div class="d-flex justify-content-between align-items-center mb-2">
            <h4 class="mb-0">Michael Roven</h4>
            <div class="d-block">
              <button type="button" class="btn btn-sm btn-outline-primary text-nowrap mb-0">Follow</button>
            </div>
          </div>
          <div class="row mb-4">
            <div class="col-auto">
              <span class="h6">323</span>
              <span>Posts</span>
            </div>
            <div class="col-auto">
              <span class="h6">3.5k</span>
              <span>Followers</span>
            </div>
            <div class="col-auto">
              <span class="h6">260</span>
              <span>Following</span>
            </div>
          </div>
          <p class="text-lg mb-0">
            Decisions: If you can’t decide, the answer is no.
            If two equally difficult paths, choose the one more
            painful in the short term (pain avoidance is creating
            an illusion of equality). Choose the path that leaves
            you more equanimous. <br><a href="javascript:;" class="text-primary text-decoration-underline-hover">More about me</a>
          </p>
        </div>
      </div>
    </div>
  </section>
  <!-- START Blogs w/ 3 images & quote & text -->
  <section>
    <div class="container">
      <div class="row">
        <div class="col-md-6 ms-auto me-auto text-center">
          <h2>From nothing to something</h2>
          <p>
            The highest status people in human history are those that asked for nothing and gave everything
          </p>
        </div>
      </div>
      <div class="row mt-5">
        <div class="col-md-6 position-relative">
          <div class="position-relative ms-md-5 me-md-n5">
            <img class="image-left shadow rounded-3 img-fluid position-relative top-0 end-0 bg-cover" src="../assets/img/bg28.jpg" alt="image">
          </div>
          <p class="blockquote border border-primary rounded w-50 p-3 text-sm text-primary float-md-end mt-4 me-md-n2 mx-auto">"Over the span of the satellite record, Arctic sea ice has been declining significantly, while sea ice in the Antarctichas increased very slightly"
            <br>
            <br>
            <small>-NOAA</small>
          </p>
          <!-- Second image on the left side of the article -->
          <div class="position-absolute bottom-0 mb-5 ms-n2 me-3 start-0 end-2 d-md-block d-none">
            <img class="image-container shadow rounded-3 img-fluid position-relative bg-cover" src="../assets/img/bg40.jpg" alt="image">
          </div>
        </div>
        <div class="col-md-5">
          <!-- First image on the right side, above the article -->
          <div class="position-relative ms-n4 mb-5 mt-8 d-md-block d-none">
            <img class="image-right shadow rounded-3 img-fluid position-relative bg-cover" src="../assets/img/bg29.jpg" alt="image">
          </div>
          <h3 class="mb-4">So what does the new record for the lowest level of winter ice actually mean</h3>
          <p>The Arctic Ocean freezes every winter and much of the sea-ice then thaws every summer, and that process will continue whatever happens with climate change. Even if the Arctic continues to be one of the fastest-warming regions of the world, it will always be plunged into bitterly cold polar dark every winter. And year-by-year, for all kinds of natural reasons, there’s huge variety of the state of the ice.
          </p>
          <p>
            For a start, it does not automatically follow that a record amount of ice will melt this summer. More important for determining the size of the annual thaw is the state of the weather as the midnight sun approaches and temperatures rise. But over the more than 30 years of satellite records, scientists have observed a clear pattern of decline, decade-by-decade.
          </p>
          <p>The Arctic Ocean freezes every winter and much of the sea-ice then thaws every summer, and that process will continue whatever happens with climate change. Even if the Arctic continues to be one of the fastest-warming regions of the world, it will always be plunged into bitterly cold polar dark every winter. And year-by-year, for all kinds of natural reasons, there’s huge variety of the state of the ice.
          </p>
        </div>
      </div>
    </div>
  </section>
  <!-- END Blogs w/ 3 images & quote & text -->
  <section class="bg-dark position-relative overflow-hidden mt-7">
    <div class="container py-lg-8 py-7">
      <div class="row">
        <div class="col-lg-6 d-flex justify-content-center flex-column">
          <div id="carouselExampleIndicator" class="carousel slide py-7" data-bs-ride="carousel">
            <div class="carousel-indicators justify-content-start ms-0">
              <a href="javascript:;">
                <img src="../assets/img/emily.jpg" alt="..." class="avatar avatar-sm avatar-scale-up shadow border-0 active rounded-circle" data-bs-target="#carouselExampleIndicator" data-bs-slide-to="0">
                <span class="text-white mx-2">&#124;</span>
              </a>
              <a href="javascript:;">
                <img src="../assets/img/emilyz.jpg" alt="..." class="avatar avatar-sm avatar-scale-up shadow border-0 rounded-circle" data-bs-target="#carouselExampleIndicator" data-bs-slide-to="1">
                <span class="text-white mx-2">&#124;</span>
              </a>
              <a href="javascript:;">
                <img src="../assets/img/james.jpg" alt="..." class="avatar avatar-sm avatar-scale-up shadow border-0 rounded-circle" data-bs-target="#carouselExampleIndicator" data-bs-slide-to="2">
              </a>
            </div>
            <div class="carousel-inner">
              <h2 class="text-white mb-0">Work with us</h2>
              <p class="text-white opacity-8 mb-1">Whatever your qualification is - we got you!</p>
              <hr class="text-white horizontal opacity-6 mb-4 mt-2 w-25 text-start">
              <div class="carousel-item active">
                <h6 class="text-white opacity-8 pe-5">"Take up one idea. Make that one idea your life - think of it, dream of it, live on that idea.
                  Let the brain, muscles, nerves, every part of your body, be full of that idea, and just leave every other idea alone.
                  This is the way to success. A single rose can be my garden... a single friend, my world."</h6>
                <div class="author mt-4">
                  <div class="name">
                    <span class="text-white">Ludwig Siato</span>
                    <div class="stats">
                      <small class="text-white opacity-6">Project manager, Apple</small>
                    </div>
                  </div>
                </div>
              </div>
              <div class="carousel-item">
                <h6 class="text-white opacity-8 pe-5">"Take up one idea. Make that one idea your life - think of it, dream of it, live on that idea.
                  Let the brain, muscles, nerves, every part of your body, be full of that idea, and just leave every other idea alone.
                  This is the way to success. A single rose can be my garden... a single friend, my world."</h6>
                <div class="author mt-4">
                  <div class="name">
                    <span class="text-white">John Down</span>
                    <div class="stats">
                      <small class="text-white opacity-6">CFO, Google</small>
                    </div>
                  </div>
                </div>
              </div>
              <div class="carousel-item">
                <h6 class="text-white opacity-8 pe-5">"Take up one idea. Make that one idea your life - think of it, dream of it, live on that idea.
                  Let the brain, muscles, nerves, every part of your body, be full of that idea, and just leave every other idea alone.
                  This is the way to success. A single rose can be my garden... a single friend, my world."</h6>
                <div class="author mt-4">
                  <div class="name">
                    <span class="text-white">Mark Jojansen</span>
                    <div class="stats">
                      <small class="text-white opacity-6">Fullstack Developer, Atlassian</small>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-6 justify-content-center flex-column d-sm-none d-md-none d-lg-flex d-xl-flex">
          <div class="row justify-content-center d-none d-md-flex">
            <div class="col-3 mb-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 mt-n4 ms-8 fadeIn2 fadeInBottom">
                <img src="../assets/img/logos/small-logos/logo-asana.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-4 mb-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-8 mt-n6 fadeIn1 fadeInBottom">
                <img src="../assets/img/logos/small-logos/logo-slack.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-4 mb-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-6 mt-2 fadeIn3 fadeInBottom">
                <img src="../assets/img/logos/small-logos/logo-google-drive.svg" alt="Logo Image">
              </div>
            </div>
          </div>
          <div class="row justify-content-end d-none d-md-flex">
            <div class="col-4 my-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-4 fadeIn1 fadeInBottom">
                <img class="avatar-img" src="../assets/img/logos/small-logos/logo-shopify.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-3 my-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 me-auto fadeIn1 fadeInBottom">
                <img class="avatar-img" src="../assets/img/logos/small-logos/logo-apple.svg" alt="Logo Image">
              </div>
            </div>
            <div class="col-3 my-4">
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 fadeIn3 fadeInBottom ms-3 mt-3">
                <img class="avatar-img" src="../assets/img/logos/small-logos/logo-invision.svg" alt="Logo Image">
              </div>
            </div>
          </div>
          <div class="row d-none d-md-flex">
            <div class="col-6">
              <!-- Logo -->
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 ms-auto mt-5 fadeIn2 fadeInBottom">
                <img class="avatar-img" src="../assets/img/logos/small-logos/logo-atlassian.svg" alt="Logo Image">
              </div>
              <!-- End Logo -->
            </div>
            <div class="col-6 mt-6">
              <!-- Logo -->
              <div class="d-block bg-white avatar avatar-lg rounded-circle p-3 mx-auto mt-n3 fadeIn3 fadeInBottom">
                <img class="avatar-img" src="../assets/img/logos/small-logos/logo-weave.svg" alt="Logo Image">
              </div>
              <!-- End Logo -->
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <section class="py-lg-7">
    <div class="container">
      <div class="row">
        <div class="col-md-6 mx-auto text-center mb-5">
          <h2>Best no-tricks pricing</h2>
          <p class="lead">If you're not satisfied, contact us within the first 30 days and we'll send you a full refund.</p>
        </div>
      </div>
      <div class="row">
        <div class="card">
          <div class="row">
            <div class="col-lg-8">
              <div class="card-body">
                <h3 class="text-info">Lifetime Membership</h3>
                <p>You have Free Unlimited Updates and Premium Support on each package. You also have 30 days to request a refund.</p>
                <div class="row mt-5 mb-2">
                  <div class="col-lg-3 col-12">
                    <h6 class="text-dark tet-uppercase">What's included</h6>
                  </div>
                  <div class="col-6">
                    <hr class="horizontal dark">
                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-12 ps-0">
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">Private code access</span>
                      </div>
                    </div>
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">Free entry to all repositories</span>
                      </div>
                    </div>
                  </div>
                  <div class="col-lg-6 col-md-6 col-12 ps-sm-2 ps-0">
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">Pro member accounts</span>
                      </div>
                    </div>
                    <div class="d-flex align-items-center p-2">
                      <i class="fas fa-check text-dark"></i>
                      <div>
                        <span class="ps-2">Support team full assist</span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-lg-4 my-auto">
              <div class="card-body text-center">
                <h6 class="mt-sm-4 mt-0 mb-0">Pay once, own it forever</h6>
                <h1 class="mt-0">
                  <small>$</small>399
                </h1>
                <button type="button" class="btn bg-gradient-info btn-lg mt-2">Get Access</button>
                <p class="text-sm">Get a free sample (20MB)</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <section>
    <div class="container">
      <div class="row">
        <div class="col-md-5 ms-auto me-auto text-center">
          <div class="p-3 info-hover-warning">
            <div class="icon icon-shape bg-gradient-warning icon-shape-circle text-primary">
              <i class="ni ni-chart-bar-32  opacity-10"></i>
            </div>
          </div>
          <h3 class="mb-0 mt-4">Beware The Components</h3>
          <h3>How To Handle Them</h3>
          <p>We’re constantly trying to express ourselves and actualize our dreams. Don't stop.</p>
        </div>
      </div>
      <div class="row mt-5">
        <div class="col-md-4 ms-auto my-auto">
          <div class="cursor-pointer">
            <div class="card card-background">
              <img class="full-background" src="../assets/img/bg26.jpg" alt="image">
              <div class="card-body pt-7 pb-6 text-center">
                <i class="ni ni-books mb-3 h3 text-white"></i>
                <a href="javascript:;" class="h4 d-block text-white up mb-0 text-decoration-underline-hover">Search and Discover!</a>
                <p class="lead mt-2 opacity-9">Website visitors</p>
                <button type="button" class="btn btn-white shadow-none mt-3">Get Started</button>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-5 me-auto my-auto ms-md-5">
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>1. Listen to Social Conversations</h5>
              <p>
                Gain access to the demographics, psychographics, and location of unique people who are interested and talk about your brand.
              </p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>2. Performance Analyze</h5>
              <p>
                Unify data from Facebook, Instagram, Twitter, LinkedIn, and Youtube to gain rich insights from easy-to-use reports.
              </p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>3. Social Conversions</h5>
              <p>
                Track actions taken on your website that originated from social, and understand the impact on your bottom line.
              </p>
            </div>
          </div>
        </div>
      </div>
      <hr class="dark my-md-6 mt-2 mx-7">
      <div class="row">
        <div class="col-md-5 ms-auto my-auto">
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>1. Always In Sync</h5>
              <p>
                No matter where you are, Trello stays in sync across all of your devices.
              </p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>2. Work With Any Team</h5>
              <p>
                Whether it’s for work or even the next family vacation, Trello helps your team.
              </p>
            </div>
          </div>
          <div class="p-3 info-horizontal d-flex">
            <div>
              <h5>3. A Productivity Platform</h5>
              <p>
                Integrate the apps your team already uses directly into your workflow.
              </p>
            </div>
          </div>
        </div>
        <div class="col-md-4 me-auto my-auto ms-md-5">
          <div class="cursor-pointer">
            <div class="card card-background">
              <img class="full-background" src="../assets/img/bg24.jpg" alt="image">
              <div class="card-body pt-7 pb-6 text-center">
                <i class="ni ni-planet mb-3 h3 text-white"></i>
                <a class="h4 text-decoration-underline-hover d-block text-white up mb-0">Talk and Meet!</a>
                <p class="lead mt-2 opacity-9">Social activities</p>
                <button type="button" class="btn btn-white mt-3">Get Started</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- -------- START FOOTER 7 w/ DARK BACKGROUND AND 3 COLUMNS ------- -->
  <footer class="footer py-5 bg-dark position-relative overflow-hidden">
    <div class="container position-relative">
      <div class="row">
        <div class="col-lg-4 me-auto my-auto text-lg-start text-center">
          <h6 class="text-white font-weight-bolder text-uppercase mb-2">Now UI</h6>
          <p class="text-white">
            Copyright © 2022 Now UI by Creative Tim.
          </p>
        </div>
        <div class="col-lg-2 col-sm-4 col-12 ms-md-auto text-lg-start text-center mb-sm-0 mb-4">
          <div>
            <h6 class="text-white text-sm mb-1">Company</h6>
            <ul class="flex-column ms-lg-n3 nav">
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  About Us
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  Careers
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  Press
                </a>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-lg-2 col-sm-4 col-12 text-lg-start text-center mb-sm-0 mb-4">
          <div>
            <h6 class="text-white text-sm mb-1">Pages</h6>
            <ul class="flex-column ms-lg-n3 nav">
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  Login
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  Register
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  About
                </a>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-lg-2 col-sm-4 col-12 text-lg-start text-center mb-sm-0 mb-4">
          <div>
            <h6 class="text-white text-sm mb-1">Products</h6>
            <ul class="flex-column ms-lg-n3 nav">
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  Free
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  PRO
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-white opacity-8 text-sm pb-0" href="javascript:;" target="_blank">
                  Latest
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'HomeView',
}
</script>

<style scoped>
  .author-header {
    background-image: url('../assets/img/bg45.jpg');
    position: relative;
    background-size: cover;
    background-position: 50%;
  }
</style>