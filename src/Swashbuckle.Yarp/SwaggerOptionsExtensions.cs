using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerOptionsExtensions
{
    /// <summary>
    /// Enables Swagger to rewrite all of its paths whether called from YARP or directly.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="prefix">The prefix to add to all paths if Swagger is being called through YARP.</param>
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
