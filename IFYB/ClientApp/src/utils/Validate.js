export function validEmail(email) {
  if(email === '' || !email) {
    return 'Email is required.';
  } else {
    let re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if(!re.test(email)) {
      return 'Valid email required.';
    }
  }
  return;
}

export function required(e, text, id) {
  if(e === '' || e === null || e === undefined) {
    if(id) {
      document.getElementById(id).focus();
    }
    return text + ' required.';
  }
  return;
}

export function min(e, i) {
  if(e.length < i)
    return ('Minimum character ' + i + '.');
  return;
}
