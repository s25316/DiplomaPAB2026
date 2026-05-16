using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GUS.REGON.API.GraphQL.TypeInterceptors;

public class DisplayFieldsInterceptor : TypeInterceptor
{
    public override void OnBeforeCompleteName(
        ITypeCompletionContext completionContext,
        DefinitionBase definition)
    {
        if (definition is not ObjectTypeDefinition objectTypeDefinition)
            return;

        foreach (var field in objectTypeDefinition.Fields)
        {
            var displayAttr = field.Member?.GetCustomAttribute<DisplayAttribute>();

            if (displayAttr is null)
                continue;

            var description = displayAttr.GetName();

            if (string.IsNullOrEmpty(description))
                continue;

            field.Description = description;
        }
    }
}