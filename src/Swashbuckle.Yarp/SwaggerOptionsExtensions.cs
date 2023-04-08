using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerOptionsExtensions
{
    /// <summary>
    /// Set the clientId and scopes for the authorizatonCode flow with proof Key for Code Exchange.
    /// </summary>
    /// <param name="options"></param>
    public static void AddYarp(this SwaggerOptions options)
    {
        options.PreSerializeFilters.Add((document, request) =>
        {
            var prefix = request.Headers["X-Forwarded-Prefix"].FirstOrDefault();

            if (prefix is null)
            {
                return;
            }

            var openApiPaths = new OpenApiPaths();
            foreach (var path in document.Paths)
            {
                openApiPaths.Add(prefix + path.Key, path.Value);
            }

            document.Paths = openApiPaths;
        });
    }
}
