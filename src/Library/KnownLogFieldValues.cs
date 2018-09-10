namespace OpenTracing.Contrib.SemanticConventions
{
    using JetBrains.Annotations;
    
    /// <summary>
    /// <a href="https://github.com/opentracing/specification/blob/master/semantic_conventions.md">Documentation</a>
    /// </summary>
    [PublicAPI]
    public static class KnownLogFieldValues
    {
        public const string Error = "error";
    }
}