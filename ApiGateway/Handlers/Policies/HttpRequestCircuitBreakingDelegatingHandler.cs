using System;
using System.Net;
using Polly;

namespace ApiGateway.Handlers.Policies
{
    public class HttpRequestCircuitBreakingDelegatingHandler : DelegatingHandler
    {
        private readonly HttpStatusCode[] _httpStatusCodesWorthRetrying =  {
           HttpStatusCode.RequestTimeout, // 408
           HttpStatusCode.InternalServerError, // 500
           HttpStatusCode.BadGateway, // 502
           HttpStatusCode.ServiceUnavailable, // 503
           HttpStatusCode.GatewayTimeout // 504
        };

        public HttpRequestCircuitBreakingDelegatingHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var policy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(response => _httpStatusCodesWorthRetrying.Contains(response.StatusCode))
            .WaitAndRetryAsync(new[] {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1)
            }, (exception, timeSpan, context) =>
            {
                // Add logic to be executed before each retry, such as logging
                Console.WriteLine($"Exception: {exception.Exception?.Message}");
            });

            var response = await policy.ExecuteAsync(async () => await base.SendAsync(request, cancellationToken));

            return response;
        }
    }
}
