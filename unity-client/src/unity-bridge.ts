import { AuthService } from './services/auth-service';
import { ExampleService } from './services/example-service';
import { UtilService } from './services/util-service';
import { IMessageBroker, IRequestMessage, IResponseMessage, RequestMessageType } from './models/index';

export class UnityBridge implements IMessageBroker {

    private utilService: UtilService;
    private exampleService: ExampleService;
    private authService: AuthService;

    private subscribers: { [messageType in RequestMessageType]: { (message: IRequestMessage): Promise<IResponseMessage<any>> }[] } = {} as any;
    private responseMessageListener: (message: IResponseMessage<any>) => void

    constructor() {
        this.utilService = new UtilService();
        this.authService = new AuthService(this.utilService);
        this.exampleService = new ExampleService(this.authService, this.utilService);

        this.authService.listenMessage(this);
        this.exampleService.listenMessage(this);
    }

    registerListeners(messageType: RequestMessageType, callback: () => Promise<IResponseMessage<any>>) {
        if (this.subscribers[messageType]) {
            this.subscribers[messageType].push(callback);
        } else {
            this.subscribers[messageType] = [callback];
        }
    }

    // listen message from outside
    async receiveMessage(message: IRequestMessage) {
        if (this.subscribers[message.messageType]) {
            for (let i = 0; i < this.subscribers[message.messageType].length; i++) {
                const response = await this.subscribers[message.messageType][i](message);
                // some messages doesn't need response
                if (response) {
                    this.responseMessageListener(response);
                }
            }
        }
    }

    subscribeResponseMessage(callback: (message: IResponseMessage<any>) => void) {
        this.responseMessageListener = callback;
    }
}
