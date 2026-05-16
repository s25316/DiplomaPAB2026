using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GUS.REGON.API.GraphQL.TypeInterceptors;

public class DisplayQueryInterceptor : TypeInterceptor
{
    public override void OnBeforeCompleteName(ITypeCompletionContext context, DefinitionBase definition)
    {
        if (definition is not ObjectTypeDefinition objDef)
            return;

        foreach (var field in objDef.Fields)
        {
            var display = field.Member?.GetCustomAttribute<DisplayAttribute>();

            if (display is null)
                continue;

            field.Description = display.GetName();
        }
    }
}