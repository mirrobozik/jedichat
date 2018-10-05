import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { Routes, RouterModule } from "@angular/router";

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
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
