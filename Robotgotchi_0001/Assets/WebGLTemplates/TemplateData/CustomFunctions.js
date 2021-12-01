const Robogachi = () => {
    var privateMethods = {};
    const serverUrl = 'https://doxiyyrbaujb.usemoralis.com:2053/server';
    const appId = 'gGlYieJYC8k7Bt8cpQSciljSCOivT0zEUorFbvCH';
    Moralis.start({serverUrl, appId});

    var userEthNFTs;
    var user;

    function getVersion() {
        console.log(Web3.version);
        return Web3.version;
    }

    function getTestString() {
        return 'Hello From Antony';
    }

    function getWalletAddress() {
        if (user) {
            return readValue(() => user.get('ethAddress'));
            // return readValue(() => user.current().attributes.ethAddress);
        }
        return '';
    }

    function getGasPrice() {
        return readValue(() => Web3.eth.getGasPrice());
    }

    function getItems() {
        return readValue(() => ['bla', 'shla'].join(','));
    }

    function getCurrentUserAddress() {
        if (user) {
            return user.get('ethAddress');
        }
    }

    async function getNftAddress() {
        userEthNFTs = await Moralis.Web3API.account.getNFTs({chain: 'matic', address: getCurrentUserAddress()});
        console.log(userEthNFTs);
        if (userEthNFTs) {
            var tokenIds = userEthNFTs.result.map((r) => r.token_address);
            unityInstance.SendMessage('[Bridge]', 'UpdateArray', tokenIds.join(','));
        }
        return readValue(() => ['bbb', 'ccc'].join(','));
    }

    async function getNftIds() {
        userEthNFTs = await Moralis.Web3API.account.getNFTs({chain: 'matic', address: getCurrentUserAddress()});
        console.log(userEthNFTs);
        if (userEthNFTs) {
            var tokenIds = userEthNFTs.result.map((r) => r.token_id);
            unityInstance.SendMessage('[Bridge]', 'UpdateArray', tokenIds.join(','));
        }
    }

    async function getFirstNftMetadata(index) {
        if (userEthNFTs && userEthNFTs.result.length > 0) {
            // const options = {
            //   chain: 'matic',
            //   addresses: userEthNFTs.result[index].token_address,
            //   token_id: userEthNFTs.result[index].token_id,
            // };
            // const tokenMetadata = await Moralis.Web3API.token.getTokenIdMetadata(options);

            const tokenMetadata = userEthNFTs.result[index].metadata;
            console.log(tokenMetadata);
            if (!tokenMetadata) {
                unityInstance.SendMessage('[Bridge]', 'UpdateText', 'Can not find');
            } else {
                unityInstance.SendMessage('[Bridge]', 'UpdateJson', tokenMetadata);
            }
        }
    }

    function readValue(callback) {
        var returnStr;
        try {
            // get address from metamask
            returnStr = callback();
        } catch (e) {
            returnStr = '';
        }
        return returnStr;
    }

    function onSendToClient(message) {
        var message = JSON.parse(message);
        privateMethods[message.FunctionName](message.Data);
    }

    privateMethods.echo = function (text) {
        console.log('Echo message was received: ', text);
        sendMessageToUnity({
            FunctionName: 'echo',
            Data: 'Echo message from client!',
        });
    }

    // add from here down
    privateMethods.login = async function () {
        user = Moralis.User.current();
        try {
            if (!user) {
                user = await Moralis.authenticate();
            }
            sendMessageToUnity({
                FunctionName: 'loginResult',
                Data: user.get('ethAddress'),
            });
        } catch (err) {
            console.log('err:', err);
            sendMessageToUnity({
                FunctionName: 'error',
                Data: err.message,
            });
        }
        console.log('logged in user:', user);
    }

    privateMethods.logOut = async function () {
        await Moralis.User.logOut();
        console.log('logged out');
    }

    privateMethods.getWalletAddress = function () {
        if (user) {
            // var userWallet = user.current().attributes.ethAddress;
            sendMessageToUnity({
                FunctionName: 'getWalletAddressResponse',
                Data: user.get('ethAddress'),
            })
        } else {
            sendMessageToUnity({
                FunctionName: 'getWalletAddressResponse',
                Data: '',
            })
        }
    }

    var sendMessageToUnity = function (message) {
        unityInstance.SendMessage('[Bridge]', 'ReceiveClientMessage', JSON.stringify(message));
    }

    return {
        getVersion,
        getTestString,
        getWalletAddress,
        getGasPrice,
        getItems,
        getNftAddress,
        getNftIds,
        getFirstNftMetadata,
        onSendToClient,
    };
};
