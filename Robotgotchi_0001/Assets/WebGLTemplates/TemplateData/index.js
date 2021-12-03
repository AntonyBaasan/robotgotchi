const RobogotchiWrapper = () => {
    // declared in the main.js
    const robotgotchiService = new RobotgotchiService(unityInstance);
    
    function receiveUnityMessage(message) {
        var message = JSON.parse(message);
        robotgotchiService.receiveUnityMessage(message);
    }

    return {
        receiveUnityMessage,
    };
};
