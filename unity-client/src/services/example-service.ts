import { IMessageBroker, IMessageListener } from '../models/index';
import { AuthService } from './auth-service';
import { UtilService } from './util-service';

export class ExampleService extends IMessageListener {

    constructor(private authService: AuthService, private utilService: UtilService) {
        super();
    }

    listenMessage(broker: IMessageBroker): void {
        broker.registerListeners('callTestApi', async () => {
            const value = await this.callApi();
            return { messageType: 'testResponse', payload: value };
        });
        broker.registerListeners('getNft', async () => {
            const value = await this.getNft();
            return { messageType: 'testResponse', payload: value };
        });
        broker.registerListeners('echo', async () => {
            return Promise.resolve({ messageType: 'echoResponse' });
        });
    }

    async callApi(): Promise<any> {
        const user = await this.authService.getCurrentUser();
        const token = user ? user.token : '';
        try {
            const response = await fetch(this.utilService.getUrl() + '/api/user', {
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
            const response = await fetch(this.utilService.getUrl() + '/api/nft', {
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