using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodePulse.API.Filters;
public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParameters = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile))
            .ToList();

        if (!fileParameters.Any())
            return;

        operation.RequestBody = new OpenApiRequestBody
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = context.MethodInfo.GetParameters()
                            .ToDictionary(
                                p => p.Name!,
                                p => p.ParameterType == typeof(IFormFile)
                                    ? new OpenApiSchema { Type = "string", Format = "binary" }
                                    : new OpenApiSchema { Type = "string" }
                            ),
                        Required = fileParameters.Select(p => p.Name!).ToHashSet()
                    }
                }
            }
        };
    }
}