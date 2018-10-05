import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ChatService } from '../../chat.service';
import { Subscription } from 'rxjs';
import { ChatMessage, ChatMessageImpl } from '../../chat.model';

@Component({
  selector: 'jedi-conversation',
  templateUrl: './conversation.component.html'
})
export class ConversationComponent implements OnInit, OnDestroy {  

  constructor(
    private _activedRoute: ActivatedRoute,
    private _chat: ChatService) {}

  private _sub: Subscription;
  private _subMessage: Subscription;
  currentUser : string;
  messages: ChatMessage[] = [];
  sending: boolean;
  newMessage: ChatMessage = new ChatMessageImpl();

  ngOnInit() {
    this._sub = this._activedRoute.paramMap.subscribe((params: ParamMap)=>{
      this.currentUser = params.get("userId");
      this.newMessage.toUuid = this.currentUser;
    });
    this._subMessage = this._chat.message.subscribe((msg)=>{
      console.log('message', msg);
      this.messages.push(msg);
    });
  }

  ngOnDestroy(): void {
    this._sub.unsubscribe();
    this._subMessage.unsubscribe();
  }

  send():void {
    var msg = Object.assign({}, this.newMessage);
    this.sending = true;
    this._chat.send(msg)
     .then(()=>{
       this.sending = false;
       this.newMessage.body = null;
     })
     .catch((e)=>{
       this.sending = false;
     })
     ;
  }
}
