const { TrapBrowserEvents } = require('../out/index.js')

// keep program alive
process.stdin.on('data', () => {})

TrapBrowserEvents.on('contextMenuClicked', () => {
    console.log('The context menu top right was clicked!')
})

// enable `contextMenuClicked` listener
TrapBrowserEvents.enableListener('contextMenuClicked')