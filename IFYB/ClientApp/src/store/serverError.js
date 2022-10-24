import { ref } from 'vue';
const serverError = ref(null);

export const setServerError = (error) => {
    console.log('setServerError')
  serverError.value = error;
};

export const resetServerError = () => {
    console.log('resetServerError')
  serverError.value = null;
};

export function useServerError() {
  return { serverError, setServerError, resetServerError };
}