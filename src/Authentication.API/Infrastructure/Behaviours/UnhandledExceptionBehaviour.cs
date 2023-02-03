namespace Authentication.API.Infrastructure.Behaviours
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     MediatR pipeline behavior to handle any unhandled exception.
    ///     For more information: https://github.com/jbogard/MediatR/wiki/Behaviors
    /// </summary>
    /// <typeparam name="TRequest">The request object passed in through IMediator.Send.</typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        ///     ctor
        /// </summary>
        public UnhandledExceptionBehaviour()
        {
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="request">The request object passed in through IMediator.Send.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="next">An async continuation for the next action in the behavior chain.</param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                string requestName = typeof(TRequest).Name;

                throw;
            }
        }
    }
}

