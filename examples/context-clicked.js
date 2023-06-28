const { TrapBrowserEvents } = require('../out/index.js')

TrapBrowserEvents.on('contextMenuClicked', () => {
    console.log('The context menu top right was clicked!')
})