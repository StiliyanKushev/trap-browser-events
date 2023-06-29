"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TrapBrowserEvents = exports.EventTypes = void 0;
var dotnet_addon_1 = require("dotnet-addon");
var EventTypes;
(function (EventTypes) {
    EventTypes[EventTypes["CONTEXT_MENU_CLICKED"] = 0] = "CONTEXT_MENU_CLICKED";
    EventTypes[EventTypes["EXTENSION_MENU_CLICKED"] = 1] = "EXTENSION_MENU_CLICKED";
})(EventTypes || (exports.EventTypes = EventTypes = {}));
var TrapBrowserEvents = exports.TrapBrowserEvents = /** @class */ (function () {
    function TrapBrowserEvents() {
    }
    /**
     * @description Adds a callback listener.
     * @param type
     * @param callback
     */
    TrapBrowserEvents.on = function (type, callback) {
        this.listeners.addEventListener(type.toString(), callback);
    };
    /**
     * @description Removes a callback listener.
     * @param type
     * @param callback
     */
    TrapBrowserEvents.off = function (type, callback) {
        this.listeners.removeEventListener(type.toString(), callback);
    };
    /**
     * @description Begins capturing event data using c# addon.
     * @param type
     */
    TrapBrowserEvents.enableListener = function (type) {
        switch (type) {
            case EventTypes.CONTEXT_MENU_CLICKED:
            case EventTypes.EXTENSION_MENU_CLICKED: {
                if (!TrapBrowserEvents.enabledTypes.has(type)) {
                    TrapBrowserEvents.enabledTypes.add(type);
                }
                var that_1 = this;
                (function wrapper() {
                    if (TrapBrowserEvents.enabledTypes.has(type)) {
                        (0, dotnet_addon_1.enableListenerType)(type).then(function () {
                            that_1.listeners.dispatchEvent(new Event(type.toString()));
                            wrapper();
                        });
                    }
                })();
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
            case EventTypes.CONTEXT_MENU_CLICKED:
            case EventTypes.EXTENSION_MENU_CLICKED: {
                if (TrapBrowserEvents.enabledTypes.has(type)) {
                    TrapBrowserEvents.enabledTypes.delete(type);
                }
                (0, dotnet_addon_1.disableListenerType)(type);
                break;
            }
            default: {
                throw new Error("Error: cannot disable ".concat(type, ", unknown!"));
            }
        }
    };
    /**
     * @description When called, the process name will
     * be targeted by the window hooks.
     * @param processName
     */
    TrapBrowserEvents.targetProcessName = function (processName) {
        (0, dotnet_addon_1.targetProcessName)(processName);
    };
    /**
     * @description When called, the process name will
     * NOT be targeted by the window hooks.
     * @param processName
     */
    TrapBrowserEvents.releaseProcessName = function (processName) {
        (0, dotnet_addon_1.releaseProcessName)(processName);
    };
    /**
     * @type EventTarget
     * @private
     * @description all internal listeners described here.
     */
    TrapBrowserEvents.listeners = new EventTarget();
    TrapBrowserEvents.enabledTypes = new Set();
    return TrapBrowserEvents;
}());
