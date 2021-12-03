var robotgotchi = new RobotgotchiService();

async function DetectMetaMask() {
    await robotgotchi.receiveUnityMessage({ messageType: 'login' });
}

async function CallApi() {
    await robotgotchi.receiveUnityMessage({ messageType: 'calltestapi' });
}

async function GetNft() {
    await robotgotchi.receiveUnityMessage({ messageType: 'getnft' });
}

async function GetCurrentUser() {
    await robotgotchi.receiveUnityMessage({ messageType: 'getcurrentuser' });
}

async function Logout() {
    await robotgotchi.receiveUnityMessage({ messageType: 'logout' });
}

