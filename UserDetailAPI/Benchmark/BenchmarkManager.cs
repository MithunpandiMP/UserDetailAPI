using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Builder;

namespace UserDetailAPI.Benchmark
{
    [MemoryDiagnoser]
    public class BenchmarkManager
    {

        private static HttpClient _httpClient;

        [Params(1, 25, 50)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            //var factory = new WebApplicationFactory<Program>()
            //.WithWebHostBuilder(configuration =>
            //{
            //    configuration.ConfigureLogging(logging =>
            //    {
            //        logging.ClearProviders();
            //    });
            //});
            //// Potential bug: The logging configuration is not being set to any specific provider.
            ///  This could lead to unexpected behavior.
            //_httpClient = factory.CreateClient();
            _httpClient = new HttpClient();
        }

        [Benchmark]
        public async Task GetAllUsers()
        {
            for (int i = 0; i < N; i++)
            {
                var response = await _httpClient.GetAsync("/GetAllUsers");
            }
        }

        //[Benchmark]
        //public async Task GetProductsOptimized()
        //{
        //    for (int i = 0; i < N; i++)
        //    {
        //        var response = await _httpClient.GetAsync("/GetProductsOptimized");
        //    }
        //}
    }
}
