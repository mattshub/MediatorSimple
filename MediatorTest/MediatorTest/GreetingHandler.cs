using System.IO;
using MediatR;
using System.Threading;

namespace MediatorTest
{
    public class GreetingHandler : IRequestHandler<GreetingRequest, GreetingResponse>
    {
        private readonly TextWriter _writer;

        public GreetingHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public GreetingResponse Handle(GreetingRequest message)
        {
            _writer.WriteLine($"--- Handling Ping: {message.Message}");
            Thread.Sleep(1000);
            return new GreetingResponse($"Hi There {message.Message}");
        }
    }
}
