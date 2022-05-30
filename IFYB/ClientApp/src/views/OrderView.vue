<template>
  <section>
    <div id="carousel-testimonials" class="page-header min-vh-100">
      <span class="mask bg-gradient-dark opacity-4"></span>
      <div class="carousel-inner">
        <carousel-item :class="{'active': page === 'email'}" icon="email-83" title="Email" subTitle="Enter your email." buttonText="Submit" :error="error" @onClickBtn="submitEmail()">
          <div class="row mb-4">
            <input id="emailInput" class="form-control" placeholder="email@example.com" type="email" @keyup.enter="submitEmail()" v-model="order.email">
          </div>
        </carousel-item>
        <two-fa :class="{'active': page === 'auth'}" :error="error" :modelValue="auth" @update:modelValue="checkAuthentication"></two-fa>
        <carousel-item :class="{'active': page === 'name'}" icon="badge" title="Name" subTitle="Enter your name." buttonText="Save" :error="error" @onClickBtn="setName()">
          <div class="row mb-4">
            <input id="name-input" class="form-control" placeholder="Your Name" type="text" @keyup.enter="setName()" v-model="order.name">
          </div>
        </carousel-item>
        <carousel-item :class="{'active': page === 'data'}" width="col-lg-9 col-md-11" icon="spaceship" title="Order data" subTitle="Enter data from your app." buttonText="Submit" :error="error" @onClickBtn="submitOrder()">
          <form>
            <div class="row text-start">
              <div class="col-md-12 d-flex pe-2 mb-3">
                <select-framework :value="order.framework" @changeFramework="changeFramework"></select-framework>
                <select-version :value="order.version" :versions="order.framework == 0 ? vueVersions : order.framework == 1 ? aspVersions : undefined" @changeVersion="(v) => order.version = v"></select-version>
              </div>
              <operating-system v-if="order.framework == 1" :isSpecificOpSystem="order.isSpecificOpSystem" :operatingSystem="order.os" :operatingVersion="order.opSystemVersion" @changeOs="(o) => order.os = o" @changeIsSpecificOpSystem="(b) => order.isSpecificOpSystem = b" @changeVersion="(v) => order.opSystemVersion = v"></operating-system>
              <browser-type v-if="order.framework == 0" :isSpecificBrowser="order.isSpecificBrowser" :browser="order.browser" :browserVersion="order.browserVersion" @changeIsSpecificBrowser="(b) => order.isSpecificBrowser = b" @changeBrowser="(b) => order.browser = b" @changeVersion="(v) => order.browserVersion = v"></browser-type>
              <git-access-selector v-if="gitAccesses.length > 0" :accesses="gitAccesses" :access="selectedAccess" @selectAccess="(a) => selectedAccess = a"></git-access-selector>
              <project-sharing :accessMode="order.accessMode" :url="order.repoUrl" :visible="selectedAccess.url == undefined" @changeAccessMode="(a) => order.accessMode = a" @changeUrl="(u) => order.repoUrl = u"></project-sharing>
              <div class="col-md-12 pe-2 mb-3">
                <div class="form-group mb-0">
                  <label>Project description*</label>
                  <text-editor id="project-description-input" :modelValue="order.projectDescription" placeholder="Project description" @update:modelValue="(t) => order.projectDescription = t"></text-editor>
                </div>
              </div>
              <div class="col-md-12 pe-2 mb-3">
                <div class="form-group mb-0">
                  <label>Bug description*</label>
                  <text-editor id="bug-description-input" :modelValue="order.bugDescription" placeholder="Bug description" @update:modelValue="(t) => order.bugDescription = t"></text-editor>
                </div>
              </div>
              <third-party-tool :isTool="order.isThirdPartyTool" :tool="order.thirdPartyTool" @changeIsThirdPartyTool="(b) => order.isThirdPartyTool = b" @changeThirdPartyTool="(t) => order.thirdPartyTool = t"></third-party-tool>
            </div>
          </form>
        </carousel-item>
        <carousel-item :class="{'active': page === 'success'}" icon="send" title="Successful order" subTitle="We will contact you shortly via email." buttonText="Bact to home" @onClickBtn="$router.push('/')"></carousel-item>
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
import GitAccessSelector from '../components/orderComponents/GitAccessSelector.vue';
import ProjectSharing from '../components/orderComponents/ProjectSharing.vue';
import ThirdPartyTool from '../components/orderComponents/ThirdPartyTool.vue';

export default {
  name: 'OrderView',
  components: { TwoFa, TextEditor, CarouselItem, SelectFramework, SelectVersion, OperatingSystem, BrowserType, GitAccessSelector, ProjectSharing, ThirdPartyTool },
  setup() {
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

    checkJwtIsActive();

    async function checkJwtIsActive() {
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
    
    async function submitEmail() {
      let err = validEmail(order.value.email);
      if(err) {
        error.value = err;
      } else {
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
    }

    async function checkAuthentication(code) {
      auth.value = code;
      try {
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
      } catch(e) {
        jwt = null;
        error.value = 'Wrong code.';
      }
      if(jwt) {
        toNamePageOrToDataPage();
      }
    }

    async function setName() {
      let err = required(order.value.name, 'Name', 'name-input');
      if(err) {
        error.value = err;
      } else {
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
    }

    async function submitOrder() {
      let err = getOrderFormError();        
      if(err) {
        error.value = err;
      } else {
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
            'gitAccessId': gitAccessId
          })
        });
        if(orderResponse.status == 200) {
          page.value = 'success';
          error.value = null;
        } else {
          error.value = `Something wrong - ${orderResponse.statusText} (${orderResponse.status})`;
        }
      }
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
        required(order.value.framework, 'Framework', 'choices-framework') ||
        required(order.value.version, 'Version', 'choices-version');
      if(!err && order.value.isSpecificOpSystem)
        err =
          required(order.value.os, 'Operating system') ||
          required(order.value.opSystemVersion, 'Operating system version', 'op-system-name-input');
      if(!err && order.value.isSpecificBrowser)
        err =
          required(order.value.browser, 'Browser type') ||
          required(order.value.browserVersion, 'Browser version', 'browser-system-name-input');
      if(!err)
        err =
          required(order.value.repoUrl, 'Git repo url', 'repo-url-input') ||
          required(order.value.accessMode, 'Project sharing') ||
          required(order.value.projectDescription, 'Project description', 'project-description-input') ||
          required(order.value.bugDescription, 'Bug description', 'bug-description-input');
      if(!err && order.value.isThirdPartyTool)
        err = required(order.value.thirdPartyTool, 'Third party tool', 'third-party-tool-input');
      return err;
    }

    function changeFramework(newFramework) {
      order.value.version = undefined;
      order.value.os = undefined;
      order.value.opSystemVersion = undefined;
      order.value.framework = newFramework;
      order.value.isSpecificOpSystem = false;
    }

    return { page, error, order, auth, aspVersions, vueVersions, gitAccesses, selectedAccess, submitEmail, checkAuthentication, setName, submitOrder, changeFramework }
  }
}
</script>

<style>
#carousel-testimonials {
  background-image: url('../assets/img/pricing3.jpg');
}
</style>