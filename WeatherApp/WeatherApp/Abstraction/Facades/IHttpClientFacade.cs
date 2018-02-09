using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Abstraction.Facades
{
    public interface IHttpClientFacade
    {
		Task<HttpResponseMessage> GetAsync(string requestUri);

	    string GetRequest(string baseUri, IDictionary<string, string> queryParameters);
    }
}
