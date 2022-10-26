import { ref } from 'vue';
import { get, post, fetchPost, timeout, requestedPage } from './web';
import router from '@/router';

const jwt = ref(localStorage.getItem('adminJwt'));
const isLoggedIn = ref(false);
const email = ref('');
const progress = ref(0);
const firstLogin = ref(false);
const page = ref("email");
const authenticationError = ref(null);
let adminId;

setJwt(localStorage.getItem('adminJwt'));

async function authenticate(e) {
  progress.value = 30;
  let response = await fetchPost('/api/authenticate/admin', {'email': e})
  progress.value = 100;
  if(response.status === 200) {
    authenticationError.value = null;
    adminId = (await response.json()).id;
    localStorage.setItem('adminId', adminId);
    email.value = e;
    setTimeout(() => {
      page.value = 'auth';
      progress.value = 100;
    }, "500");
  } else {
    authenticationError.value = 'errors.notAdministratorEmail';
    progress.value = 0;
  }
}

async function authenticateWithCode(code) {
  let response = await fetchPost(`/api/authenticate/admin/${adminId}`, {'adminId': adminId, 'password': code});
  let data = await response.json();
  if(response.status === 200) {
    authenticationError.value = null;
    setJwt(data.jwt);
    router.push(requestedPage.value ? requestedPage.value.fullPath : '/admin');
  } else if(response.status === 401) {
    const passwordExpired = data.passwordExpired;
    if (!passwordExpired) {
      authenticationError.value = 'errors.wrongCode';
    } else {
      page.value = 'failed';
    }
  }
}

function cancelLogin() {
  progress.value = 0;
  authenticationError.value = null;
  setJwt(null);
  page.value = 'email';
}

function setJwt(newJwt) {
  jwt.value = newJwt;
  if (newJwt) {
    localStorage.setItem('adminJwt', newJwt);
    isLoggedIn.value = true;
  } else {
    localStorage.removeItem('adminJwt');
    localStorage.removeItem('adminId');
    isLoggedIn.value = false;
  }
}

function adminGet(route) {
  return get(route, jwt.value)
}

function adminPost(route, body) {
  return post(route, body, jwt.value)
}


if (jwt.value) {
  adminGet('/api/authenticate/admin/check-jwt').then(resp => {
    if (resp.status !== 200) {
      setJwt(null);
      timeout.value = true;
    }
  })
}

function logout() {
  if (jwt.value) {
    setJwt(null);
  }
}

export function useAdminAuthentication() {
  return {
    page, progress, firstLogin, email, authenticationError, authenticate, authenticateWithCode, cancelLogin,
    requestedPage, setJwt, isLoggedIn, 'get': adminGet, 'post': adminPost, logout
  };
}
