export interface ChatMessage {
    id : string;
    sentUTC : Date;
    fromUuid : string;
    toUuid : string;
    body : string;
}

export class ChatMessageImpl implements ChatMessage {
    id: string;    
    sentUTC: Date;
    fromUuid: string;
    toUuid: string;
    body: string;
}

export interface User {
    id: string;
    name: string;
    unreadMessages: number;
    online: boolean;
}