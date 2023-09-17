mergeInto(LibraryManager.library, {
  saveToLocalStorage: function(keyPtr, valuePtr) {
    var key = UTF8ToString(keyPtr);
    var value = UTF8ToString(valuePtr);
    localStorage.setItem(key, value);
  },

  checkLocalStorageKey: function(keyPtr) {
    var key = UTF8ToString(keyPtr);
    return localStorage.getItem(key) !== null;
  },

  loadFromLocalStorage: function(keyPtr) {
    var key = UTF8ToString(keyPtr);
    var value = localStorage.getItem(key);
    var buffer = _malloc(value.length + 1);
    writeAsciiToMemory(value, buffer);
    return buffer;
  },

  removeFromLocalStorage: function(keyPtr) {
    var key = UTF8ToString(keyPtr);
    localStorage.removeItem(key);
  }
});