import { ref } from 'vue';
import { event } from 'vue-gtag';

const eurPrice = ref(null);
const usdPrice = ref(null);
const workdays = ref(null);
const sshKey = ref(null);
const gitServices = ref(null);

fetch('/api/settings').then(resp => {
  resp.json().then(data => {
    eurPrice.value = data.eurPrice;
    usdPrice.value = data.usdPrice;
    workdays.value = data.workdays;
    sshKey.value = data.sshKey;
    gitServices.value = data.gitServices;
  });
});

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


async function get(route, jwt) {
  return await fetch(route, {
    method: 'GET',
    headers: {
      'Authorization': `bearer ${jwt}`
    }
  })
}
async function post(route, body, jwt) {
  return await fetch(route, {
    method: 'POST',
    headers: {
      'Authorization': `bearer ${jwt}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body)
  })
}

const requestedPage = ref(null);
const userJwt = ref(localStorage.getItem('jwt'));
const isUserLoggedIn = ref(false);
const userName = ref(localStorage.getItem('jwt'));
const userEmail = ref(null);

setUserJwt(localStorage.getItem('jwt'));

async function setUserJwt(jwt) {
  event('set-jwt', { 'value': jwt });
  userJwt.value = jwt;
  if (jwt) {
    localStorage.setItem('jwt', jwt);
    await setUserData();
  } else {
    localStorage.removeItem('jwt');
    userName.value = null;
    userEmail.value = null;
    isUserLoggedIn.value = false;
  }
}

async function setUserData () {
  let response = await userGet('/api/clients/client');
  if(response.status == 200){
    let user = await response.json();
    localStorage.setItem('userName', user.name);
    userName.value = user.name;
    userEmail.value = user.email;
    isUserLoggedIn.value = true;
  } else {
    localStorage.removeItem('userName');
    userName.value = null;
    isUserLoggedIn.value = false;
  }
}

async function setName(name) {
  let response = await userPost('/api/clients/name', {'name': name});
  event('set-name', { 'value': response.status });
  if(response.status == 200) {
    resetServerError();
    localStorage.setItem('userName', name);
    userName.value = name;
    isUserLoggedIn.value = true;
  } else {
    setServerError(response.statusText);
    localStorage.removeItem('userName');
    userName.value = null;
    isUserLoggedIn.value = false;
  }
}

function userGet(route) {
  return get(route, userJwt.value)
}

function userPost(route, body) {
  return post(route, body, userJwt.value)
}

if (userJwt.value) {
  userGet('/api/authenticate/check-jwt').then(resp => {
    if (resp.status !== 200) {
      setUserJwt(null);
    }
  })
}

export function useUserAuthentication() {
  return { requestedPage, 'jwt': userJwt, 'setJwt': setUserJwt, 'name': userName, 'email': userEmail, 'isLoggedIn': isUserLoggedIn, setName, 'get': userGet, 'post': userPost };
}

const adminJwt = ref(localStorage.getItem('adminJwt'));
const isAdminLoggedIn = ref(false);

setAdminJwt(localStorage.getItem('adminJwt'));

function setAdminJwt(jwt) {
  event('admin-set-jwt', { 'value': jwt });
  adminJwt.value = jwt;
  if (jwt) {
    localStorage.setItem('adminJwt', jwt);
    isAdminLoggedIn.value = true;
  } else {
    localStorage.removeItem('adminJwt');
    localStorage.removeItem('adminId');
    isAdminLoggedIn.value = false;
  }
}

function adminGet(route) {
  return get(route, adminJwt.value)
}

function adminPost(route, body) {
  return post(route, body, adminJwt.value)
}

if (adminJwt.value) {
  adminGet('/api/authenticate/admin/check-jwt').then(resp => {
    if (resp.status !== 200) {
      setAdminJwt(null);
    }
  })
}

export function useAdminAuthentication() {
  return { requestedPage, 'setJwt': setAdminJwt, 'isLoggedIn': isAdminLoggedIn, 'get': adminGet, 'post': adminPost };
}

function setPaymentToken(token) {
  localStorage.setItem('paymentToken', token);
}

function clearPaymentToken() {
  localStorage.removeItem('paymentToken');
}

function isPaymentInProgress(token) {
  return localStorage.getItem('paymentToken') === token;
}

export function usePayment() {
  return { setPaymentToken, clearPaymentToken, isPaymentInProgress };
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
  if(response.status == 200) {
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
    if(response.status == 200) {
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