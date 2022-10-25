import { ref } from 'vue';
import { get, post, fetchPost, timeout, requestedPage } from './web';
import router from '@/router';
import { useMessages } from "../store";

const { tm } = useMessages();

const adminJwt = ref(localStorage.getItem('adminJwt'));
const isAdminLoggedIn = ref(false);
const adminEmail = ref('');
const progress = ref(0);
const page = ref("email");
const authenticationError = ref(null);
let adminId;

setAdminJwt(localStorage.getItem('adminJwt'));

async function authenticate(email) {
  progress.value = 30;
  let response = await fetchPost('/api/authenticate/admin', {'email': email})
  progress.value = 100;
  if(response.status === 200) {
    authenticationError.value = null;
    adminId = (await response.json()).id;
    localStorage.setItem('adminId', adminId);
    adminEmail.value = email;
    setTimeout(() => {
      page.value = 'auth';
      progress.value = 100;
    }, "500");
  } else {
    authenticationError.value = tm('errors.notAdministratorEmail');
    progress.value = 0;
  }
}

async function authenticateWithCode(code) {
  let response = await fetchPost(`/api/authenticate/admin/${adminId}`, {'adminId': adminId, 'password': code});
  let data = await response.json();
  if(response.status === 200) {
    authenticationError.value = null;
    setAdminJwt(data.jwt);
    router.push(requestedPage.value ? requestedPage.value.fullPath : '/admin');
  } else if(response.status === 401) {
    const passwordExpired = data.passwordExpired;
    if (!passwordExpired) {
      authenticationError.value = tm('errors.wrongCode');
    } else {
      page.value = 'failed';
    }
  }
}

function cancelLogin() {
  progress.value = 0;
  authenticationError.value = null;
  setAdminJwt(null);
  page.value = 'email';
}

function setAdminJwt(jwt) {
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
      timeout.value = true;
    }
  })
}

function adminLogout() {
  if (adminJwt.value) {
    setAdminJwt(null);
  }
}

export function useAdminAuthentication() {
  return {
    page, progress, 'email': adminEmail, authenticationError, authenticate, authenticateWithCode, cancelLogin,
    requestedPage, 'setJwt': setAdminJwt, 'isLoggedIn': isAdminLoggedIn, 'get': adminGet, 'post': adminPost, 'logout': adminLogout
  };
}
