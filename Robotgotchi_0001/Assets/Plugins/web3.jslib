// web3.jslib

mergeInto(LibraryManager.library, {
  OnSendToClient: function(messageText) {
    robogotchiWrapper.receiveUnityMessage(UTF8ToString(messageText));
  }
});
