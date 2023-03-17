using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;


var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(ConfigureServices);

var host = builder.Build();

host.Run();


const string HttpBinOrgApiHost = "http://httpbin.org";

//static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
//{
//    services
//        .AddHttpClient("HttpBinOrgApi", (provider, client) =>
//        {
//            client.BaseAddress = new System.Uri(HttpBinOrgApiHost);
//            client.DefaultRequestHeaders.Add("Accept", "application/json");
//        })
//        .AddTypedClient(c => RestService.For<IHttpBinOrgApi>(c));
//}

static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
{
    services.AddHttpClient("HttpBinOrgApi", ConfigureHttpClient)
        .AddTypedClient(c => RestService.For<IHttpBinOrgApi>(c));

}

static void ConfigureHttpClient(IServiceProvider provider, HttpClient client)
{
    client.BaseAddress = new System.Uri(HttpBinOrgApiHost);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}
