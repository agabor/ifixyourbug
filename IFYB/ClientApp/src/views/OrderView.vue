<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :class="{'active': page === 'email'}" icon="email-83" :title="$t('order.email')" :subTitle="$t('order.emailDes')" :buttonText="$t('order.submit')" :error="error" @onClickBtn="trySubmitEmail()">
          <div class="row mb-4">
            <input id="emailInput" class="form-control" :placeholder="$t('order.emailExample')" type="email" @keyup.enter="trySubmitEmail()" v-model="order.email">
          </div>
        </carousel-item>
        <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
        <carousel-item :class="{'active': page === 'name'}" icon="badge" :title="$t('order.name')" :subTitle="$t('order.nameDes')" :buttonText="$t('order.save')" :error="error" @onClickBtn="trySetName()">
          <div class="row mb-4">
            <input id="name-input" class="form-control" placeholder="Your Name" type="text" @keyup.enter="trySetName()" v-model="order.name">
          </div>
        </carousel-item>
        <carousel-item class="full-height" :class="{'active': page === 'data'}" width="col-lg-9 col-md-11" icon="spaceship" :title="$t('order.orderData')" :subTitle="$t('order.orderDataDes')" :buttonText="$t('order.submit')" :error="error" @onClickBtn="trysubmitOrder()">
          <form>
            <div class="row text-start">
              <div class="col-md-12 d-flex pe-2 mb-3">
                <select-framework v-model:modelvalue="order.framework" @update:modelvalue="changeFramework"></select-framework>
                <select-version v-model:modelvalue="order.version" :versions="order.framework == 0 ? vueVersions : order.framework == 1 ? aspVersions : undefined"></select-version>
              </div>
              <operating-system v-if="order.framework == 1" v-model:isSpecificOpSystem="order.isSpecificOpSystem" v-model:operatingSystem="order.os" v-model:version="order.opSystemVersion"></operating-system>
              <browser-type v-if="order.framework == 0" v-model:isSpecificBrowser="order.isSpecificBrowser" v-model:browser="order.browser" v-model:version="order.browserVersion"></browser-type>
              <online-app v-model:available="order.isAvailableApp" v-model:url="order.availableAppUrl"></online-app>
              <git-access-selector v-if="gitAccesses.length > 0" :accesses="gitAccesses" v-model:access="selectedAccess"></git-access-selector>
              <project-sharing v-model:accessMode="order.accessMode" v-model:url="order.repoUrl" :visible="selectedAccess.url == undefined"></project-sharing>
              <div class="col-md-12 pe-2 mb-3">
                <div class="form-group mb-0">
                  <label>{{ $t('order.projectDescription') }}*</label>
                  <text-editor id="project-description-input" v-model:modelValue="order.projectDescription" :placeholder="$t('order.projectDescription')"></text-editor>
                </div>
              </div>
              <div class="col-md-12 pe-2 mb-3">
                <div class="form-group mb-0">
                  <label>{{ $t('order.bugDescription') }}*</label>
                  <text-editor id="bug-description-input" v-model:modelValue="order.bugDescription" :placeholder="$t('order.bugDescription')"></text-editor>
                </div>
              </div>
              <third-party-tool v-model:isTool="order.isThirdPartyTool" v-model:tool="order.thirdPartyTool"></third-party-tool>
            </div>
          </form>
        </carousel-item>
        <carousel-item :class="{'active': page === 'success'}" icon="send" :title="$t('order.successfulOrder')" :subTitle="$t('order.successfulOrderDes')" :buttonText="$t('order.backToHome')" @onClickBtn="$router.push('/')"></carousel-item>
      </div>
    </div>
  </section>
</template>

<script>
import { ref, watch } from 'vue';
import { validEmail, required } from '../utils/Validate';
import TwoFa from '../components/2FA.vue';
import TextEditor from '../components/TextEditor.vue';
import CarouselItem from '../components/CarouselItem.vue';
import SelectFramework from '../components/orderComponents/SelectFramework.vue';
import SelectVersion from '../components/orderComponents/SelectVersion.vue';
import OperatingSystem from '../components/orderComponents/OperatingSystem.vue';
import BrowserType from '../components/orderComponents/BrowserType.vue';
import OnlineApp from '../components/orderComponents/OnlineApp.vue';
import GitAccessSelector from '../components/orderComponents/GitAccessSelector.vue';
import ProjectSharing from '../components/orderComponents/ProjectSharing.vue';
import ThirdPartyTool from '../components/orderComponents/ThirdPartyTool.vue';
import { useI18n } from "vue-i18n";

