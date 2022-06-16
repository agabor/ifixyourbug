import { ref } from 'vue'

const serverError = ref(null);

export function useServerError() {
  const setServerError = (error) => {
    serverError.value = error;
  };
  return { serverError, setServerError };
}

const authenticationPage = ref(null);
const activeClient = ref(null);

export function useAuthentication() {
  const setAuthenticationPage = (page) => {
    authenticationPage.value = page;
  };
  const setActiveClient = (client) => {
    activeClient.value = client;
  }
  return { authenticationPage, setAuthenticationPage, activeClient, setActiveClient };
}