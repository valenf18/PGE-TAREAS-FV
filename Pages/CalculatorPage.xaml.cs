using MauiExcepciones.Services;

namespace MauiExcepciones.Pages;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage() => InitializeComponent();

    async void OnCalcularClicked(object sender, EventArgs e)
    {
        try
        {
            // VALIDACIÓN: si TryParse falla, lanzamos FormatException explícita
            if (!double.TryParse(Num1Entry.Text, out var a) ||
                !double.TryParse(Num2Entry.Text, out var b))
                throw new FormatException("Entradas no numéricas.");

            var op = OpPicker.SelectedIndex;
            if (op < 0) throw new InvalidOperationException("Seleccioná una operación.");

            double r = op switch
            {
                0 => a + b,
                1 => a - b,
                2 => a * b,
                3 => b == 0 ? throw new DivideByZeroException() : a / b,
                4 => a < 0 ? throw new ArgumentOutOfRangeException(nameof(a), "No se puede calcular raíz de número negativo.") : Math.Sqrt(a),
                5 => (a == 0 && b <= 0) ? throw new ArgumentOutOfRangeException(nameof(b), "Potencia indefinida para base cero y exponente no positivo.") : Math.Pow(a, b),
                _ => throw new InvalidOperationException("Operación desconocida.")
            };

            ResultLabel.Text = $"Resultado: {r}";
        }
        catch (Exception ex)
        {
            // Log técnico + mensaje seguro
            LogService.WriteLine($"[Calc] {ex.GetType().Name} - {ex.Message}");
            await DisplayAlert("Atención", ExceptionMapper.ToUserMessage(ex), "OK");
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
