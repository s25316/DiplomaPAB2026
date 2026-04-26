using Scalar.AspNetCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace RADON.API;

public class Program
{
    public static string JsonPropertyName(string name) => char.ToLowerInvariant(name[0]) + name.Substring(1);
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi(options =>
        {
            options.AddOperationTransformer((operation, context, cancellationToken) =>
            {
                foreach (var apiParam in operation.Parameters)
                {
                    var propMetadata = context.Description.ParameterDescriptions
                        .FirstOrDefault(p => string.Equals(p.Name, apiParam.Name, StringComparison.OrdinalIgnoreCase));

                    var containerType = propMetadata?.ModelMetadata?.ContainerType;
                    if (containerType != null)
                    {
                        var prop = containerType.GetProperty(propMetadata!.ModelMetadata.PropertyName!);
                        if (prop != null)
                        {
                            var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                            var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();

                            string? description = displayAttr?.GetName() ?? descAttr?.Description;

                            if (!string.IsNullOrEmpty(description))
                            {
                                apiParam.Description = description;
                            }
                        }
                    }
                }
                return Task.CompletedTask;
            });
            options.AddSchemaTransformer((schema, context, cancellationToken) =>
            {
                var type = context.JsonTypeInfo.Type;
                if (schema.Properties == null) return Task.CompletedTask;

                foreach (var property in type.GetProperties())
                {
                    var displayAttr = property.GetCustomAttribute<DisplayAttribute>();

                    var descAttr = property.GetCustomAttribute<DescriptionAttribute>();

                    string? descriptionText = displayAttr?.GetName() ?? descAttr?.Description;

                    if (!string.IsNullOrEmpty(descriptionText))
                    {
                        var jsonPropertyName = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name
                                               ?? JsonPropertyName(property.Name);

                        if (schema.Properties.TryGetValue(jsonPropertyName, out var openApiProperty))
                        {
                            openApiProperty.Description = descriptionText;
                        }
                    }
                }
                return Task.CompletedTask;
            });
        });

        var app = builder.Build();
        app.UseExceptionHandler();

        app.MapOpenApi();
        app.MapScalarApiReference();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
