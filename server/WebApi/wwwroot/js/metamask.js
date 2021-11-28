let ethereum;

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
    const response = await postData('https://localhost:7226/api/auth/noncetosign', {
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
    const verifyResponse = await postData('https://localhost:7226/api/auth/verifysignedmessage', {
        signature: signature,
        address: userAddresses[0],
    });

    console.log('verifyResponse:', verifyResponse);
    showResult('logged in successfully: ' + verifyResponse.token);
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
        showResult(err);
    }
}

function toHex(stringToConvert) {
    return stringToConvert
        .split('')
        .map((c) => c.charCodeAt(0).toString(16).padStart(2, '0'))
        .join('');
}

function showResult(text) {
    document.getElementById("login-result").textContent = text;
}
