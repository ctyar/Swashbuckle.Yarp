using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Swashbuckle.Yarp.Tests;

public class SwashbuckleYarpTests
{
    [Fact]
    public async Task AddYarpDirectTest()
    {
        var expected =
#if NET10_0_OR_GREATER
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { },
              "tags": [
                {
                  "name": "Swashbuckle.Yarp.Tests"
                }
              ]
            }
            """;
#else
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { }
            }
            """;
#endif
        expected = expected.Replace("\r\n", "\n");
        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseSwagger(o =>
        {
            o.AddYarp("/api1");
        });
        app.UseSwaggerUI();

        app.MapGet("todos", () => Results.Ok());
        app.MapControllers();
        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("swagger/v1/swagger.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddYarpProxyTest()
    {
        var expected =
#if NET10_0_OR_GREATER
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/api1/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { },
              "tags": [
                {
                  "name": "Swashbuckle.Yarp.Tests"
                }
              ]
            }
            """;
#else
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/api1/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { }
            }
            """;
#endif
        expected = expected.Replace("\r\n", "\n");
        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseSwagger(o =>
        {
            o.AddYarp("/api1");
        });
        app.UseSwaggerUI();

        app.MapGet("todos", () => Results.Ok());
        app.MapControllers();
        app.Start();
        var client = app.GetTestClient();
        client.DefaultRequestHeaders.Add("X-Forwarded-Host", "https://example.com/api1");

        var actual = await client.GetStringAsync("swagger/v1/swagger.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddYarpWithForwardedPrefixDirectTest()
    {
        var expected =
#if NET10_0_OR_GREATER
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { },
              "tags": [
                {
                  "name": "Swashbuckle.Yarp.Tests"
                }
              ]
            }
            """;
#else
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { }
            }
            """;
#endif
        expected = expected.Replace("\r\n", "\n");
        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseSwagger(o =>
        {
            o.AddYarpWithForwardedPrefix();
        });
        app.UseSwaggerUI();

        app.MapGet("todos", () => Results.Ok());
        app.MapControllers();
        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("swagger/v1/swagger.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddYarpWithForwardedPrefixProxyTest()
    {
        var expected =
#if NET10_0_OR_GREATER
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/api1/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { },
              "tags": [
                {
                  "name": "Swashbuckle.Yarp.Tests"
                }
              ]
            }
            """;
#else
            """
            {
              "openapi": "3.0.4",
              "info": {
                "title": "Swashbuckle.Yarp.Tests",
                "version": "1.0"
              },
              "paths": {
                "/api1/todos": {
                  "get": {
                    "tags": [
                      "Swashbuckle.Yarp.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK"
                      }
                    }
                  }
                }
              },
              "components": { }
            }
            """;
#endif
        expected = expected.Replace("\r\n", "\n");
        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseSwagger(o =>
        {
            o.AddYarpWithForwardedPrefix();
        });
        app.UseSwaggerUI();

        app.MapGet("todos", () => Results.Ok());
        app.MapControllers();
        app.Start();
        var client = app.GetTestClient();
        client.DefaultRequestHeaders.Add("X-Forwarded-Prefix", "api1");

        var actual = await client.GetStringAsync("swagger/v1/swagger.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }
}
