import { ref } from 'vue'
import { resetServerError, setServerError } from './serverError'

function checkServerError(response) {
  if(response.status === 200 || response.status === 401 || response.status === 403 || response.status === 404)
    resetServerError();
  else
    setServerError(response.statusText);
}

export async function get(route, jwt) {
  let response = await fetch(route, {
    method: 'GET',
    headers: {
    'Authorization': `bearer ${jwt}`
    }
  })
  checkServerError(response);
  return response;
}

export async function post(route, body, jwt) {
  let response = await fetch(route, {
    method: 'POST',
    headers: {
    'Authorization': `bearer ${jwt}`,
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(body)
  })
  checkServerError(response);
  return response;
}

export async function postData(route, data, jwt) {
  let response = await fetch(route, {
    method: 'POST',
    headers: {
    'Authorization': `bearer ${jwt}`
    },
    body: data
  })
  checkServerError(response);
  return response;
}

export async function fetchPost(route, body) {
  let response = await fetch(route, {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(body)
  })
  checkServerError(response);
  return response;
}

export async function fetchGet(route) {
  let response = await fetch(route, {
    method: 'GET'
  })
  checkServerError(response);
  return response;
}

export const timeout = ref(false);

export const requestedPage = ref(null);