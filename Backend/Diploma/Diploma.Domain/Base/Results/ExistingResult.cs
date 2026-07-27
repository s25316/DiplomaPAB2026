namespace Diploma.Domain.Base.Results;

public sealed class ExistingResult
{
    public bool IsExist { get; private set; }
    public bool IsNotFound => !IsExist;


    private ExistingResult(bool success) => IsExist = success;


    public static readonly ExistingResult Exist = new(true);
    public static readonly ExistingResult NotFound = new(false);
}