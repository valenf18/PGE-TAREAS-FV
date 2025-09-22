using System.Text;

namespace MauiExcepciones.Services;

public static class LogService
{
    static readonly string LogPath = Path.Combine(FileSystem.AppDataDirectory, "errors.log");

    public static void WriteLine(string text)
    {
        try
        {
            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {text}{Environment.NewLine}";
            // using / IDisposable: libera el FileStream/StreamWriter aunque haya error
            using var fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(line);
        }
        catch
        {
            // En producción podrías enrutar a otro sink (AppCenter, etc.).
            System.Diagnostics.Debug.WriteLine("Fallo al escribir log.");
        }
    }

    public static string ReadAll() =>
        File.Exists(LogPath) ? File.ReadAllText(LogPath) : "(sin errores registrados)";
}
