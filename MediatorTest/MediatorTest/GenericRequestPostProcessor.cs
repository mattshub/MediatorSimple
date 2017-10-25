﻿using System.IO;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace MediatorTest
{
    public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly TextWriter _writer;

        public GenericRequestPostProcessor(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Process(TRequest request, TResponse response)
        {
            _writer.WriteLine("- All Done");
            return Task.FromResult(0);
        }
    }
}
