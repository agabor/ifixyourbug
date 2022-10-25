import { ref } from 'vue';
import { get, post, postData, fetchPost, timeout, requestedPage } from './web';
import router from '@/router';
import { useMessages } from "../store";

const { tm } = useMessages();

const userJwt = ref(localStorage.getItem('jwt'));
const isUserLoggedIn = ref(userJwt.value != null);
const userName = ref(localStorage.getItem('clientName'));
const userEmail = ref(localStorage.getItem('clientEmail'));
const progress = ref(0);
const firstLogin = ref(false);
const page = ref("email");
const authenticationError = ref(null);
let clientId;

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

async function authenticate(email, acceptedPolicy) {
  progress.value = 30;
  let response = await fetchPost('/api/authenticate', {'email': email, 'privacyPolicyAccepted': acceptedPolicy})
  progress.value = 100;
  if(response.status === 200) {
    authenticationError.value = null;
    clientId = (await response.json()).id;
    userEmail.value = email;
    setTimeout(() => {
      page.value = 'auth';
      progress.value = 100;
    }, "500");
  } else if(response.status === 401) {
    firstLogin.value = true;
    progress.value = 0;
  } else {
    userEmail.value = null;
    progress.value = 0;
  }
}

async function authenticateWithCode(code) {
  let response = await fetchPost(`/api/authenticate/${clientId}`,{'password': code})
  let data = await response.json();
  progress.value = 100;
  if(response.status === 200) {
    setTimeout(() => {
      authenticationError.value = null;
      setUserData(data.jwt);
      toNamePageOrToTargetPage();
      progress.value = 100;
    }, "500");
  } else if(response.status === 401) {
    progress.value = 0;
    const passwordExpired = data.passwordExpired;
    if (!passwordExpired) {
      authenticationError.value = tm('errors.wrongCode');
    } else {
      page.value = 'failed';
    }
  }
}

function toNamePageOrToTargetPage() {
  if(userName.value) {
    router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
  } else {
    page.value = 'name';
  }
}

async function setName(name) {
  window.rdt('track', 'SignUp');
  let response = await userPost('/api/clients/name', {'name': name});
  progress.value = 100;
  if(response.status === 200) {
    setTimeout(() => {
      localStorage.setItem('clientName', name);
      userName.value = name;
      router.push(requestedPage.value ? requestedPage.value.fullPath : '/my-orders');
    }, "500");
  } else {
    localStorage.removeItem('clientName');
    userName.value = null;
  }
}

async function cancelLogin() {
  progress.value = 0;
  firstLogin.value = false;
  authenticationError.value = null;
  resetUserData();
  page.value = 'email';
}

export function userGet(route) {
  return get(route, userJwt.value)
}

export function userPost(route, body) {
  return post(route, body, userJwt.value)
}

function userPostData(route, data) {
  return postData(route, data, userJwt.value)
}

function setUserData(jwt) {
  let parts = jwt.split('.');
  if (parts.length === 3) {
    let base64 = parts[1];
    let payload = JSON.parse(b64DecodeUnicode(base64));
    userName.value = payload.name;
    userEmail.value = payload.email;
    if(userName.value) {
      localStorage.setItem('clientName', userName.value);
    }
    localStorage.setItem('clientEmail', userEmail.value);
    setUserJwt(jwt);
  } else {
    resetUserData();
  }
}

function b64DecodeUnicode(str) {
  return decodeURIComponent(atob(str).split('').map(function(c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
  }).join(''));
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
  } else {
    localStorage.removeItem('order');
    resetUserData();
  }
  page.value = "email";
  firstLogin.value = false;
}

let checkJwtPromise = Promise.resolve();
if (userJwt.value) {
  checkJwtPromise = new Promise((resolve) => {
    userGet('/api/authenticate/check-jwt').then(async resp => {
      if (resp.status !== 200) {
        resetUserData();
        timeout.value = true;
      } else {
        setUserData((await resp.json()).jwt);
        router.push(requestedPage.value ? requestedPage.value : '/');
      }
      resolve();
    })
  });
} else {
  resetUserData();
}

export function useUserAuthentication() {
  return {
    page, progress, firstLogin, authenticationError, authenticate, authenticateWithCode, cancelLogin,
    requestedPage, 'jwt': userJwt, setUserData, resetUserData, 'setJwt': setUserJwt, 'name': userName, 'email': userEmail, 'isLoggedIn': isUserLoggedIn, setName, 'get': userGet, 'post': userPost, 'postData': userPostData, 'logout': userLogout
  };
}
