declare module TrapBrowserEvents {
    type eventTypes = 'contextMenuClicked' | 'extensionMenuClicked';
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
    static on(type: TrapBrowserEvents.eventTypes, callback: EventListener): void;
    /**
     * @description Removes a callback listener.
     * @param type
     * @param callback
     */
    static off(type: TrapBrowserEvents.eventTypes, callback: EventListener): void;
    /**
     * @description Begins capturing event data using c# addon.
     * @param type
     */
    static enableListener(type: TrapBrowserEvents.eventTypes): void;
    /**
     * @description Stops capturing event data from c# addon.
     * @param type
     */
    static disableListener(type: TrapBrowserEvents.eventTypes): void;
}
export {};
