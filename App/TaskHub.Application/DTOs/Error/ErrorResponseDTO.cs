namespace TaskHub.Application.DTOs.Error;

public class ErrorResponseDTO
{
    public int StatusCode { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Causa { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}