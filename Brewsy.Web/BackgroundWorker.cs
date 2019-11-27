using Brewsy.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Brewsy.Web
{
    public class BackgroundWorker : IHostedService
    {
        private readonly ILogger<BackgroundWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public BackgroundWorker(IServiceScopeFactory scopeFactory, ILogger<BackgroundWorker> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<BrewsyContext>();
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();
                var users = context.Users.Where(x => !string.IsNullOrEmpty(x.StripeUserId));

                foreach (var user in users)
                {
                    var requestOptions = new RequestOptions
                    {
                        ApiKey = configuration["Stripe:ApiKey"],
                        StripeAccount = user.StripeUserId
                    };

                    var paymentIntents = new PaymentIntentService();
                    var paymentsIntents = paymentIntents.List(new PaymentIntentListOptions { }, requestOptions);

                    foreach (var paymentIntent in paymentsIntents.Where(x => x.Status == "requires_capture"))
                    {
                        paymentIntents.Capture(paymentIntent.Id, requestOptions: requestOptions);
                    }
                }
            } 

            _logger.LogInformation("Timed Hosted Service is working");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
