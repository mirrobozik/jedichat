import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ChatService } from './chat.service';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  // constructor(
  //   private _chat : ChatService,
  //   private _router: Router) {}

  // canActivate(
  //   next: ActivatedRouteSnapshot,
  //   state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    
  //     if (this._chat.connected) {
  //       return true;
  //     }
      
  //     this._router.navigate([""]);

  //     return false;
  // }

  constructor(
    private _oauthService: OAuthService, 
    private _router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this._oauthService.hasValidAccessToken()) {
      return true;
    }

    this._router.navigate(['']);
    return false;
  }
}
