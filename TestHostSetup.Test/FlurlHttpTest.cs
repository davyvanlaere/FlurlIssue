using Flurl.Http.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestHostSetup.Test
{
    public class FlurlHttpTest : IClassFixture<CustomWebApplicationFactory<TestHostSetup.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<TestHostSetup.Startup>
            _factory;

        public FlurlHttpTest(CustomWebApplicationFactory<TestHostSetup.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task TestFlurl()
        {
            using (var test = new HttpTest())
            {
                var response = await _client.GetAsync("/WeatherForecast");
                Assert.Equal<HttpStatusCode>(System.Net.HttpStatusCode.OK, response.StatusCode);

                // This fails ... 
                // Thsi approach worked in our 2.2 tests, but since converting to 3.1 it no longer does.
                test.ShouldHaveMadeACall();
            }
        }
    }
}