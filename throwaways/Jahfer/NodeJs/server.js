var osc = require('osc-min');
var dgram = require('dgram');

var udp = dgram.createSocket('udp4');
var outport = 3333,
	inport  = 3334;
	
console.log("sending heartbeat messages to http://localhost:" + outport);

sendHeartbeat = function() {
	buf = osc.toBuffer({
		address: "/heartbeat",
		args: [
			12,
			"stttttring"
		]
	})
	
	udp.send(buf, 0, buf.length, outport, "localhost");
}

setInterval(sendHeartbeat, 2000);



console.log("recieving messages from http://localhost:" + inport);
var sock = dgram.createSocket("udp4", function (msg, rinfo) {
    try {
        console.log(osc.fromBuffer(msg));
    } catch (error) {
        console.log("invalid OSC packet");
	}
});

sock.bind(inport);