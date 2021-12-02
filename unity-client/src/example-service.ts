import { AuthService } from './auth-service';
import { UtilService } from './util-service';

export class ExampleService {

    constructor(private authService: AuthService, private utilService: UtilService) { }

    async callApi() {
        const token = this.authService.getUser().token;
        try {
            const response = await fetch(this.utilService.getUrl() + '/api/user', {
                method: 'GET', // GET, POST, PUT, DELETE, etc.
                headers: new Headers({
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + token,
                }),
                mode: 'cors', // no-cors, *cors, same-origin
            });
            const body = await response.json();
            this.utilService.showText(body);
        } catch (err) {
            console.log(err);
            this.utilService.showText(err);
        }
    }

    async getNft() {
        const token = this.authService.getUser().token;
        try {
            const response = await fetch(this.utilService.getUrl() + '/api/nft', {
                method: 'GET', // GET, POST, PUT, DELETE, etc.
                headers: new Headers({
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + token,
                }),
                mode: 'cors', // no-cors, *cors, same-origin
            });
            const body = await response.json();
            this.utilService.showText(body);
        } catch (err) {
            console.log(err);
            this.utilService.showText(err);
        }
    }

}