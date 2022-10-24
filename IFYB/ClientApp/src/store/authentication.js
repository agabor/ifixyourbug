import { ref } from 'vue';
import { get, post, postData, timeout, requestedPage } from './web'
import { resetServerError, setServerError } from './serverError'

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
  window.rdt('track', 'SignUp');
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

export function userGet(route) {
  return get(route, userJwt.value)
}

export function userPost(route, body) {
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
    let payload = JSON.parse(b64DecodeUnicode(base64));
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
