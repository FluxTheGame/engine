var net = require('net');

var tcp_server = net.createServer(function (c) {

	var start = Date.now();

	console.log('<-- server connected');
	c.write("Hello from Node.js!\n");

	c.on('end', function() {
		console.log('<-- server disconnected');
	});

	c.on('data', function(data) {
		console.log('--> ', data.toString());
		console.log('<-- sending heartbeat');
		c.write('Heartbeat ~ ' + (Date.now() - start));
	});

});

tcp_server.listen(8000);