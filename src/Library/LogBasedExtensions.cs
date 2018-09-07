namespace OpenTracing.Contrib.SemanticConventions
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

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
                        [KnownLogFieldNames.Error.Object] = exception
                    });
        }

        public static ISpan LogMessage(
            [NotNull] this ISpan span,
            /*TODO: FormattableString*/
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