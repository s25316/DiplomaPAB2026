using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RADON.API.GraphQL.TypeInterceptors;

public class DisplayAttributeInterceptor : TypeInterceptor
{
    public override void OnBeforeCompleteName(
        ITypeCompletionContext completionContext,
        DefinitionBase definition)
    {
        if (definition is ObjectTypeDefinition objectTypeDefinition)
        {
            foreach (var field in objectTypeDefinition.Fields)
            {
                var displayAttr = field.Member?.GetCustomAttribute<DisplayAttribute>();

                if (displayAttr != null)
                {
                    var description = displayAttr.GetName();

                    if (!string.IsNullOrEmpty(description))
                    {
                        field.Description = description;
                    }
                }
            }
        }
    }
}