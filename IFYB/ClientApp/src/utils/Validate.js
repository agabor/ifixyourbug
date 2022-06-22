export function validEmail(email) {
  if(email === '' || !email) {
    return 'errors.requiredEmail';
  } else {
    let re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if(!re.test(email)) {
      return 'errors.requiredValidEmail';
    }
  }
  return null;
}

export function required(e, errorText) {
  if(e === '' || e === null || e === undefined) {
    return errorText;
  }
  return null;
}

export function min(e, i) {
  if(e.length < i)
    return ('errors.minLength');
  return null;
}
