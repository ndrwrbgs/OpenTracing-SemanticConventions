namespace OpenTracing.Contrib.SemanticConventions
{
    using JetBrains.Annotations;

    [PublicAPI]
    public static class KnownLogFieldNames
    {
        public const string Event = "event";
        public const string Message = "message";
        public const string Stack = "stack";
        
        [PublicAPI]
        public static class Error
        {
            public const string Object = "error.object";
            public const string Kind = "error.kind";
        }
    }
}