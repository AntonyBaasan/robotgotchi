var robotgotchi = new RobotgotchiService();

async function DetectMetaMask() {
    await robotgotchi.receiveUnityMessage({ messageType: 'login' });
}

async function CallApi() {
    await robotgotchi.receiveUnityMessage({ messageType: 'callTestApi' });
}

async function GetNft() {
    await robotgotchi.receiveUnityMessage({ messageType: 'getNft' });
}

async function GetCurrentUser() {
    await robotgotchi.receiveUnityMessage({ messageType: 'getCurrentUser' });
}

async function Logout() {
    await robotgotchi.receiveUnityMessage({ messageType: 'logout' });
}

