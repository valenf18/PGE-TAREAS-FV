using MauiExcepciones.Services;
using System.Net.Http;

namespace MauiExcepciones.Pages;

public partial class LoginPage : ContentPage
{
    readonly FakeAuthService _service = new();
    int _fallosSeguidos = 0;

    public LoginPage() => InitializeComponent();

    async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            Busy.IsVisible = Busy.IsRunning = true;
            LoginBtn.IsEnabled = false;

            // Validación de entrada
            var user = UserEntry.Text?.Trim() ?? string.Empty;
            var pass = PassEntry.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentNullException(nameof(user), "Usuario vacío.");
            if (string.IsNullOrWhiteSpace(pass))
                throw new ArgumentNullException(nameof(pass), "Clave vacía.");
            if (user.Length < 3 || user.Length > 20)
                throw new ArgumentOutOfRangeException(nameof(user), "Usuario fuera de longitud (3–20).");
            if (pass.Length < 3 || pass.Length > 20)
                throw new ArgumentOutOfRangeException(nameof(pass), "Clave fuera de longitud (3–20).");

            // Simulación de login con timeout
            var ok = await _service.LoginAsync(user, pass, TimeSpan.FromSeconds(2.5));

            if (ok)
            {
                _fallosSeguidos = 0;
                ResultLabel.Text = "Resultado: acceso concedido";
            }
            else
            {
                _fallosSeguidos++;
                ResultLabel.Text = "Resultado: credenciales inválidas";
                LogService.WriteLine($"[Login] {user} | {DateTime.Now:g} | Falla: credencial");

                if (_fallosSeguidos >= 3)
                {
                    await DisplayAlert("Bloqueo", "Demasiados intentos fallidos. Espera 15 segundos.", "OK");
                    await Task.Delay(15000);
                    _fallosSeguidos = 0;
                }
            }
        }
        catch (Exception ex) when (
            ex is HttpRequestException ||
            ex is TaskCanceledException ||
            ex is ArgumentNullException ||
            ex is ArgumentOutOfRangeException)
        {
            _fallosSeguidos++;
            string tipo = ex switch
            {
                HttpRequestException => "red",
                TaskCanceledException => "timeout",
                ArgumentNullException => "validación",
                ArgumentOutOfRangeException => "validación",
                _ => "desconocido"
            };

            LogService.WriteLine($"[Login] {UserEntry.Text} | {DateTime.Now:g} | Falla: {tipo} - {ex.Message}");

            string mensaje = ex switch
            {
                TaskCanceledException => "Esperá unos segundos e intentá de nuevo.",
                HttpRequestException => "No pudimos comunicarnos con el servicio.",
                _ => ExceptionMapper.ToUserMessage(ex)
            };

            await DisplayAlert("Atención", mensaje, "OK");
            ResultLabel.Text = "Resultado: error de comunicación o validación";

            if (_fallosSeguidos >= 3)
            {
                await DisplayAlert("Bloqueo", "Demasiados intentos fallidos. Espera 15 segundos.", "OK");
                await Task.Delay(15000);
                _fallosSeguidos = 0;
            }
        }
        finally
        {
            Busy.IsVisible = Busy.IsRunning = false;
            LoginBtn.IsEnabled = true;
        }
    }
}