using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GrpcTest.Common.Messages.HelloService;

namespace GrpcTest.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Channel channel = new Channel("localhost:9000", ChannelCredentials.Insecure);

            // Single message
            var client = new HelloServiceClient(channel);
            String user = "Roya";

            var reply = client.SayHello(new Common.Messages.HelloNameRequest { Name = user });
            Console.WriteLine("Basic hello world!");
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("*****************************************************************");

            // message with type
            const int years = -7;
            long dateValue = DateTime.Today.AddYears(years).Date.Ticks;
            var response2 = client.SayHelloWithDetails(new Common.Messages.HelloRequest
            {
                Name = user, Age=Math.Abs(years), IsHappy = true, DateOfBirthLong = dateValue
            });
            Console.WriteLine("Hello with type.");
            Console.WriteLine("Greeting: " + response2.Message);
            Console.WriteLine("*****************************************************************");

            // List of messages
            var request3 = new Common.Messages.HelloListRequest();
            request3.Names.Add("rahman");
            request3.Names.Add("maria");
            request3.Names.Add("hosha");
            request3.Names.Add("roya");
            request3.Names.Add("cyrus");
            var response3 = client.SayHelloList(request3);

            Console.WriteLine("Hello list.");
            foreach (var item in response3.Names)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("*****************************************************************");

            // Stream list
            var listOfNames = new List<string>()
            {
                "rahman",
                "maria",
                "hosha",
                "roya",
                "cyrus"
            };

            Console.WriteLine("Hello stream.");
            foreach (var name in listOfNames)
            {
                var streamRequest = new Common.Messages.HelloStreamListRequest();
                streamRequest.Names.Add(name);
                var streamResponse = client.SayHelloListStream(streamRequest);

                foreach (var item in streamResponse.Names)
                {
                    Console.WriteLine(item); 
                }

                Thread.Sleep(1000); // simulating a stream!!!
            }
            Console.WriteLine("*****************************************************************");

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
