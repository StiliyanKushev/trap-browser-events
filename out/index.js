"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TrapBrowserEvents = void 0;
require("../bin/Release/net7.0/win-x64/publish/TrapBrowserEvents");
var node_api_dotnet_1 = require("node-api-dotnet");
var TrapBrowserEvents = exports.TrapBrowserEvents = /** @class */ (function () {
    function TrapBrowserEvents() {
    }
    /**
     * @description Adds a callback listener.
     * @param type
     * @param callback
     */
    TrapBrowserEvents.on = function (type, callback) {
        this.listeners.addEventListener(type, callback);
    };
    /**
     * @description Removes a callback listener.
     * @param type
     * @param callback
     */
    TrapBrowserEvents.off = function (type, callback) {
        this.listeners.removeEventListener(type, callback);
    };
    /**
     * @description Begins capturing event data using c# addon.
     * @param type
     */
    TrapBrowserEvents.enableListener = function (type) {
        var _this = this;
        switch (type) {
            case "contextMenuClicked":
            case "extensionMenuClicked": {
                (0, node_api_dotnet_1.enableListenerType)(type, function () { return _this.listeners.dispatchEvent(new Event(type)); });
            }
            default: {
                throw new Error("Error: cannot enable ".concat(type, ", unknown!"));
            }
        }
    };
    /**
     * @description Stops capturing event data from c# addon.
     * @param type
     */
    TrapBrowserEvents.disableListener = function (type) {
        switch (type) {
            case "contextMenuClicked":
            case "extensionMenuClicked": {
                (0, node_api_dotnet_1.disableListenerType)(type);
            }
            default: {
                throw new Error("Error: cannot disable ".concat(type, ", unknown!"));
            }
        }
    };
    /**
     * @type EventTarget
     * @private
     * @description all internal listeners described here.
     */
    TrapBrowserEvents.listeners = new EventTarget();
    return TrapBrowserEvents;
}());
