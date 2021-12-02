import { UtilService } from './util-service';
import { User } from './models/user';


const firebaseConfig = {
  apiKey: 'AIzaSyD4V2Gcj6E1roskQg7yTekz4eCSNkmQgag',
  authDomain: 'robotgotchi-1.firebaseapp.com',
  projectId: 'robotgotchi-1',
  storageBucket: 'robotgotchi-1.appspot.com',
  messagingSenderId: '292289771339',
  appId: '1:292289771339:web:7a337df8ff49a8fbc45ace',
  measurementId: 'G-R2F4TGRGFS',
};

export class AuthService {

  private user: User;

  constructor(private utilService: UtilService) {
    (window as any).firebase.initializeApp(firebaseConfig);
  }

  async login(): Promise<User> {
    const provider = await (window as any).detectEthereumProvider();

    if (!provider) {
      throw new Error('Please install MetaMask');
    }

    // STEP1
    const ethereum = provider;
    var userAddresses = await ethereum.request({ method: 'eth_requestAccounts' });

    if (!userAddresses || userAddresses.length == 0) {
      throw new Error('Please login with MetaMask');
    }

    console.log(userAddresses[0]);

    // STEP2
    const url = this.utilService.getUrl();
    const response = await this.utilService.postData(url + '/api/auth/noncetosign', {
      address: userAddresses[0],
    });

    console.log('nonce:', response.nonce);

    // STEP3
    var signature = await ethereum.request({
      method: 'personal_sign',
      params: [`0x${this.utilService.toHex(response.nonce)}`, userAddresses[0]],
    });

    console.log('signature:', signature);

    // STEP4
    const verifyResponse = await this.utilService.postData(url + '/api/auth/verifysignedmessage', {
      signature: signature,
      address: userAddresses[0],
    });

    console.log('verifyResponse:', verifyResponse);
    this.utilService.showText('logged in successfully: ' + verifyResponse.token);

    var customToken = verifyResponse.token;

    const userCredential = await (window as any).firebase.auth().signInWithCustomToken(customToken);
    const uid = await userCredential.user.getIdToken();
    const token = await userCredential.user.getIdToken();

    this.user = { uid, token };
    return this.user;
  }

  getUser(): User {
    return this.user;
  }



}


