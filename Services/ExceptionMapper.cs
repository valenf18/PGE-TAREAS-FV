namespace MauiExcepciones.Services;

public static class ExceptionMapper
{
    public static string ToUserMessage(Exception ex)
    {
        return ex switch
        {
            DivideByZeroException => "No se puede dividir por cero.",
            FormatException => "Ingresá números válidos (usa punto decimal si corresponde).",
            FileNotFoundException => "El archivo no existe.",
            UnauthorizedAccessException => "La app no tiene permisos para acceder al archivo.",
            IOException => "Error de entrada/salida al manejar el archivo.",
            HttpRequestException => "Problema de conexión con el servicio. Intentá nuevamente.",
            TaskCanceledException => "La operación tardó demasiado (timeout).",
            ArgumentNullException => "Falta un dato requerido.",
            ArgumentOutOfRangeException => "El valor está fuera del rango permitido.",
            InvalidOperationException => "La operación no es válida en el estado actual.",
            _ => "Ocurrió un error inesperado. Volvé a intentar."
        };
    }
}
