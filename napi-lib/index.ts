import {
    enableListenerType,
    disableListenerType,
    targetProcessName,
    releaseProcessName
} from 'dotnet-addon';

export enum EventTypes {
    CONTEXT_MENU_CLICKED,
    EXTENSION_MENU_CLICKED
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
    public static on(type: EventTypes, callback: EventListener): void {
        this.listeners.addEventListener(type.toString(), callback)    
    }

    /**
     * @description Removes a callback listener.
     * @param type
     * @param callback
     */
    public static off(type: EventTypes, callback: EventListener): void {
        this.listeners.removeEventListener(type.toString(), callback)
    }

    private static enabledTypes = new Set<EventTypes>();

    /**
     * @description Begins capturing event data using c# addon.
     * @param type
     */
    public static enableListener(type: EventTypes) {
        switch (type) {
            case EventTypes.CONTEXT_MENU_CLICKED:
            case EventTypes.EXTENSION_MENU_CLICKED: {
                if (!TrapBrowserEvents.enabledTypes.has(type)) {
                    TrapBrowserEvents.enabledTypes.add(type)
                }

                let that = this;

                (function wrapper() {
                    if (TrapBrowserEvents.enabledTypes.has(type)) {
                        enableListenerType(type).then(() => {
                            that.listeners.dispatchEvent(new Event(type.toString()))
                            wrapper()
                        })
                    }
                })()

                break
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
    public static disableListener(type: EventTypes) {
        switch (type) {
            case EventTypes.CONTEXT_MENU_CLICKED:
            case EventTypes.EXTENSION_MENU_CLICKED: {
                if (TrapBrowserEvents.enabledTypes.has(type)) {
                    TrapBrowserEvents.enabledTypes.delete(type)
                }
                disableListenerType(type)
                break
            }
            default: {
                throw new Error(`Error: cannot disable ${type}, unknown!`)
            }
        }
    }

    /**
     * @description When called, the process name will 
     * be targeted by the window hooks.
     * @param processName
     */
    public static targetProcessName(processName: string) {
        targetProcessName(processName);
    }

    /**
     * @description When called, the process name will 
     * NOT be targeted by the window hooks.
     * @param processName
     */
    public static releaseProcessName(processName: string) {
        releaseProcessName(processName);
    }
}