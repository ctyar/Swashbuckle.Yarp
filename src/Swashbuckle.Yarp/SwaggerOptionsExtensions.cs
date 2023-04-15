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
    public static void AddYarp(this SwaggerOptions options, string prefix)
    {
        options.PreSerializeFilters.Add((document, request) =>
        {
            var isForwarded = request.Headers["X-Forwarded-Host"].Any();

            if (!isForwarded)
            {
                return;
            }

            if (!prefix.StartsWith('/'))
            {
                prefix = '/' + prefix;
            }

            prefix = prefix.TrimEnd('/');

            var openApiPaths = new OpenApiPaths();
            foreach (var path in document.Paths)
            {
                openApiPaths.Add(prefix + path.Key, path.Value);
            }

            document.Paths = openApiPaths;
        });
    }
}
