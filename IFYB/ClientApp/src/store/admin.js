import { get, post, timeout, requestedPage } from './web'
import { ref } from 'vue'

const adminJwt = ref(localStorage.getItem('adminJwt'));
const isAdminLoggedIn = ref(false);

setAdminJwt(localStorage.getItem('adminJwt'));

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
  return { requestedPage, 'setJwt': setAdminJwt, 'isLoggedIn': isAdminLoggedIn, 'get': adminGet, 'post': adminPost, 'logout': adminLogout };
}
