
import { IRequestMessage, IResponseMessage } from './models/message';
import { UnityBridge } from './unity-bridge';

class RobotgotchiService {

    private unityBridge: UnityBridge;

    constructor(private unityInstance: any) {
        this.unityBridge = new UnityBridge();
        this.unityBridge.subscribeResponseMessage(this.responseUnityMessage.bind(this));
    }

    public async receiveUnityMessage(message: IRequestMessage) {
        this.unityBridge.receiveMessage(message);
    }

    private responseUnityMessage(message: IResponseMessage<any>) {
        console.log(message);
        this.unityInstance?.SendMessage('[Bridge]', 'ReceiveClientMessage', JSON.stringify(message));
    }

}

(window as any).RobotgotchiService = RobotgotchiService;
