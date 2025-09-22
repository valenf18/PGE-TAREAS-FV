namespace MauiExcepciones.Services;

public sealed class FakeAuthService
{
    // Simula un “endpoint” remoto con delay y fallas controladas
    public async Task<bool> LoginAsync(string user, string pass, TimeSpan timeout, CancellationToken externalCt = default)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(externalCt);
        cts.CancelAfter(timeout);

        // Reglas de demo:
        // - user == "timeout" → demora mucho hasta caer en TaskCanceledException
        // - user == "net"     → simular HttpRequestException
        // - resto: válido si pass == "1234"
        try
        {
            if (user == "timeout")
                await Task.Delay(TimeSpan.FromSeconds(10), cts.Token);
            else
                await Task.Delay(TimeSpan.FromSeconds(1.2), cts.Token);

            if (user == "net")
                throw new HttpRequestException("Simulada caída de red/servidor.");

            return pass == "1234";
        }
        catch (OperationCanceledException oce) when (cts.IsCancellationRequested)
        {
            // MAUI suele surfacear TaskCanceledException por timeout
            throw new TaskCanceledException("Timeout simulado", oce);
        }
    }
}
