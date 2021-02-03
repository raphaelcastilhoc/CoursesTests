using System.Threading.Tasks;

namespace CoursesTests.ExternalAdapters.Http
{
    public interface IHttpClientAdapter
    {
        Task PostAsync<T>(string clientName, string endpointSufix, T dto);
    }
}
