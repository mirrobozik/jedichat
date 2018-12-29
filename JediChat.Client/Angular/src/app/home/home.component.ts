import { Component, OnInit } from '@angular/core';
import { ChatService } from '../chat.service';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'jedi-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private _oauthService: OAuthService,
    private _chat : ChatService,
    private _router: Router) { }

  userName: string;
  serverUrl: string = "http://localhost:5004";
  connecting: boolean;

  ngOnInit() {
  }

  login() : void {
    //this._oauthService.log
    // this.connecting = true;

    // this._chat.start(this.serverUrl, this.userName)
    // .then(()=>{
    //   this.connecting = false;
    //   this._router.navigate(["/messages"])
    // });
  }
}
