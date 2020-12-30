using DrinkManager.API.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Data;

namespace DrinkManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dbConnectionString = Environment.GetEnvironmentVariable("SERILOG_DB");
            Console.WriteLine(dbConnectionString);
            var sinkOpts = new MSSqlServerSinkOptions { TableName = "AppLogs", AutoCreateSqlTable = true, SchemaName = "dbo" };
            var columnOpts = new ColumnOptions
            {
                AdditionalColumns = new List<SqlColumn>
                {
                    new SqlColumn
                    {
                        ColumnName = "UserName",
                        AllowNull = true,
                        DataLength = 50,
                        DataType = SqlDbType.VarChar,
                        PropertyName = "UserName"
                    }
                }
            };


            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: dbConnectionString,
                    sinkOptions: sinkOpts,
                    columnOptions: columnOpts)
                .Enrich.FromLogContext()
                .Enrich.WithUserName()
                .CreateLogger();

            try
            {
                Log.Information("DrinkManager Starting Up");
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<DrinkAppContext>();


                    context.Database.Migrate();
                    Seeder.SeedSettings(context).Wait();
                }
                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "DrinkManager failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
