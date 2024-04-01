import socket

class SocketServer:
    def __init__(self, host="127.0.0.1", port=25001):
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.bind((host, port))

    def await_connection(self):
        self.sock.listen(1)
        return self.sock.accept()