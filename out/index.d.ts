export declare enum EventTypes {
    CONTEXT_MENU_CLICKED = 0,
    EXTENSION_MENU_CLICKED = 1
}
export declare class TrapBrowserEvents {
    /**
     * @type EventTarget
     * @private
     * @description all internal listeners described here.
     */
    private static listeners;
    /**
     * @description Adds a callback listener.
     * @param type
     * @param callback
     */
    static on(type: EventTypes, callback: EventListener): void;
    /**
     * @description Removes a callback listener.
     * @param type
     * @param callback
     */
    static off(type: EventTypes, callback: EventListener): void;
    private static enabledTypes;
    /**
     * @description Begins capturing event data using c# addon.
     * @param type
     */
    static enableListener(type: EventTypes): void;
    /**
     * @description Stops capturing event data from c# addon.
     * @param type
     */
    static disableListener(type: EventTypes): void;
    /**
     * @description When called, the process name will
     * be targeted by the window hooks.
     * @param processName
     */
    static targetProcessName(processName: string): void;
    /**
     * @description When called, the process name will
     * NOT be targeted by the window hooks.
     * @param processName
     */
    static releaseProcessName(processName: string): void;
}
