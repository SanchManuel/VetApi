using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApi.IntegrationTests
{
    public sealed class HealthCheckTests(VetApiWebApplicationFactory factory) : IClassFixture<VetApiWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task LiveHealthCheck_ShouldReturnHealthy()
        {
            // Act
            var response = await _client.GetAsync("/health/live");

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("Healthy", content);
        }
    }

}