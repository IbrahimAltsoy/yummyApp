using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using yummyApp.Application.Abstract.Common;

namespace yummyApp.Application.Behaviors
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        readonly ILogger<TRequest> _logger;
        readonly Stopwatch _stopwatch;
        readonly IUser _user;

        public PerformanceBehaviour(ILogger<TRequest> logger, Stopwatch stopwatch, IUser user)
        {
            _logger = logger;
            _stopwatch = stopwatch;
            _user = user;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _stopwatch.Start();
            var response = await next();
            _stopwatch.Stop();
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            var requestName = typeof(TRequest).Name;
            var userId = _user.Id ?? "Unknown";
            //var userName = (await _accountService.GetUserNameAsync(_user.Id ?? "")) ?? "Unknown";
            var userName = "ibrahim";
            if (elapsedMilliseconds > 100)
            {
                _logger.LogWarning($"[Performance] Request: {requestName}, User: {userId}-{userName}, Time: {elapsedMilliseconds}");
            }

            return response;
        }
    }
}
