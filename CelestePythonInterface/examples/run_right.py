from CelestePythonInterface import SocketInterface, SessionParameters, SocketServer

def run_right(input):
    return [1,0,0,0,0,0,0]

session_params = SessionParameters()

socket_server = SocketServer()
client_socket, addr = socket_server.await_connection()

while True:
    interface = SocketInterface(client_socket, run_right, SessionParameters())
    interface.run()