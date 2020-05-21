using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MusicForWeather.WebApplication;
using System.Net.Http;

namespace MusicForWeather.Integration.Test.Base
{
    public class BaseIntegrationTest
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }

        protected BaseIntegrationTest()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            ));

            Client = Server.CreateClient();
        }
    }
}