const { TrapBrowserEvents, EventTypes } = require('../out/index.js')

// keep program alive
process.stdin.on('data', () => {})

TrapBrowserEvents.on(EventTypes.CONTEXT_MENU_CLICKED, () => {
    console.log('The context menu top right was clicked!')
})

// enable `contextMenuClicked` listener
TrapBrowserEvents.enableListener(EventTypes.CONTEXT_MENU_CLICKED)

// enable hooks for google chrome
TrapBrowserEvents.targetProcessName('chrome.exe')
TrapBrowserEvents.targetProcessName('hoody_msedge.exe')

setInterval(() => {
    console.log('Main thread is working :)')
}, 1000)