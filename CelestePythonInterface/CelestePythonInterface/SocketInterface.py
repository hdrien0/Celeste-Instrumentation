import struct

class SocketInterface:

    def __init__(self,sock, agent, sessionParameters, preprocessor = lambda x : x):
        self.sock = sock
        self.IODimensions = (26,7)
        self.agent = agent
        self.preprocessor = preprocessor
        self.sessionParameters = sessionParameters

    def agent_output_to_byte(self,floats):
        if len(floats) != 7:
            raise ValueError("Input list must contain 7 floats")
        
        byte = 0
        for i, f in enumerate(floats):
            if f > 0.5:
                byte |= 1 << i
        
        return bytes([byte])

    def run(self):
        receivedData = self.sock.recv(1024)
        if (receivedData.decode("ASCII") != "LOADINFO"):
            print("Invalid data received : expected LOADINFO, got :")
            print(receivedData)
            raise Exception()
        self.sock.sendall(bytes(self.sessionParameters.SerializeToXML(), "UTF-8"))

        try :
            while True:
                receivedData = self.sock.recv(2048)

                try:
                    if (receivedData.decode("ASCII") == "END"):
                        self.sock.sendall(bytes(1))
                        finalData = self.sock.recv(2048)

                        finalPlayerState = struct.unpack(f"{str(self.IODimensions[0])}f", finalData)
                        self.sock.sendall(bytes(1))
                        return finalPlayerState
                except:
                    pass

            
                deserialized = struct.unpack(f"{str(self.IODimensions[0])}f", receivedData)
                self.sock.sendall(self.agent_output_to_byte(self.agent(self.preprocessor(deserialized))))

        except Exception as e:
            print(f"Client disconnected: {e}")
            print(receivedData)