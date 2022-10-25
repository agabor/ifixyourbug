import { useAdminAuthentication } from "./admin";
import { useUserAuthentication } from "./client";

export function authenticator(isClient) {
  if(isClient) 
    return useUserAuthentication();
  else
    return useAdminAuthentication();
}