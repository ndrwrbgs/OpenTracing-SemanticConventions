[![NuGet](https://img.shields.io/nuget/v/OpenTracing.Contrib.SemanticConventions.svg)](https://www.nuget.org/packages/OpenTracing.Contrib.SemanticConventions)

# OpenTracing-SemanticConventions
Extensions on top of the OpenTracing APIs, following the [OpenTracing Semantic Conventions spec](https://github.com/opentracing/specification/blob/master/semantic_conventions.md).

# Usage

After importing the library, see intellisense on `ISpan` primarily.

## Tags
SetXYZTags extensions that use the `KnownTagNames` but strongly-typed. E.g. `SetHttpTags(new Http(method: "Post", url: "example.com", statusCode: "500"))`

## Logs
LogXYZ extensions that use `KnownLogFieldNames` (and `KnownLogFieldValues` where applicable). E.g. `LogError(exception)` which sets the `event` and `error.kind`/`error.object` logs as the Conventions specify.
