import { ref } from 'vue'

const serverError = ref(null);

export function useServerError() {
  const setServerError = (error) => {
    serverError.value = error;
  };
  return { serverError, setServerError };
}

const authenticationPage = ref(null);

export function useAuthentication() {
  const setAuthenticationPage = (page) => {
    authenticationPage.value = page;
  };
  return { authenticationPage, setAuthenticationPage };
}