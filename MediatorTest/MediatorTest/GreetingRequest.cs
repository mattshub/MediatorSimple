using MediatR;

namespace MediatorTest
{
    public class GreetingRequest : IRequest<GreetingResponse>
    {
        public string Message { get; }

        public GreetingRequest(string message)
        {
            Message = message;
        }
    }
}
