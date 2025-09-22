using MauiExcepciones.Services;

namespace MauiExcepciones.Pages;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage() => InitializeComponent();

    async void OnCalcularClicked(object sender, EventArgs e)
    {
        try
        {
            // VALIDACI�N: si TryParse falla, lanzamos FormatException expl�cita
            if (!double.TryParse(Num1Entry.Text, out var a) ||
                !double.TryParse(Num2Entry.Text, out var b))
                throw new FormatException("Entradas no num�ricas.");

            var op = OpPicker.SelectedIndex;
            if (op < 0) throw new InvalidOperationException("Seleccion� una operaci�n.");

            double r = op switch
            {
                0 => a + b,
                1 => a - b,
                2 => a * b,
                3 => b == 0 ? throw new DivideByZeroException() : a / b,
                4 => a < 0 ? throw new ArgumentOutOfRangeException(nameof(a), "No se puede calcular ra�z de n�mero negativo.") : Math.Sqrt(a),
                5 => (a == 0 && b <= 0) ? throw new ArgumentOutOfRangeException(nameof(b), "Potencia indefinida para base cero y exponente no positivo.") : Math.Pow(a, b),
                _ => throw new InvalidOperationException("Operaci�n desconocida.")
            };

            ResultLabel.Text = $"Resultado: {r}";
        }
        catch (Exception ex)
        {
            // Log t�cnico + mensaje seguro
            LogService.WriteLine($"[Calc] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Atenci�n", ExceptionMapper.ToUserMessage(ex), "OK");
        }
        finally
        {
            // Ejemplo de finally: reestablecer UI
            (sender as Button)!.IsEnabled = true;
        }
    }

    async void OnVerLogClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Log", LogService.ReadAll(), "OK");
    }
}
