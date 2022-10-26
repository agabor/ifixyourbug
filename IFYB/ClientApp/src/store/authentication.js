import { useAdminAuthentication } from "./admin";
import { useClientAuthentication } from "./client";

export function useAuthenticator(isClient) {
  if(isClient) 
    return useClientAuthentication();
  else
    return useAdminAuthentication();
}