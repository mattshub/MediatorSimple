using System.Threading.Tasks;
using MediatR.Pipeline;
using StructureMap.Pipeline;
using MediatR;

namespace MediatorTest
{
    using System;
    using System.IO;
    using global::StructureMap;

    class IoCConfiguration
    {
        public static IMediator BuildMediator()
        {
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<GreetingRequest>();
                    scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(ICancellableAsyncRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(ICancellableAsyncNotificationHandler<>));
                });

                //Pipeline
                cfg.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPreProcessorBehavior<,>));
                cfg.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPostProcessorBehavior<,>));
                cfg.For(typeof(IPipelineBehavior<,>)).Add(typeof(GenericPipelineBehavior<,>));
                cfg.For(typeof(IRequestPreProcessor<>)).Add(typeof(GenericRequestPreProcessor<>));
                cfg.For(typeof(IRequestPostProcessor<,>)).Add(typeof(GenericRequestPostProcessor<,>));

                // This is the default but let's be explicit. At most we should be container scoped.
                cfg.For<IMediator>().LifecycleIs<TransientLifecycle>().Use<Mediator>();

                cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
                cfg.For<TextWriter>().Use(Console.Out);
            });


            var mediator = container.GetInstance<IMediator>();

            return mediator;
        }
    }
}
