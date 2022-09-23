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
  