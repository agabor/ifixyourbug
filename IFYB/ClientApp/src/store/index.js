import { ref } from 'vue';
import { timeout } from './web'
import { resetServerError, setServerError } from './serverError'
import { userGet, userPost } from './authentication'

export function useTimeout() {
  return { timeout }
}

const eurPrice = ref(null);
const usdPrice = ref(null);
const workdays = ref(null);
const sshKey = ref(null);
const gitServices = ref(null);


const loaded = ref(false);
function onLoad() {
  window.removeEventListener('load', onLoad);
  loaded.value = true;
  fetch('/api/settings').then(resp => {
    resp.json().then(data => {
      eurPrice.value = data.eurPrice;
      usdPrice.value = data.usdPrice;
      workdays.value = data.workdays;
      sshKey.value = data.sshKey;
      gitServices.value = data.gitServices;
    });
  });
  loadBootstrap();
  loadRedditPixel();
  let isEuropean = Intl.DateTimeFormat().resolvedOptions().timeZone.split('/')[0] === 'Europe';
  if (localStorage.getItem('visited') !== 'true' && !isEuropean) {
    localStorage.setItem('visited', 'true');
    fetch('/api/visitor', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({'referrer': document.referrer, 'search': window.location.search, 'timeZone': Intl.DateTimeFormat().resolvedOptions().timeZone, 'analytics': false, 'advertisement': false})
    });
  }
}

window.addEventListener('load', onLoad);

export function useWindowLoad() {
  return { loaded }
}
export function useSettings() {
  return { eurPrice, usdPrice, workdays };
}

const inputErrors = ref({
  framework: null,
  version: null,
  applicationUrl: null,
  specificPlatform: null,
  specificPlatformVersion: null,
  thirdPartyTool: null,
  bugDescription: null,
  accessMode: null,
  url: null,
  selectedAccess: null,
  confirmMessage: null,
  acceptTerms: null,
  name: null,
  email: null,
  message: null
});

export function useInputError() {
  const setInputError = (property, error) => {
    inputErrors.value[property] = error;
  };
  const hasInputError = () => {
    return Object.values(inputErrors.value).some(x => x !== null);
  }
  const resetInputErrors = () => {
    for (const property in inputErrors.value) {
      inputErrors.value[property] = null;
    }
  }
  return { inputErrors, setInputError, resetInputErrors, hasInputError };
}

export function useSshKey() {
  return { sshKey }
}
export function useGitServices() {
  return { gitServices }
}

const gitAccesses = ref([]);

async function setGitAccesses() {
  let response = await userGet('/api/git-accesses');
  if(response.status === 200) {
    resetServerError();
    gitAccesses.value = await response.json();
  } else {
    setServerError(response.statusText);
    gitAccesses.value = [];
  }
}

async function getGitAccessId(id, url, mode) {
  let gitAccessId;
  if(id){
    gitAccessId = id;
  } else {
    let response = await userPost(`/api/git-accesses`, {'url': url, 'accessMode': mode});
    if(response.status === 200) {
      resetServerError();
      gitAccessId = (await response.json()).id;
      setGitAccesses();
    } else {
      setServerError(response.statusText);
    }
  }
  return gitAccessId;
}

export function useGitAccess() {
  setGitAccesses();
  return { gitAccesses, getGitAccessId }
}

const loadedTinymce = ref(false);
const loadedBootstrap = ref(false);
const loadedRedditPixel = ref(false);

function loadTinymce() {
  if(!loadedTinymce.value) {
    const script = document.createElement('script');
    script.src = '/tinymce/tinymce.min.js';
    script.type = 'module';
      script.onload = () => {
        loadedTinymce.value = true;
      };
    document.body.appendChild(script);
  }
}

function loadBootstrap() {
  if(!loadedBootstrap.value) {
    const script = document.createElement('script');
    script.src = '/assets/js/bootstrap.min.js';
    script.type = 'module';
      script.onload = () => {
        loadedBootstrap.value = true;
      };
    document.body.appendChild(script);
  }
}

function loadRedditPixel() {
  if (!loadedRedditPixel.value) {
    loadedRedditPixel.value = true;
    ! function(w, d) {
      if (!w.rdt) {
          var p = w.rdt = function() {
              p.sendEvent ? p.sendEvent.apply(p, arguments) : p.callQueue.push(arguments)
          };
          p.callQueue = [];
          var t = d.createElement("script");
          t.src = "https://www.redditstatic.com/ads/pixel.js", t.async = !0;
          var s = d.getElementsByTagName("script")[0];
          s.parentNode.insertBefore(t, s)
      }
    }(window, document);
    window.rdt('init', 't2_sy5jico8', {
        "optOut": false,
        "useDecimalCurrencyValues": true
    });
    window.rdt('track', 'PageVisit');
  }
}

export function useTinyMce() {
  return { loadedTinymce, loadTinymce }
}

import { messages } from '../utils/Messages'
 
function tm(variable, props) {
  if(variable === null || variable === '')
    return variable;
  let message = variable;
  let parts = variable.split('.');
  if(parts.length > 1)
    message = getMessage(variable.split('.'));
  if(props && Object.keys(props).length > 0)
    message = replaceMessageKeys(message, props);
  return message;
}

function getMessage(parts) {
  let message = messages;
  for (let i = 0; i < parts.length; i++) {
    if(message[parts[i]])
      message = message[parts[i]];
    else
      return null;
  }
  return message;
}

function replaceMessageKeys(message, props) {
  let keys = Object.keys(props);
  for (let i = 0; i < keys.length; i++) {
    const re = new RegExp(`{.?\\b${keys[i]}\\b.?}`, 'gi');
    message =  message.replace(re, props[keys[i]]);
  }
  return message;
}

export function useMessages() {
  return { tm }
}