using DocumentViewerApp.Client.Data;
using System.Net.Http.Json;

namespace DocumentViewerApp.Client.Services;

public class ClientDocumentService(HttpClient httpClient) : IDocumentService
{
    private readonly HttpClient _httpClient = httpClient;

    public Task<IEnumerable<DocumentInfo>> FetchAsync()
    {
        return _httpClient.GetFromJsonAsync<IEnumerable<DocumentInfo>>("_api/documents")!;
    }
}
