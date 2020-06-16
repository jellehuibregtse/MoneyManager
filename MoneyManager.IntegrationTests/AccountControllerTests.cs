﻿using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MoneyManager.Web;
using Xunit;

namespace MoneyManager.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AccountControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// This tests verifies that all URLs given end with response code in range (200-299); It makes sure that
        /// requests don't fail and return content type text/html. 
        /// </summary>
        /// <param name="url">The URL which is being tested.</param>
        /// <returns></returns>
        [Theory]
        [InlineData("/Account")]
        [InlineData("/Account/Index")]
        [InlineData("/Account/Create")]
        [InlineData("/Account/Details")]
        [InlineData("/Account/Edit")]
        [InlineData("/Account/Delete")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
        public async Task Get_SecurePageRequiresAnAuthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/user/login",
                response.Headers.Location.OriginalString);
        }

        [Theory]
        [InlineData("/")]
        public async Task Get_SecurePageIsAvailableForAuthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = true});
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}