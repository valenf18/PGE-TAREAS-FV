using MauiExcepciones.Services;

namespace MauiExcepciones.Pages;

public partial class ExceptionsPage : ContentPage
{
    public ExceptionsPage() => InitializeComponent();

    // ArgumentNullException
    async void OnArgNullClicked(object sender, EventArgs e)
    {
        try
        {
            string? nombre = null;

            if (nombre is null)
                throw new ArgumentNullException(nameof(nombre), "El nombre es requerido.");

            _ = nombre.Length;
        }
        catch (Exception ex)
        {
            LogService.WriteLine($"[ArgNull] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Demo", ExceptionMapper.ToUserMessage(ex), "OK");
        }
        finally
        {
            ArgNullLabel.Text = "Usá validaciones tempranas para evitar valores nulos.";
        }
    }

    // ArgumentOutOfRangeException
    async void OnAoorClicked(object sender, EventArgs e)
    {
        try
        {
            int edad = -3;

            if (edad < 0 || edad > 120)
                throw new ArgumentOutOfRangeException(nameof(edad), "La edad debe estar entre 0 y 120.");

            _ = edad;
        }
        catch (Exception ex)
        {
            LogService.WriteLine($"[Aoor] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Demo", ExceptionMapper.ToUserMessage(ex), "OK");
        }
        finally
        {
            AoorLabel.Text = "Validá rangos antes de usar valores en lógica.";
        }
    }

    // InvalidOperationException
    async void OnInvOpClicked(object sender, EventArgs e)
    {
        try
        {
            var cola = new Queue<int>();

            if (cola.Count == 0)
                throw new InvalidOperationException("No se puede extraer de una cola vacía.");

            _ = cola.Dequeue();
        }
        catch (Exception ex)
        {
            LogService.WriteLine($"[InvalidOp] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Demo", ExceptionMapper.ToUserMessage(ex), "OK");
        }
        finally
        {
            InvOpLabel.Text = "Verificá que la colección tenga elementos antes de operar.";
        }
    }

    // FormatException
    async void OnFormatClicked(object sender, EventArgs e)
    {
        try
        {
            var texto = "12,34";

            if (!double.TryParse(texto, out var _))
                throw new FormatException("Formato numérico inválido para la cultura actual.");
        }
        catch (Exception ex)
        {
            LogService.WriteLine($"[Format] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Demo", ExceptionMapper.ToUserMessage(ex), "OK");
        }
        finally
        {
            FormatLabel.Text = "Usá TryParse para validar formatos antes de convertir.";
        }
    }

    // NullReferenceException (simulada)
    async void OnNullRefClicked(object sender, EventArgs e)
    {
        try
        {
            object? obj = null;

            if (obj is null)
                throw new NullReferenceException("Referencia nula detectada.");

            _ = obj.ToString();
        }
        catch (Exception ex)
        {
            LogService.WriteLine($"[NullRef] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Demo", ExceptionMapper.ToUserMessage(ex), "OK");
        }
        finally
        {
            NullRefLabel.Text = "Asegurate de que el objeto no sea null antes de usarlo.";
        }
    }

    // IndexOutOfRangeException (simulada)
    async void OnIndexOutClicked(object sender, EventArgs e)
    {
        try
        {
            var lista = new[] { 1, 2, 3 };

            if (lista.Length <= 3)
                throw new IndexOutOfRangeException("Índice fuera del rango válido.");

            _ = lista[3];
        }
        catch (Exception ex)
        {
            LogService.WriteLine($"[IndexOut] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Demo", ExceptionMapper.ToUserMessage(ex), "OK");
        }
        finally
        {
            IndexOutLabel.Text = "Verificá que el índice esté dentro del rango del array.";
        }
    }
}