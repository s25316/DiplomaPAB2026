namespace Diploma.Database.Models.Shared;

public class Error
{
    public Guid ErrorId { get; set; }
    public string Message { get; set; } = null!;
    public string? StackTrace { get; set; }
    public string? TraceIdentifier { get; set; }
    public string? ExceptionType { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}