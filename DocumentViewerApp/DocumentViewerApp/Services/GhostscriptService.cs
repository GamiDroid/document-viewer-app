#if WINDOWS

using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;

namespace DocumentViewerApp.Services;

public class GhostscriptService
{
    private readonly GhostscriptVersionInfo _versionInfo;

    public GhostscriptService(IConfiguration configuration)
    {
        _versionInfo = GetVersionInfoFromDll(configuration["GhostscriptLibraryLocation"]);
    }

    private static GhostscriptVersionInfo GetVersionInfoFromDll(string? libraryDirectory)
    {
        if (string.IsNullOrWhiteSpace(libraryDirectory))
        {
            throw new InvalidProgramException("Ghostscript library directory is empty");
        }

        string gsDllPath = Path.GetFullPath(Path.Combine(libraryDirectory ?? string.Empty, Environment.Is64BitProcess ? @"gsdll64.dll" : @"gsdll32.dll"));
        if (!File.Exists(gsDllPath))
        {
            throw new FileLoadException($"The library {gsDllPath} required to run this program is not present.");
        }

        return new GhostscriptVersionInfo(gsDllPath);
    }

    public Stream ConvertPdfToImage(Stream pdfStream)
    {
        using GhostscriptRasterizer rasterizer = new();

        rasterizer.CustomSwitches.Add("-dNEWPDF=false");
        rasterizer.Open(pdfStream, _versionInfo, dllFromMemory: true);

        var img = rasterizer.GetPage(72, 1);

        var imageStream = new MemoryStream();
        img.Save(imageStream, ImageFormat.Jpeg);

        return imageStream;
    }
}

#endif