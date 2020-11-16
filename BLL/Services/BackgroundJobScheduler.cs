using BLL.Data.Repositories;
using BLL.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BackgroundJobScheduler : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private Timer _timer;

        public BackgroundJobScheduler(IServiceProvider services)
        {
            _services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            var timeToStart = TimeToStart();

            Task.Delay(timeToStart, stoppingToken).ContinueWith(o =>
            {
                _timer = new Timer(DoWork, null, TimeSpan.Zero,
                    TimeSpan.FromDays(1));
            }, stoppingToken);

            return Task.CompletedTask;
        }

        private TimeSpan TimeToStart()
        {
            using (var scope = _services.CreateScope())
            {
                var settingRepository =
                    scope.ServiceProvider
                        .GetRequiredService<ISettingRepository>();

                var timeOfStart = settingRepository.GetSetting(Settings.ReportTime).Value;

                return TimeSpan.Parse(timeOfStart, new CultureInfo("en-US")) -
                    DateTime.Now.TimeOfDay + TimeSpan.FromHours(24);
            }
        }

        private void DoWork(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IEmailService>();

                scopedProcessingService.SendAdminEmail(new Email
                {
                    Subject = "Test email",
                    Body = "Test body"
                });
            }

        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
