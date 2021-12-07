import { AuthService, ExampleService, UtilService, DiscoveryService } from './services/index';
import { IMessageBroker, IRequestMessage, IResponseMessage, RequestMessageType } from './models/index';

export class UnityBridge implements IMessageBroker {

    private utilService: UtilService;
    private exampleService: ExampleService;
    private authService: AuthService;
    private discoveryService: DiscoveryService;

    private subscribers: { [messageType in RequestMessageType]: { (message: IRequestMessage): Promise<IResponseMessage<any>> }[] } = {} as any;
    private responseMessageListener: (message: IResponseMessage<any>) => void

    constructor() {
        this.utilService = new UtilService();
        this.authService = new AuthService(this.utilService);
        this.exampleService = new ExampleService(this.authService, this.utilService);
        this.discoveryService = new DiscoveryService(this.utilService);

        this.authService.listenMessage(this);
        this.discoveryService.listenMessage(this);
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
