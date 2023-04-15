# Swashbuckle Yarp

A package to simplify using Swashbuckle Swagger behind Yarp.

## Usage

1. Add a route for each of your APIs like this:
```json
"route1": {
  "ClusterId": "cluster1",
  "Match": {
    "Path": "/api1/{**catch-all}"
  },
  "Transforms": [
    {
      "PathRemovePrefix": "/api1"
    }
  ]
}
```
With this config calling `/api1` of your YARP will redirect to your RESTful API.

2. Add `AddYarp()` with the same prefix in your RESTful API:
```csharp
app.UseSwagger(o =>
{
    o.AddYarp("api1");
});
```

You can check the [samples](/samples) directory for a complete working sample.

## Build
[Install](https://get.dot.net) the [required](global.json) .NET SDK.

Run:
```
$ dotnet build
```
