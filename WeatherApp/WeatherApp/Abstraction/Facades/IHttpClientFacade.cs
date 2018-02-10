using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Abstraction.Facades
{
    public interface IHttpClientFacade
    {
		Task<HttpResponseMessage> GetAsync(string requestUri);

	    string GetRequestWithQueryParameters(string baseUri, IDictionary<string, string> queryParameters);
    }
}
