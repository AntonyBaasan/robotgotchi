import { UtilService } from './util-service';
import { User } from '../models/user';
import { IMessageBroker, IMessageListener } from '../models/index';


const firebaseConfig = {
  apiKey: 'AIzaSyD4V2Gcj6E1roskQg7yTekz4eCSNkmQgag',
  authDomain: 'robotgotchi-1.firebaseapp.com',
  projectId: 'robotgotchi-1',
  storageBucket: 'robotgotchi-1.appspot.com',
  messagingSenderId: '292289771339',
  appId: '1:292289771339:web:7a337df8ff49a8fbc45ace',
  measurementId: 'G-R2F4TGRGFS',
};

export class AuthService extends IMessageListener {

  constructor(private utilService: UtilService) {
    super();
    (window as any).firebase.initializeApp(firebaseConfig);
    // after initialize authentication warmup current user
    (window as any).firebase.auth();
  }

  listenMessage(broker: IMessageBroker): void {
    broker.registerListeners('login', async () => {
      const user = await this.login();
      return { messageType: 'userinfo', payload: user };
    });
    broker.registerListeners('logout', async () => {
      await this.logout();
      return { messageType: 'userinfo', payload: null };
    });
    broker.registerListeners('getcurrentuser', async () => {
      const user = await this.getCurrentUser();
      return Promise.resolve({ messageType: 'userinfo', payload: user });
    });
  }

  async login(): Promise<User> {

    const currentUser = await this.getCurrentUser();
    if (currentUser) {
      return currentUser;
    }

    const provider = await (window as any).detectEthereumProvider({ mustBeMetaMask: true });

    if (!provider) {
      throw new Error('Please install MetaMask!');
    }

    // STEP1
    const ethereum = provider;
    var userAddresses = await ethereum.request({ method: 'eth_requestAccounts' });

    if (!userAddresses || userAddresses.length == 0) {
      throw new Error('Please login with MetaMask');
    }

    console.log(userAddresses[0]);

    // STEP2
    const url = this.utilService.getWebApiUrl();
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

    var customToken = verifyResponse.token;

    const userCredential = await (window as any).firebase.auth().signInWithCustomToken(customToken);
    const uid = userCredential.user.uid;
    const token = await userCredential.user.getIdToken();

    return { uid, token };
  }

  async logout(): Promise<void> {
    return await (window as any).firebase.auth().signOut();
  }

  async getCurrentUser(): Promise<User> {
    const currentUser = (window as any).firebase.auth().currentUser;
    if (currentUser) {
      const currentToken = await currentUser.getIdToken();
      return { uid: currentUser.uid, token: currentToken };
    }
  }
}
