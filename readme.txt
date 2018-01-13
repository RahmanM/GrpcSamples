Sample of GRPC client and server communication including:

- Simple Unary communication
- Unary communication using list
- Unary communication using strongly typed
- Both client and server are streaming

Steps:

- install Grpc.Tools via nuget (This creates a folder inside the packages e.g.  GrpcTest.Server\packages\Grpc.Tools.1.8.3\tools\windows_x64)
- install grpc via nuget
- Create the messages.proto file
- Generate code using the protoc.bat file (set the protoc tools folder in bat file e.g. GrpcTest.Server\packages\Grpc.Tools.1.8.3\tools\windows_x64)
- Add reference to google.protobuf
