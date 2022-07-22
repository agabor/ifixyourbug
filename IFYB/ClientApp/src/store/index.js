import { ref, computed } from 'vue';

const eurPrice = ref(null);
const usdPrice = ref(null);
const workdays = ref(null);

fetch('/api/offer').then(resp => {
  resp.json().then(data => {
    eurPrice.value = data.eurPrice;
    usdPrice.value = data.usdPrice;
    workdays.value = data.workdays;
  });
});

export function useOfferData() {
  return { eurPrice, usdPrice, workdays };
}

const serverError = ref(null);

const setServerError = (error) => {
  serverError.value = error;
};

export function useServerError() {
  return { serverError, setServerError };
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
const userName = ref(null);
const userEmail = ref(null);

function setUserJwt(jwt) {
  if (jwt)
    localStorage.setItem('jwt', jwt);
  else
    localStorage.removeItem('jwt');
  userJwt.value = jwt;
  setName();
}

async function setName(name) {
  if(!name){
    let response = await userGet('/api/clients/name');
    if(response.status == 200){
      let user = await response.json();
      userName.value = user.name;
      userEmail.value = user.email;
      isUserLoggedIn.value = true;
    } else {
      userName.value = null;
      userEmail.value = null;
      isUserLoggedIn.value = false;
    }
  } else {
    let response = await userPost('/api/clients/name', {'name': name});
    if(response.status == 200) {
      setServerError(null);
      let user = await (await userGet('/api/clients/name')).json();
      userName.value = user.name;
      userEmail.value = user.email;
      isUserLoggedIn.value = true;
    } else {
      setServerError(response.statusText);
      userName.value = null;
      userEmail.value = null;
      isUserLoggedIn.value = false;
    }
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

function setAdminJwt(jwt) {
  if (jwt) {
    localStorage.setItem('adminJwt', jwt);
  } else {
    localStorage.removeItem('adminJwt');
    localStorage.removeItem('adminId');
  }
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