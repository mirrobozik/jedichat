import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from "../environments/environment";

export const authConfig: AuthConfig = {

  // Url of the Identity Provider
  issuer: environment.identityProviderUrl,

  // Login Url of the Identity Provider
  loginUrl: `${environment.identityProviderUrl}/connect/authorize`,

  // Login Url of the Identity Provider
  logoutUrl: `${environment.identityProviderUrl}/connect/endsession`,


  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/',

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: environment.clientId,

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. Also provide user sepecific
  scope: 'openid profile email',  
}
