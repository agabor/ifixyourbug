import { ref, computed } from 'vue'

const serverError = ref(null);

export function useServerError() {
  const setServerError = (error) => {
    serverError.value = error;
  };
  return { serverError, setServerError };
}

const requestedPage = ref(null);

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

const userJwt = ref(localStorage.getItem('jwt'));

function setUserJwt(jwt) {
  if (jwt)
    localStorage.setItem('jwt', jwt);
  else
    localStorage.removeItem('jwt');
  userJwt.value = jwt
}

const isUserLoggedIn = computed(() => userJwt.value !== null);

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
  return { requestedPage, 'setJwt': setUserJwt, 'isLoggedIn': isUserLoggedIn, 'get': userGet, 'post': userPost };
}

const adminJwt = ref(localStorage.getItem('adminJwt'));

function setAdminJwt(jwt) {
  if (jwt)
    localStorage.setItem('adminJwt', jwt);
  else
    localStorage.removeItem('adminJwt');
    adminJwt.value = jwt
}

const isAdminLoggedIn = computed(() => adminJwt.value !== null);

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