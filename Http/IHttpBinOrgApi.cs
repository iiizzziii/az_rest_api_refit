using Refit;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// This interface represents access to the HttpBin.org API.
/// </summary>
public interface IHttpBinOrgApi
{
    [Get("/status/{code}")]
    Task<HttpContent> StatusCodes(int code);

    [Post("/post")]
    Task<GetRequestResponse> GetRequest(Stream content = null, 
                                        [Query] IDictionary<string, string> query = default);
}