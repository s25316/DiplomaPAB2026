using Base.Pipelines.Operations;
using GUS.REGON.Errors;

namespace GUS.REGON.Extensions;

internal static class RegonExceptionExtensions
{
    public static Exception MapToException(this OperationError item)
    {
        return new RegonException.Other(item.ToString());
    }
}