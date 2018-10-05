import { Component, OnInit } from "@angular/core";
import { ChatService } from "../chat.service";
import { Router } from "@angular/router";

@Component({
  selector: "jedi-chat",
  templateUrl: "./chat.component.html"
})
export class ChatComponent implements OnInit {
    
  userName: string;

  constructor(
    private _chat: ChatService, 
    private _router: Router) { }

  ngOnInit() {
    this.userName = this._chat.user;
  }

  logout() : void {
    this._chat.stop()
      .then(()=>{
        this._router.navigate([""]);
      });
  }
}
