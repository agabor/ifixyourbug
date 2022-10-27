import { ref } from 'vue';
import { get, post, postData, fetchPost, timeout, requestedPage } from './web';
import router from '@/router';

const jwt = ref(localStorage.getItem('jwt'));
const isLoggedIn = ref(jwt.value != null);
const name = ref(localStorage.getItem('clientName'));
const email = ref(localStorage.getItem('clientEmail'));
const progress = ref(0);
const firstLogin = ref(false);
const page = ref("email");
const authenticationError = ref(null);
let clientId;

setJwt(localStorage.getItem('jwt'));

async function authenticate(e, acceptedPolicy) {
  progress.value = 30;
  let response = await fetchPost('/api/authenticate', {'email': e, 'privacyPolicyAccepted': acceptedPolicy})
  progress.value = 100;
  if(response.status === 200) {
    clientId = (await response.json()).id;
    email.value = e;
    setTimeout(() => {
      page.value = 'auth';
      progress.value = 100;
    }, "500");
  } else if(response.status === 401) {
    firstLogin.value = true;
    progress.value = 0;
  } else {
    email.value = null;
    progress.value = 0;
  }
}

async function authenticateWithCode(code) {
  let response = await fetchPost(`/api/authenticate/${clientId}`,{'password': code})
  let data = await response.json();
  progress.value = 100;
  authenticationError.value = null;
  if(response.status === 200) {
    setTimeout(() => {
      setData(data.jwt);
      toNamePageOrToTargetPage();
      progress.value = 100;
    }, "500");
  } else if(response.status === 401) {
    progress.value = 0;
    const passwordExpired = data.passwordExpired;
    if (!passwordExpired) {
      authenticationError.value = 'errors.wrongCode';
    } else {
      page.value = 'failed';
    }
  }
}

function toNamePageOrToTargetPage() {
  if(name.value) {
    router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
  } else {
    page.value = 'name';
  }
}

async function setName(n) {
  window.rdt('track', 'SignUp');
  let response = await clientPost('/api/clients/name', {'name': n});
  progress.value = 100;
  if(response.status === 200) {
    setTimeout(() => {
      localStorage.setItem('clientName', n);
      name.value = n;
      router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
    }, "500");
  } else {
    localStorage.removeItem('clientName');
    name.value = null;
  }
}

async function cancelLogin() {
  progress.value = 0;
  firstLogin.value = false;
  authenticationError.value = null;
  resetData();
  page.value = 'email';
}

function setJwt(newJwt) {
  jwt.value = newJwt;
  if (newJwt) {
    localStorage.setItem('jwt', newJwt);
    isLoggedIn.value = true;
  } else {
    localStorage.removeItem('jwt');
    isLoggedIn.value = false;
  }
}

export function clientGet(route) {
  return get(route, jwt.value)
}

export function clientPost(route, body) {
  return post(route, body, jwt.value)
}

function clientPostData(route, data) {
  return postData(route, data, jwt.value)
}

function setData(newJwt) {
  let parts = newJwt.split('.');
  if (parts.length === 3) {
    let base64 = parts[1];
    let payload = JSON.parse(b64DecodeUnicode(base64));
    name.value = payload.name;
    email.value = payload.email;
    if(name.value) {
      localStorage.setItem('clientName', name.value);
    }
    localStorage.setItem('clientEmail', email.value);
    setJwt(newJwt);
  } else {
    resetData();
  }
}

function b64DecodeUnicode(str) {
  return decodeURIComponent(atob(str).split('').map(function(c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
  }).join(''));
}

function resetData() {
  name.value = null;
  email.value = null;
  localStorage.removeItem('clientName');
  localStorage.removeItem('clientEmail');
  setJwt(null);
}

function logout() {
  if (jwt.value) {
    checkJwtPromise
    .finally(() => {
      localStorage.removeItem('order');
      resetData();
    });
  } else {
    localStorage.removeItem('order');
    resetData();
  }
  page.value = "email";
  firstLogin.value = false;
}

let checkJwtPromise = Promise.resolve();
if (jwt.value) {
  checkJwtPromise = new Promise((resolve) => {
    clientGet('/api/authenticate/check-jwt').then(async resp => {
      if (resp.status !== 200) {
        resetData();
        timeout.value = true;
      } else {
        setData((await resp.json()).jwt);
      }
      resolve();
    })
  });
} else {
  resetData();
}

export function useClientAuthentication() {
  return {
    page, progress, firstLogin, authenticationError, authenticate, authenticateWithCode, cancelLogin,
    requestedPage, jwt, setData, resetData, setJwt, name, email, isLoggedIn, setName, 'get': clientGet, 'post': clientPost, 'postData': clientPostData, logout
  };
}
