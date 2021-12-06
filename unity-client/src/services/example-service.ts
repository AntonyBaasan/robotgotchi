import { IMessageBroker, IMessageListener } from '../models/index';
import { AuthService } from './auth-service';
import { UtilService } from './util-service';

export class ExampleService extends IMessageListener {

    constructor(private authService: AuthService, private utilService: UtilService) {
        super();
    }

    listenMessage(broker: IMessageBroker): void {
        broker.registerListeners('calltestapi', async () => {
            const value = await this.callApi();
            return { messageType: 'testresponse', payload: value };
        });
        broker.registerListeners('getnft', async () => {
            const value = await this.getNft();
            return { messageType: 'testresponse', payload: value };
        });
        broker.registerListeners('echo', async () => {
            return Promise.resolve({ messageType: 'echoresponse' });
        });
    }

    async callApi(): Promise<any> {
        const user = await this.authService.getCurrentUser();
        const token = user ? user.token : '';
        try {
            const response = await fetch(this.utilService.getWebApiUrl() + '/api/user', {
                method: 'GET', // GET, POST, PUT, DELETE, etc.
                headers: new Headers({
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + token,
                }),
                mode: 'cors', // no-cors, *cors, same-origin
            });
            if (response.status === 200) {
                return await response.json();
            }
        } catch (err) {
            console.log(err);
            return err;
        }
    }

    async getNft(): Promise<any> {
        const user = await this.authService.getCurrentUser();
        const token = user ? user.token : '';
        try {
            const response = await fetch(this.utilService.getWebApiUrl() + '/api/nft', {
                method: 'GET', // GET, POST, PUT, DELETE, etc.
                headers: new Headers({
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + token,
                }),
                mode: 'cors', // no-cors, *cors, same-origin
            });
            if (response.status === 200) {
                return await response.json();
            }
        } catch (err) {
            console.log(err);
            return err;
        }
    }

}