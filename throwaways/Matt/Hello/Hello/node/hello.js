//Hello World node.js with c#

var express = require('express')
  , server = express.createServer()
  , socketio = require('socket.io');

  // configure Express
server.configure(function () {
    server.use(express.bodyParser());
    server.use('/content', express.static(__dirname + '/content'));
    server.use('/scripts', express.static(__dirname + '/scripts'));
    });

// start server listening at host:port
server.listen(3000, 'localhost'); // http listen on host:port e.g. http://localhost:3000/

// configure Socket.IO
var io = socketio.listen(server); // start socket.io
console.log('Listening...');



// ***************************************************************
//    Socket.IO Client Handlers
// ***************************************************************
io.sockets.on('connection', function (socket) {

    socket.on('hello', function (data) {
        console.log('received hello');
		console.log(data);
        io.sockets.emit('hi', {SomeDataOne: "This is my data!"});  // broadcast event to all clients
    });

    socket.on('disconnect', function () {
        console.log('client disconnected');
        io.sockets.emit('clientdisconnected'); // broadcast event to all clients (no data)
    });

});

// ***************************************************************
//    Socket.IO Namesspace 'logger'
// ***************************************************************
var logger = io
  .of('/logger')
  .on('connection', function (nsSocket) {
      console.log('client connected to [logger] namespace');
      logger.emit('traceEvent', new eventLog({
          eventCode: 'connection',
          messageText: 'logger namespace'
      }));  // all 'logger' clients will receive this event

      nsSocket.on('disconnect', function () {
          console.log('client disconnected from [logger] namespace');
          logger.emit('traceEvent', new eventLog({
              eventCode: 'disconnect',
              messageText: 'logger namespace'
          }));  // all 'logger' clients will receive this event
      });

      nsSocket.on('messageAck', function (data, fn) {
          console.log('[logger].[messageAck]: {0}'.format(JSON.stringify(data)));
          
          if (fn != 'undefined') {
              console.log('  sending ack message \r\n');
              fn('hello son, {0}'.format( data.hello));
          } else {
              console.log(' ** expecting return function to call, but was missing?');
          }

          // also bcast traceEvent message
          logger.emit('traceEvent', new eventLog({
              eventCode: 'messageAck',
              messageText: 'logger namespace'
          }));  // all 'logger' clients will receive this event
      });
      
  });

  // simple object to pass on events - matches our C# object
  function eventLog(obj) {
      this.eventCode; // map to socket.io onEvents: onSignOn, sessionStart, instructorJoin, pollingStart
      this.msgText; // 
      this.timeStamp = new Date().toLocaleTimeString();

      // IF AN OBJECT was passed then initialize properties from that object
      for (var prop in obj) {
          this[prop] = obj[prop];
      }
  };

