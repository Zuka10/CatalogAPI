using EcommerceAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Catalog.Test
{
    public class RateLimiterTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public RateLimiterTest()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(_configuration));

            _client = _server.CreateClient();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [Test]
        public void TestRateLimit()
        {
            for (int i = 0; i < 6; i++)
            {
                var formData = new List<KeyValuePair<string, string>>
                {
                    new("userName", "ZukaZ"),
                    new("password", "Pass1234!")
                };
                var content = new FormUrlEncodedContent(formData);
                var response = _client.PostAsync("/api/authenticate/login", content).Result;
                if (i < 5)
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
                else
                {
                    Assert.AreEqual(HttpStatusCode.TooManyRequests, response.StatusCode);
                }
            }
        }
    }
}