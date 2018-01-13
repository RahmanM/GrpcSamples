setlocal

cd /d %~dp0

set TOOLS_PATH=D:\rahman\GrpcTest.Server\packages\Grpc.Tools.1.8.3\tools\windows_x64
set GRPC_TOOLS_PATH=D:\rahman\GrpcTest.Server\packages\Grpc.Tools.1.8.3\tools\windows_x64
mkdir Generated

%TOOLS_PATH%\protoc.exe ^
         --plugin=protoc-gen-grpc=%GRPC_TOOLS_PATH%\grpc_csharp_plugin.exe ^
         --csharp_out=Generated ^
         --grpc_out=Generated ^
         -I . ^
         -I protos\ ^
         protos\messages.proto


endlocal