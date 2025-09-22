using MauiExcepciones.Services;
using System.Text;

namespace MauiExcepciones.Pages;

public partial class FileManagerPage : ContentPage
{
    public FileManagerPage() => InitializeComponent();

    // Ruta completa del archivo en AppDataDirectory
    string FullPath(string file) => Path.Combine(FileSystem.AppDataDirectory, file);

    // Guardar contenido en archivo
    async void OnGuardarClicked(object sender, EventArgs e)
    {
        try
        {
            var file = FileNameEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException(nameof(file));

            var path = FullPath(file);

            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs, Encoding.UTF8);
            await sw.WriteAsync(TextEditor.Text ?? string.Empty);

            StatusLabel.Text = $"Estado: guardado en {path}";
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException || ex is ArgumentNullException)
        {
            LogService.WriteLine($"[Guardar] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Error", ExceptionMapper.ToUserMessage(ex), "OK");
        }
    }

    // Abrir archivo y mostrar contenido
    async void OnAbrirClicked(object sender, EventArgs e)
    {
        try
        {
            var file = FileNameEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException(nameof(file));

            var path = FullPath(file);

            if (!File.Exists(path))
                throw new FileNotFoundException("No existe", path);

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            var content = await sr.ReadToEndAsync();

            TextEditor.Text = content;

            // Mostrar tamaño y fecha de modificación
            var info = new FileInfo(path);
            var size = info.Length;
            var modified = info.LastWriteTime;

            StatusLabel.Text = $"Estado: abierto {path} | {size} bytes | modificado: {modified:g}";
        }
        catch (Exception ex) when (ex is FileNotFoundException || ex is UnauthorizedAccessException || ex is IOException || ex is ArgumentNullException)
        {
            LogService.WriteLine($"[Abrir] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Error", ExceptionMapper.ToUserMessage(ex), "OK");
        }
    }

    // Duplicar archivo con nuevo nombre
    async void OnDuplicarClicked(object sender, EventArgs e)
    {
        try
        {
            var original = FileNameEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(original))
                throw new ArgumentNullException(nameof(original));

            var originalPath = FullPath(original);

            if (!File.Exists(originalPath))
                throw new FileNotFoundException("Archivo original no encontrado.", originalPath);

            var nuevo = await DisplayPromptAsync("Duplicar archivo", "Ingresa el nuevo nombre:");
            if (string.IsNullOrWhiteSpace(nuevo))
                throw new ArgumentNullException(nameof(nuevo));

            var nuevoPath = FullPath(nuevo);

            using var fsRead = new FileStream(originalPath, FileMode.Open, FileAccess.Read);
            using var fsWrite = new FileStream(nuevoPath, FileMode.Create, FileAccess.Write);
            await fsRead.CopyToAsync(fsWrite);

            StatusLabel.Text = $"Estado: duplicado como {nuevo}";
            LogService.WriteLine($"[Duplicar] {original} -> {nuevo}");
        }
        catch (Exception ex) when (ex is FileNotFoundException || ex is UnauthorizedAccessException || ex is IOException || ex is ArgumentNullException)
        {
            LogService.WriteLine($"[Duplicar] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Error", ExceptionMapper.ToUserMessage(ex), "OK");
        }
    }
}