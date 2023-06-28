"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TrapBrowserEvents = void 0;
var dotnet_addon_1 = require("dotnet-addon");
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
                (0, dotnet_addon_1.enableListenerType)(type, function () { return _this.listeners.dispatchEvent(new Event(type)); });
                break;
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
                (0, dotnet_addon_1.disableListenerType)(type);
                break;
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
