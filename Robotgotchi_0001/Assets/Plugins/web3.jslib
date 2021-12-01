// web3.jslib

mergeInto(LibraryManager.library, {
  OnSendToClient: function(messageText) {
    robogachi.onSendToClient(UTF8ToString(messageText));
  }
});
