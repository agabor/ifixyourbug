export function validEmail(email) {
  if(email === '' || !email)
    return 'errors.requiredEmail';
  let re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  if(!re.test(email)) {
    return 'errors.requiredValidEmail';
  }
  return null;
}

export function required(e, errorText) {
  if(e) {
    e = e.toString().replaceAll('\n', '');
    e = e.toString().replaceAll(' ', '');
  }
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

export function validGitUrl(url) {
  if(url === '' || !url)
    return 'errors.requiredGitRepoUrl';
  if(!testHttpUrl(url) && !testSshUrl(url)) {
    return 'errors.badSharingUrl';
  }
  return null;
}

function testHttpUrl(url) {
  let re = /^(http(s)?:\/\/.)(www.)?[-a-zA-Z0-9@:%._+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_+.~#?&/=]*)/;
  return re.test(url);
}

function testSshUrl(url) {
  let re = /^(ssh:\/\/.)?[-a-zA-Z0-9:%._+~#=]{2,256}@[-a-zA-Z0-9:%._+~#=]{2,256}(\.|:)[a-z]{2,6}\b([-a-zA-Z0-9@:%_+.~#?&/=].*)(\.git)$/;
  return re.test(url);
}