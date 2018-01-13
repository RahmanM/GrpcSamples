using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrpcTest.Common.Messages.HelloService;
using GrpcTest.Common.Messages;
using Grpc.Core;

namespace GrpcTestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 9000;

            var server = new Server();
            server.Services.Add(BindService(new HelloService()));
            server.Ports.Add(new ServerPort("localhost", port, ServerCredentials.Insecure));

            server.Start();

            Console.WriteLine($"Listening to port {port}");
            Console.WriteLine("Press any key to shutdown");
            Console.ReadLine();
            server.ShutdownAsync().Wait();
        }
    }

    public class HelloService : HelloServiceBase
    {
        public override async Task<HelloNameResponse> SayHello(HelloNameRequest request, ServerCallContext context)
        {
            return await Task.FromResult<HelloNameResponse>( new HelloNameResponse() { Message = $"Hello {request.Name}!" });
        }

        public override async Task<HelloResponse> SayHelloWithDetails(HelloRequest request, ServerCallContext context)
        {
            var happy = request.IsHappy ? "Happy" : "Not Happy";
            DateTime dob = new DateTime(request.DateOfBirthLong);

            return await Task.FromResult<HelloResponse>(new HelloResponse()
            {
                Message = $"Hellow {request.Name}, you are {request.Age} years old, your approx. DOB is {dob} and you are {happy}"
            });
        }

        public override async Task<HelloListResponse> SayHelloList(HelloListRequest request, ServerCallContext context)
        {
            var response = new HelloListResponse();
            foreach (var item in request.Names)
            {
                response.Names.Add($"Hello {item}!");
            }

            return await Task.FromResult<HelloListResponse>(response);
        }

        public override async Task<HelloStreamListResponse> SayHelloListStream(HelloStreamListRequest request, ServerCallContext context)
        {
            var response = new HelloStreamListResponse();
            foreach (var item in request.Names)
            {
                response.Names.Add($"Hello {item}!");
            }

            return await Task.FromResult<HelloStreamListResponse>(response);
        }
    }
}
