import { Component, OnInit, OnDestroy } from '@angular/core';
import { ChatService } from '../../chat.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'jedi-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit, OnDestroy {
  
  constructor(private _chat:ChatService) { }

  private _subMembers: Subscription;
  private _subJoin: Subscription;
  private _subLeave: Subscription;
  users: any[];

  ngOnInit() {    
    this._subMembers = this._chat.members.subscribe((members)=>{
      this.users = members;
    });

    this._subJoin = this._chat.join.subscribe((user)=>{
      this.users.push(user);
    });

    this._subLeave = this._chat.leave.subscribe((user)=>{
      this.users = this.users.filter(u=>u!=user);
    });
  }

  ngOnDestroy(): void {
    this._subMembers.unsubscribe();
    this._subJoin.unsubscribe();
    this._subLeave.unsubscribe();
  }
}
