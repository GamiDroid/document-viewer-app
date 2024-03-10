
using DocumentViewerApp.Client.Services;

namespace DocumentViewerApp.Apis;

public static class DocumentEndpoints
{
    public static void MapDocumentEndpoints(this WebApplication app)
    {
        app.MapGet("_api/documents", GetAllDocumentsAsync);
    }

    private static async Task<IResult> GetAllDocumentsAsync(IDocumentService documentService)
    {
        var documentInfos = await documentService.FetchAsync();
        return Results.Ok(documentInfos);
    }
}
