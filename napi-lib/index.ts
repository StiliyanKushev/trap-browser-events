import {
    enableListenerType,
    disableListenerType
} from 'dotnet-addon';

module TrapBrowserEvents {
    export type eventTypes = 'contextMenuClicked' | 'extensionMenuClicked'
}

export class TrapBrowserEvents {
    /**
     * @type EventTarget
     * @private
     * @description all internal listeners described here.
     */
    private static listeners = new EventTarget()

    /**
     * @description Adds a callback listener.
     * @param type
     * @param callback
     */
    public static on(type: TrapBrowserEvents.eventTypes, callback: EventListener): void {
        this.listeners.addEventListener(type, callback)    
    }

    /**
     * @description Removes a callback listener.
     * @param type
     * @param callback
     */
    public static off(type: TrapBrowserEvents.eventTypes, callback: EventListener): void {
        this.listeners.removeEventListener(type, callback)    
    }

    private static enabledTypes = new Set <TrapBrowserEvents.eventTypes>();

    /**
     * @description Begins capturing event data using c# addon.
     * @param type
     */
    public static enableListener(type: TrapBrowserEvents.eventTypes) {
        switch (type) {
            case "contextMenuClicked":
            case "extensionMenuClicked": {
                if (!TrapBrowserEvents.enabledTypes.has(type)) {
                    TrapBrowserEvents.enabledTypes.add(type);
                }

                let that = this;

                (function wrapper() {
                    if (TrapBrowserEvents.enabledTypes.has(type)) {
                        enableListenerType(type).then(() => {
                            that.listeners.dispatchEvent(new Event(type))
                            wrapper()
                        })
                    }
                })()

                break;
            }
            default: {
                throw new Error(`Error: cannot enable ${type}, unknown!`)
            }
        }
    }

    /**
     * @description Stops capturing event data from c# addon.
     * @param type
     */
    public static disableListener(type: TrapBrowserEvents.eventTypes) {
        switch (type) {
            case "contextMenuClicked":
            case "extensionMenuClicked": {
                if (TrapBrowserEvents.enabledTypes.has(type)) {
                    TrapBrowserEvents.enabledTypes.delete(type);
                }
                disableListenerType(type)
                break;
            }
            default: {
                throw new Error(`Error: cannot disable ${type}, unknown!`)
            }
        }
    }
}