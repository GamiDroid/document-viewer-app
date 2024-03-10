using DocumentViewerApp.Client.Data;

namespace DocumentViewerApp.Client.Services;

public interface IDocumentService
{
    Task<IEnumerable<DocumentInfo>> FetchAsync();
}
