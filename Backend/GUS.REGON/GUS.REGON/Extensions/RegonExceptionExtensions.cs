using Base.Pipelines.Operations;
using GUS.REGON.Errors;
using static GUS.REGON.Errors.RegonException;

namespace GUS.REGON.Extensions;

internal static class RegonExceptionExtensions
{
    public static RegonException MapToRegonException(this OperationError item)
    {
        return new Other(item.ToString());
    }
}