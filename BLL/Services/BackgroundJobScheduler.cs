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
            Console.WriteLine("Starting...");
            var timeToStart = TimeToStart();
            var reportInterval = ReportInterval();

            Task.Delay(timeToStart, stoppingToken).ContinueWith(o =>
            {
                _timer = new Timer(DoWork, null, TimeSpan.Zero, reportInterval);
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

                var timeOfStart = TimeSpan.Parse(settingRepository.GetSetting(Settings.ReportTime).Value,
                    new CultureInfo("en-US"));

                var interval = int.Parse(settingRepository.GetSetting(Settings.ReportInterval).Value);

                if (timeOfStart > DateTime.Now.TimeOfDay && interval == 1)
                {
                    interval--;
                }

                var nextReportDate = DateTime.Now.AddDays(interval);
                var reportDateTime = new DateTime(
                    nextReportDate.Year,
                    nextReportDate.Month,
                    nextReportDate.Day,
                    timeOfStart.Hours,
                    timeOfStart.Minutes,
                    timeOfStart.Seconds);

                var totalHours = reportDateTime.Subtract(DateTime.Now).TotalHours;
                settingRepository.SetSetting(Settings.NextReportDate, reportDateTime.ToString("f"));



                return TimeSpan.FromHours(totalHours < 0 ? totalHours + 24 : totalHours);
            }
        }

        private TimeSpan ReportInterval()
        {
            using (var scope = _services.CreateScope())
            {
                var settingRepository =
                    scope.ServiceProvider
                        .GetRequiredService<ISettingRepository>();


                return TimeSpan.FromDays(double.Parse(settingRepository.GetSetting(Settings.ReportInterval).Value));
            }
        }

        private void DoWork(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IEmailService>();

                var settingRepository =
                    scope.ServiceProvider
                        .GetRequiredService<ISettingRepository>();

                scopedProcessingService.SendAdminEmail(new Email
                {
                    Subject = "Test email",
                    Body = "Test body"
                });

                settingRepository.SetSetting(Settings.LastReportDate, DateTime.Now.ToString("f"));
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
