let ethereum;
let url = location.protocol + '//' + location.hostname;
//let url = 'https://localhost:7226';
let token = '';

const firebaseConfig = {
    apiKey: "AIzaSyD4V2Gcj6E1roskQg7yTekz4eCSNkmQgag",
    authDomain: "robotgotchi-1.firebaseapp.com",
    projectId: "robotgotchi-1",
    storageBucket: "robotgotchi-1.appspot.com",
    messagingSenderId: "292289771339",
    appId: "1:292289771339:web:7a337df8ff49a8fbc45ace",
    measurementId: "G-R2F4TGRGFS"
};
firebase.initializeApp(firebaseConfig);

async function DetectMetaMask() {
    const provider = await detectEthereumProvider();

    if (!provider) {
        throw new Error('Please install MetaMask');
    }

    // STEP1
    ethereum = provider;
    var userAddresses = await ethereum.request({ method: 'eth_requestAccounts' });

    if (!userAddresses || userAddresses.length == 0) {
        throw new Error('Please login with MetaMask');
    }

    console.log(userAddresses[0]);

    // STEP2
    const response = await postData(url + '/api/auth/noncetosign', {
        address: userAddresses[0],
    });

    console.log('nonce:', response.nonce);

    // STEP3
    var signature = await ethereum.request({
        method: 'personal_sign',
        params: [`0x${this.toHex(response.nonce)}`, userAddresses[0]],
    });

    console.log('signature:', signature);

    // STEP4
    const verifyResponse = await postData(url + '/api/auth/verifysignedmessage', {
        signature: signature,
        address: userAddresses[0],
    });

    console.log('verifyResponse:', verifyResponse);
    showText('logged in successfully: ' + verifyResponse.token);

    var customToken = verifyResponse.token;

    const userCredential = await firebase.auth().signInWithCustomToken(customToken);
    const accessToken = await userCredential.user.getIdToken();
    token = accessToken
}

async function CallApi() {
    try {
        const response = await fetch(url + '/api/user', {
            method: 'GET', // GET, POST, PUT, DELETE, etc.
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }),
            mode: 'cors', // no-cors, *cors, same-origin
        });
        const body = await response.json();
        showText(body);
    } catch (err) {
        console.log(err);
        showText(err);
    }
}

async function GetNft() {
    try {
        const response = await fetch(url + '/api/nft', {
            method: 'GET', // GET, POST, PUT, DELETE, etc.
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }),
            mode: 'cors', // no-cors, *cors, same-origin
        });
        const body = await response.json();
        showText(body);
    } catch (err) {
        console.log(err);
        showText(err);
    }
}

async function postData(url = '', data = {}) {
    try {
        // Default options are marked with *
        const response = await fetch(url, {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            mode: 'cors', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Content-Type': 'application/json',
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            redirect: 'follow', // manual, *follow, error
            referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
            body: JSON.stringify(data), // body data type must match "Content-Type" header
        });
        return response.json(); // parses JSON response into native JavaScript objects
    } catch (err) {
        console.log(err);
        showText(err);
    }
}

function toHex(stringToConvert) {
    return stringToConvert
        .split('')
        .map((c) => c.charCodeAt(0).toString(16).padStart(2, '0'))
        .join('');
}

function showText(text) {
    document.getElementById('login-result').textContent = text;
}
