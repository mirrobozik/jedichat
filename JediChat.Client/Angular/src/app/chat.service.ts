import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@aspnet/signalr";
import { ChatMessageImpl, ChatMessage } from './chat.model';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private _hubConnection: HubConnection;
  private _url : string;  
  private _user: string;
  // Message Event
  private _messageSubject: ReplaySubject<ChatMessage> = new ReplaySubject<ChatMessage>();
  private _message: Observable<ChatMessage> = this._messageSubject.asObservable();
  // Members Event
  private _membersSubject: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);
  private _members: Observable<string[]> = this._membersSubject.asObservable();
  // Join Event
  private _joinSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  private _join: Observable<string> = this._joinSubject.asObservable();
  // Leave Event
  private _leaveSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  private _leave: Observable<string> = this._leaveSubject.asObservable();
  // Close Event
  private _closeSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  private _close: Observable<any> = this._closeSubject.asObservable();

  constructor(private _http: Http) { }
  
  start(url:string, userId:string) : Promise<void> {

    this._url = url;
    this._user = userId;

    var builder = new HubConnectionBuilder();
    builder.configureLogging(LogLevel.Debug);
    builder.withUrl(`${this._url}/chat?uuid=${this._user}`);

    this._hubConnection = builder.build();    
    

    this._hubConnection.onclose((e)=>{
      this._closeSubject.next(e);
    });

    this._hubConnection.on("message", (msg)=>{
      if (msg===null) {
        return;
      }
      this._messageSubject.next(msg);
    });

    this._hubConnection.on("members", (members)=>{
      this._membersSubject.next(members);
    });

    this._hubConnection.on("joined", (user)=>{
      this._joinSubject.next(user);
    });

    this._hubConnection.on("leaved", (user)=>{
      this._leaveSubject.next(user);
    });

    return this._hubConnection.start();
  }

  stop() : Promise<void> {
    return this._hubConnection.stop()
      .then(()=>{
        this._user = null;
        this._hubConnection = null;
      });
  }

  send(message:ChatMessageImpl) : Promise<void> {
    return this._hubConnection
      .invoke("send", message);
  } 

  public get user() : string {
    return this._user;
  }
  
  public get connected() : boolean {
    return this._hubConnection!=null;
  }
  
  public get message() : Observable<ChatMessage> {
    return this._message;
  }
  
  public get members() : Observable<string[]> {
    return this._members;
  }

  public get join() : Observable<string> {
    return this._join;
  }

  public get leave() : Observable<string> {
    return this._leave;
  }
  
  public get close() : Observable<any> {
    return this._close;
  }

  public getMessages(fromUser:string) : Promise<ChatMessage> {
    let users = [this.user, fromUser];
    let usersParamValue = users.join(",")
    return this._http.get(`${this._url}/messages?users=${usersParamValue}&limit=50`)
      .toPromise()
      .then((data)=>{
        return data.json()
      });
  }
}
