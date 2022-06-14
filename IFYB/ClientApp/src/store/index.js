import { ref } from 'vue'

const serverError = ref(null);

export function useServerError() {
  const setServerError = (error) => {
    serverError.value = error;
  };
  return { serverError, setServerError };
}