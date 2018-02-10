using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using WeatherApp.Abstraction.Facades;

namespace WeatherApp.Facades
{
	[ExcludeFromCodeCoverage]
    public class HttpClientFacade : IHttpClientFacade
    {
	    private readonly HttpClient _client;
	    public HttpClientFacade()
	    {
		    _client = new HttpClient();
	    }
		
	    public Task<HttpResponseMessage> GetAsync(string requestUri)
	    {
		    return _client.GetAsync(requestUri);
	    }

	    public string GetRequestWithQueryParameters(string baseUri, IDictionary<string, string> queryParameters)
	    {
			return QueryHelpers.AddQueryString(baseUri, queryParameters);
		}
    }
}
