import { ref } from 'vue';
import { get, post, postData, timeout, requestedPage } from './web'

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
}

window.addEventListener('load', onLoad);

export function useWindowLoad() {
  return { loaded }
}
export function useSettings() {
  return { eurPrice, usdPrice, workdays };
}

const serverError = ref(null);

const setServerError = (error) => {
  serverError.value = error;
};

const resetServerError = () => {
  serverError.value = null;
};

export function useServerError() {
  return { serverError, setServerError, resetServerError };
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
  repoUrl: null,
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
  return { inputErrors, setInputError, hasInputError };
}


const userJwt = ref(localStorage.getItem('jwt'));
const isUserLoggedIn = ref(userJwt.value != null);
const userName = ref(localStorage.getItem('clientName'));
const userEmail = ref(localStorage.getItem('clientEmail'));

setUserJwt(localStorage.getItem('jwt'));

function setUserJwt(jwt) {
  userJwt.value = jwt;
  if (jwt) {
    localStorage.setItem('jwt', jwt);
    isUserLoggedIn.value = true;
  } else {
    localStorage.removeItem('jwt');
    isUserLoggedIn.value = false;
  }
}

async function setName(name) {
  let response = await userPost('/api/clients/name', {'name': name});
  if(response.status === 200) {
    resetServerError();
    localStorage.setItem('clientName', name);
    userName.value = name;
  } else {
    setServerError(response.statusText);
    localStorage.removeItem('clientName');
    userName.value = null;
  }
}

function userGet(route) {
  return get(route, userJwt.value)
}

function userPost(route, body) {
  return post(route, body, userJwt.value)
}

function userPostData(route, data) {
  return postData(route, data, userJwt.value)
}

async function setUserData(resp) {
  const data = await resp.json();
  let parts = data.jwt.split(".");
  if (parts.length == 3) {
    let base64 = parts[1];
    let payload = JSON.parse(atob(base64));
    userName.value = payload.name;
    userEmail.value = payload.email;
    if(userName.value) {
      localStorage.setItem('clientName', userName.value);
    }
    localStorage.setItem('clientEmail', userEmail.value);
    setUserJwt(data.jwt);
  } else {
    resetUserData();
  }
}

function resetUserData() {
  userName.value = null;
  userEmail.value = null;
  localStorage.removeItem('clientName');
  localStorage.removeItem('clientEmail');
  setUserJwt(null);
}

function userLogout() {
  if (userJwt.value) {
    checkJwtPromise
    .finally(() => {
      localStorage.removeItem('order');
      resetUserData();
    });
  }
}

let checkJwtPromise = Promise.resolve();
if (userJwt.value) {
  checkJwtPromise = new Promise((resolve) => {
    userGet('/api/authenticate/check-jwt').then(async resp => {
      if (resp.status !== 200) {
        setUserJwt(null);
        timeout.value = true;
      } else {
        await setUserData(resp);
      }
      resolve();
    })
  });
}

export function useUserAuthentication() {
  return { requestedPage, 'jwt': userJwt, setUserData, resetUserData, 'setJwt': setUserJwt, 'name': userName, 'email': userEmail, 'isLoggedIn': isUserLoggedIn, setName, 'get': userGet, 'post': userPost, 'postData': userPostData, 'logout': userLogout };
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
    script.src = 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js';
    script.integrity = 'sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF';
    script.crossOrigin = 'anonymous';
    script.type = 'module';
      script.onload = () => {
        loadedBootstrap.value = true;
      };
    document.body.appendChild(script);
  }
}

export function useScripts() {
  return { loadedTinymce, loadTinymce, loadedBootstrap, loadBootstrap }
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