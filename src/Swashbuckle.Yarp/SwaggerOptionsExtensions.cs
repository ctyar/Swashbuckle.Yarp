using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerOptionsExtensions
{
    /// <summary>
    /// Enables Swagger to rewrite all of its paths whether called from YARP or directly.
    /// The prefix will be determined using the X-Forwarded-Prefix header.
    /// </summary>
    /// <param name="options"></param>
    public static void AddYarp(this SwaggerOptions options)
    {
        options.PreSerializeFilters.Add((document, request) =>
        {
            if (!request.Headers.TryGetValue("X-Forwarded-Prefix", out var headerValue))
            {
                return;
            }

            var prefix = headerValue.FirstOrDefault();

            if (prefix is null)
            {
                return;
            }

            if (!prefix.StartsWith("/"))
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

            if (!prefix.StartsWith("/"))
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
