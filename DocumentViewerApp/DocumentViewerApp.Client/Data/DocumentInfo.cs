namespace DocumentViewerApp.Client.Data;

public record DocumentInfo(
    Guid Id,
    string FileName,
    long SizeInBytes)
{
}
