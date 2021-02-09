using Xunit;
using IfoodAPI;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Reflection;

namespace IFoodAPI.Teste.API
{
    public class ProdutosTest
    {
        private readonly HttpClient _client;


        [Theory]
        [InlineData("GET")]
        public async Task GetAll(string method)
        {
            //arrange 
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/produtos/");
            //act
            var response = await _client.SendAsync(request);
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Theory]
        [InlineData("GET", 215)]
        [InlineData("GET", 1)]        
        public async Task Get(string method, int? id = null)
        {
            //arrange 
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/produtos/{id}");
            //act
            var response = await _client.SendAsync(request);
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        public ProdutosTest()
        {
            var projectDir = GetProjectPath("", typeof(Startup).GetTypeInfo().Assembly);
            var server =  new TestServer(new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseContentRoot(projectDir)
                    .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json")
                    .Build()).UseStartup<Startup>());

            _client = server.CreateClient();
        }

        private static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = System.AppContext.BaseDirectory;

            // Find the path to the target project
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                directoryInfo = directoryInfo.Parent;

                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                if (projectDirectoryInfo.Exists)
                {
                    var projectFileInfo = new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj"));
                    if (projectFileInfo.Exists)
                    {
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
                    }
                }
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }

    }
}
