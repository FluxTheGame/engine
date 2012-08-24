var app = require('express').createServer()
  , io = require('socket.io').listen(app)
  , id = 1;

app.listen(8000);

app.get('/', function (req, res) {
  res.sendfile(__dirname + '/index.html');
});

io.sockets.on('connection', function (socket) {

    socket.emit('connected');
	
	socket.on('touch', function (data) {
		console.log("Touch Event...");
		console.log(data);
	});
	
	socket.on('drop', function (data) {
		console.log("Drop Event...");
		console.log(data);
	});
	
	socket.on('join', function (data) {
		console.log("User Joined Event...");
		console.log(data);
		console.log("ID: "+id);
		
		socket.emit('assignID', {id: id});
		
		id++;
	});

    socket.on('gameapp', function(data) {
        console.log("Game Application joined...");
        console.log(data);

        socket.emit('gameapp', {});
    });
	
});