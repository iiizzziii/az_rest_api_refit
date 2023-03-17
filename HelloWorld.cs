using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace RestApiRefit
{
    public class HelloWorld
    {
        private readonly ILogger _logger;
        private readonly IHttpBinOrgApi _client;

        public HelloWorld(ILoggerFactory loggerFactory, IHttpBinOrgApi client)
        {
            _logger = loggerFactory.CreateLogger<HelloWorld>();
            _client = client;
        }

        [Function(nameof(HelloWorld))]
        public async Task<HttpResponseData> Run(
            
                    [HttpTrigger(AuthorizationLevel.Function, "get", "post")]
        
                    HttpRequestData req)

        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            var queryStringCollection = HttpUtility.ParseQueryString(req.Url.Query);
            var queryStrings = queryStringCollection.ToDictionary();

            try
            {
                var result = await _client.GetRequest(req.Body, query: queryStrings);
                await response.WriteAsJsonAsync(result);
            }
            catch (Refit.ApiException e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                await response.WriteStringAsync(e.Message);
            }

            return response;
        }
    }
}
