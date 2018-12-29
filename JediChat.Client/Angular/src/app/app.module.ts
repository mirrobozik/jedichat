import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { HttpClientModule, HttpClient } from "@angular/common/http";
import { Routes, RouterModule } from "@angular/router";
import { OAuthModule } from 'angular-oauth2-oidc';

import { AppComponent } from './app.component';
import { ChatComponent } from './chat/chat.component';
import { MemberListComponent } from './chat/member-list/member-list.component';
import { ConversationComponent } from './chat/conversation/conversation.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth.guard';

const routes : Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "messages",
    component: ChatComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: ":userId",
        component: ConversationComponent,
        canActivate: [AuthGuard] 
      }
    ] 
  }  
];

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    MemberListComponent,
    ConversationComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    OAuthModule.forRoot()
  ],
  providers: [
    HttpClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
