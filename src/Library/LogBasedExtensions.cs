namespace OpenTracing.Contrib.SemanticConventions
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="ISpan.Log(IEnumerable{KeyValuePair{string,object}})"/> based on <see cref="KnownLogFieldNames"/> and <see cref="KnownLogFieldValues"/>
    /// </summary>
    [PublicAPI]
    public static class LogBasedExtensions
    {
        public static ISpan LogError(
            [NotNull] this ISpan span,
            Exception exception)
        {
            span.SetTag(KnownTagNames.Error, true);
            return span
                .Log(
                    new Dictionary<string, object>(2)
                    {
                        [KnownLogFieldNames.Event] = KnownLogFieldValues.Error,
                        [KnownLogFieldNames.Error.Kind] = exception.GetType().Name,
                        [KnownLogFieldNames.Error.Object] = exception
                    });
        }
        
#if NETSTANDARD2_0
        public static ISpan LogMessage(
            [NotNull] this ISpan span,
            FormattableString formattableString)
        {
            return LogMessage(
                span,
                formattableString.Format,
                formattableString.GetArguments());
        }
#else
        public static ISpan LogMessage(
            [NotNull] this ISpan span,
            string message)
        {
            return span
                // TODO: there should be a way to pass these without the object allocation/boxing, custom struct?
                .Log(
                    new Dictionary<string, object>(1)
                    {
                        [KnownLogFieldNames.Message] = message
                    });
        }
#endif

        [StringFormatMethod("messageFormat")]
        public static ISpan LogMessage(
            [NotNull] this ISpan span,
            string messageFormat,
            [NotNull] params object[] parameters)
        {
            // TODO: Perf optimizations
            var dictionary = new Dictionary<string, object>(parameters.Length + 1);
            dictionary["message.format"] = messageFormat;
            for (int i = 0; i < parameters.Length; i++)
            {
                dictionary["message." + i] = parameters[i];
            }

            return span
                // TODO: Should accept Dictionary explicitly for the enumerator benefits
                .Log(dictionary);
        }
    }
}