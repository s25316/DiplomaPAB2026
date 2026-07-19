namespace Diploma.Application.Interfaces.Security;

public interface IJwtNameIdentifierExtractor
{
    Guid Extract(string token);
}