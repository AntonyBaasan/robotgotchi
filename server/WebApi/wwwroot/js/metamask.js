var robotgotchi = new Robotgotchi();

async function DetectMetaMask() {
    await robotgotchi.login();
}

async function CallApi() {
    await robotgotchi.callTestApi();
}

async function GetNft() {
    await robotgotchi.getNft();
}
