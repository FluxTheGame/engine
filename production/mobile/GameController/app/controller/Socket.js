/*
 * @file Socket.js
 * 
 */


Ext.define('GC.controller.Socket', {
	extend: 'Ext.app.Controller',

	init: function() {

		if (typeof this.socket === "undefined") {
			var connected = this.initSocketConnection();
		}

		if (!connected)
			return;

		var s = this.socket;

		s.on('news', function (data) {
		    console.log('Server says: ', data);
		    s.emit('my other event', { my: 'data' });
		});
	},

	initSocketConnection: function() {
		console.info('[controller/Socket] Initializing socket');

		if (typeof io !== 'undefined') {
			if (this.socket = io.connect('http://'+GC.app.settings.server+':'+GC.app.settings.port)) {
				console.info('[controller/Socket] Socket connected at http://'+GC.app.settings.server+':'+GC.app.settings.port+'/');
				return true;
			} else {
				console.warn('[controller/Socket] Socket.io failed!');
			}
		} else {
			console.warn('[controller/Socket] Socket connection failed! Did you forget to start the Node.js server?');
			return false;
		}
	}
})