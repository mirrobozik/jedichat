import { Component, OnInit } from '@angular/core';
import { ChatService } from '../chat.service';
import { Router } from '@angular/router';

@Component({
  selector: 'jedi-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private _chat : ChatService,
    private _router: Router) { }

  userName: string;
  connecting: boolean;

  ngOnInit() {
  }

  login() : void {
    this.connecting = true;

    this._chat.start("http://localhost:5000", this.userName)
    .then(()=>{
      this.connecting = false;
      this._router.navigate(["/messages"])
    });
  }
}
