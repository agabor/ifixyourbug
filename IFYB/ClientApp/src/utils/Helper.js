import { useServerError, useUserAuthentication } from "../store";

const { setServerError, resetServerError } = useServerError();
const { get, post } = useUserAuthentication();

export async function getGitAccesses() {
  let response = await get('/api/git-accesses');
  if(response.status == 200) {
    resetServerError();
    return await response.json();
  } else {
    setServerError(response.statusText);
  }
  return [];
}

export async function getGitAccessId(id, url, mode) {
  let gitAccessId;
  if(id){
    gitAccessId = id;
  } else {
    let response = await post(`/api/git-accesses`, {'url': url, 'accessMode': mode});
    if(response.status == 200) {
      resetServerError();
      gitAccessId = (await response.json()).id;
    } else {
      setServerError(response.statusText);
    }
  }
  return gitAccessId;
}