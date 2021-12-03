export interface IRequestMessage {
    messageType: RequestMessageType;
    payload?: any;
}

export interface IResponseMessage<T> {
    messageType: ResponseMessageType;
    payload?: T;
}

export abstract class IMessageBroker {
    abstract registerListeners(messageType: RequestMessageType, callback: () => Promise<IResponseMessage<any>>): void;
}

export abstract class IMessageListener {
    abstract listenMessage(broker: IMessageBroker): void;
}

export type RequestMessageType =
    'echo' |
    'callTestApi' |
    'getNft' |
    'getCurrentUser' |
    'logout' |
    'login';


export type ResponseMessageType =
    'userInfo' |
    'testResponse' |
    'echoResponse';

