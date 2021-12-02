import { AuthService } from './auth-service';
import { ExampleService } from './example-service';
import { UtilService } from './util-service';

class Robotgotchi {

    private utilService: UtilService;
    private exampleService: ExampleService;
    private authService: AuthService;

    constructor() {
        this.utilService = new UtilService();
        this.authService = new AuthService(this.utilService);
        this.exampleService = new ExampleService(this.authService, this.utilService);
    }

    public async login(): Promise<void> {
        await this.authService.login();
    }

    public async callTestApi(){
        await this.exampleService.callApi();
    }

    public async getNft(){
        await this.exampleService.getNft();
    }
}

(window as any).Robotgotchi = Robotgotchi;
