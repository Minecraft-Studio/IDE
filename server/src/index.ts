import WebSocket from 'ws';
let port = 35565;

const server = new WebSocket.Server({ port });
server.on('connection', (socket, req) => {
	console.log(`Connected`);
	socket.on('message', message => {
		socket.send(`Connected ${message}`);
	});
});
server.on('error', console.error);
server.on('listening', () => {
	console.log(`Running on port ${port} (ws://localhost:${port})`);
});
