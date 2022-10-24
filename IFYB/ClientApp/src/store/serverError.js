import { ref } from 'vue';
const serverError = ref(null);

export const setServerError = (error) => {
  serverError.value = error;
};

export const resetServerError = () => {
  serverError.value = null;
};

export function useServerError() {
  return { serverError, setServerError, resetServerError };
}