export default {
  name: 'OrderView',
  components: { TwoFa, TextEditor, CarouselItem, SelectFramework, SelectVersion, OperatingSystem, BrowserType, OnlineApp, GitAccessSelector, ProjectSharing, ThirdPartyTool },
  setup() {
    const { tm } = useI18n();
    const page = ref('email');
    const order = ref({});
    const error = ref(null);
    const auth = ref('');
    const aspVersions = ['3.1', '5.0', '6.0', '7.0'];
    const vueVersions = ['2.6', '3.0', '3.1', '3.2'];
    const gitAccesses = ref([]);
    const selectedAccess = ref({});
    let clientId;
    let jwt;
    order.value.isThirdPartyTool = false;
    order.value.isSpecificOpSystem = false;

    watch(selectedAccess, () => {
      if(selectedAccess.value) {
        order.value.accessMode = selectedAccess.value.accessMode;
        order.value.repoUrl = selectedAccess.value.url;
      } else {
        order.value.accessMode = undefined;
        order.value.repoUrl = undefined;
      }
    })

    setJwtIfActive();

    async function setJwtIfActive() {
      if(localStorage.getItem('jwt')) {
        let authResponse = await fetch('/authenticate/check-jwt', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        if(authResponse.status == 200) {
          jwt = localStorage.getItem('jwt');
          toNamePageOrToDataPage();
        }
      }
    }
    
    function trySubmitEmail() {
      let err = validEmail(order.value.email);
      if(err) {
        error.value = tm(err);
      } else {
        submitEmail();
      }
    }

    async function submitEmail() {
      const response = await fetch('/authenticate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'email': order.value.email})
      });
      clientId = (await response.json()).id;
      page.value = 'auth';
      error.value = null;
    }

    async function checkAuthentication(code) {
      auth.value = code;
      try {
        await tryAuthentication();
      } catch(e) {
        handleAuthenticationError();
      }
      if(jwt) {
        toNamePageOrToDataPage();
      }
    }

    async function tryAuthentication() {
      const response = await fetch(`/authenticate/${clientId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'clientId': clientId, 'password': auth.value})
      });
      jwt = (await response.json()).jwt;
      localStorage.setItem('jwt', jwt);
      error.value = null;
    }

    function handleAuthenticationError() {
      jwt = null;
      error.value = tm('errors.wrongCode');
    }

    function trySetName() {
      let err = required(order.value.name, tm('errors.requiredName'), 'name-input');
      if(err) {
        error.value = err;
      } else {
        setName();
      }
    }

    async function setName() {
      await fetch('/clients/name', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${jwt}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({'name': order.value.name})
      });
      page.value = 'data';
      error.value = null;
    }

    function trysubmitOrder() {
      let err = getOrderFormError();        
      if(err) {
        error.value = err;
      } else {
        submitOrder();
      }
    }

    async function submitOrder() {
      let orderResponse = await fetch('/orders', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${jwt}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          'framework': order.value.framework,
          'version': order.value.version,
          'thirdPartyTool': order.value.isThirdPartyTool ? order.value.thirdPartyTool : '',
          'projectDescription': order.value.projectDescription,
          'bugDescription': order.value.bugDescription,
          'gitAccessId': await getGitAccessId()
        })
      });
      if(orderResponse.status == 200) {
        page.value = 'success';
        error.value = null;
      } else {
        error.value = `${tm('errors.somethingWrong')} - ${orderResponse.statusText} (${orderResponse.status})`;
      }
    }

    async function getGitAccessId() {
      let gitAccessId;
      if(selectedAccess.value.id != undefined){
        gitAccessId = selectedAccess.value.id;
      } else {
        let gitResponse = await fetch('/git-accesses', {
          method: 'POST',
          headers: {
            'Authorization': `bearer ${jwt}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({'url': order.value.repoUrl, 'accessMode': order.value.accessMode})
        });
        gitAccessId = (await gitResponse.json()).id;
      }
      return gitAccessId;
    }

    async function toNamePageOrToDataPage() {
      let nameResponse = await fetch('/clients/name', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${jwt}`
        }
      })
      if(nameResponse.status == 404) {
        page.value = 'name';
      } else {
        let gitResponse = await fetch('/git-accesses', {
          method: 'GET',
          headers: {
            'Authorization': `bearer ${jwt}`
          }
        })
        gitAccesses.value = await gitResponse.json();
        page.value = 'data';
      }
    }

    function getOrderFormError() {
      let err =
        required(order.value.framework, tm('errors.requiredFramework'), 'choices-framework') ||
        required(order.value.version, tm('errors.requiredVersion'), 'choices-version');
      if(!err && order.value.isSpecificOpSystem)
        err =
          required(order.value.os, tm('errors.requiredOS')) ||
          required(order.value.opSystemVersion, tm('errors.requiredOSVersion'), 'op-system-name-input');
      if(!err && order.value.isSpecificBrowser)
        err =
          required(order.value.browser, tm('errors.requiredBrowserType')) ||
          required(order.value.browserVersion, tm('errors.requiredBrowserVersion'), 'browser-system-name-input');
      if(!err && order.value.isAvailableApp)
        err = required(order.value.availableAppUrl, tm('errors.requiredAppUrl'), 'app-url-input');
      if(!err)
        err =
          required(order.value.repoUrl, tm('errors.requiredGitRepoUrl'), 'repo-url-input') ||
          required(order.value.accessMode, tm('errors.requiredProjectSharing')) ||
          required(order.value.projectDescription, tm('errors.requiredProjectDes'), 'project-description-input') ||
          required(order.value.bugDescription, tm('errors.requiredBugDes'), 'bug-description-input');
      if(!err && order.value.isThirdPartyTool)
        err = required(order.value.thirdPartyTool, tm('errors.requiredThirdPartyTool'), 'third-party-tool-input');
      return err;
    }

    function changeFramework(newFramework) {
      order.value.framework = newFramework;
      order.value.version = undefined;
      order.value.os = undefined;
      order.value.opSystemVersion = undefined;
      order.value.isSpecificOpSystem = undefined;
      order.value.isSpecificBrowser = undefined;
      order.value.browser = undefined;
      order.value.browserVersion = undefined;
    }

    return { page, error, order, auth, aspVersions, vueVersions, gitAccesses, selectedAccess, trySubmitEmail, checkAuthentication, trySetName, trysubmitOrder, changeFramework }
  }
}
</script>

<style>
#carousel-testimonials {
  position: fixed;
  height: 100vh;
  width: 100vw;
  background-image: url('../assets/img/pricing3.jpg');
}
.full-height{
  overflow: auto;
  height: 100vh;
}

</style>