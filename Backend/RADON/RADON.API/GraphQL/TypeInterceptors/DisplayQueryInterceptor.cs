using HotChocolate.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors.Definitions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RADON.API.GraphQL.TypeInterceptors;

public class DisplayQueryInterceptor : TypeInterceptor
{
    public override void OnBeforeCompleteName(ITypeCompletionContext context, DefinitionBase definition)
    {
        if (definition is ObjectTypeDefinition objDef)
        {
            bool isRootQuery = objDef.Name.Equals(OperationTypeNames.Query);

            foreach (var field in objDef.Fields)
            {
                var display = field.Member?.GetCustomAttribute<DisplayAttribute>();

                if (display != null)
                {
                    field.Description = display.GetName();
                }
            }
        }
    }
}