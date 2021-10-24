using Azure.Function;
using Infra.CrossCutting.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Azure.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Environment.CurrentDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

            var configuration = builder.GetContext().Configuration;
            var emailSettings = new EmailSettings
            {
                Smtp = configuration.GetValue<string>("EmailSettings.Smtp"),
                Port = Convert.ToInt32(config["EmailSettings.Port"]),
                From = config["EmailSettings.From"],
                Password = config["EmailSettings.Password"]
            };
            builder.Services.AddSingleton(emailSettings);

            //builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        }
    }
}
