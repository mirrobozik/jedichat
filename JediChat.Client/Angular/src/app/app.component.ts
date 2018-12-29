import { Component, OnInit } from "@angular/core";
import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { authConfig } from "./auth.config";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {
    
  constructor(
    private _oauthService: OAuthService
  ){    
    this._oauthService.configure(authConfig);
    this._oauthService.tokenValidationHandler = new JwksValidationHandler();
    //this._oauthService.loadDiscoveryDocumentAndTryLogin();
    this._oauthService.loadDiscoveryDocument()
      .then( doc => {
        this._oauthService.tryLogin()
          .catch(err => {
            console.error(err);
          })
          .then(() => {
            if(!this._oauthService.hasValidAccessToken()) {
              this._oauthService.initImplicitFlow();
            } else {
              var token = this._oauthService.getAccessToken();
              console.log('token', token);              
            }
          });
      });
  }

  ngOnInit(): void {    
  }

}
