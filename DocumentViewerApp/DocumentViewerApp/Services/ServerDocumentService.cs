using DocumentViewerApp.Client.Data;
using DocumentViewerApp.Client.Services;

namespace DocumentViewerApp.Services;

public class ServerDocumentService(IConfiguration configuration) : IDocumentService
{
    private readonly IConfiguration _configuration = configuration;

    public Task<IEnumerable<DocumentInfo>> FetchAsync()
    {
        var saveLocation = _configuration["DocumentSaveLocation"];
        if (string.IsNullOrWhiteSpace(saveLocation))
        {
            throw new InvalidProgramException("DocumentSaveLocation is not configured");
        }

        DirectoryInfo directory = new(saveLocation);
        var documentInfos = directory
            .GetFiles("*.pdf", SearchOption.TopDirectoryOnly)
            .Select(fi => new DocumentInfo(Guid.NewGuid(), fi.Name, fi.Length));

        return Task.FromResult(documentInfos);
    }
}
