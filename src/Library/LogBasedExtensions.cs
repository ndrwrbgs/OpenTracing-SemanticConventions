using System.Linq;

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
                    // If the ITracer needs these as a dictionary, it's just as effective for it to construct the dictionary as for us to
                    new []
                    {
                        new KeyValuePair<string, object>(KnownLogFieldNames.Event, KnownLogFieldValues.Error),
                        new KeyValuePair<string, object>(KnownLogFieldNames.Error.Kind, exception.GetType().Name),
                        new KeyValuePair<string, object>(KnownLogFieldNames.Error.Object, exception),
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
                    new[]
                    {
                        new KeyValuePair<string, object>(KnownLogFieldNames.Message, message)
                    });
        }
#endif

        /// <summary>
        /// Logs a formatable message in a structured-logging supported way (the format items are kept separate from the format)
        /// </summary>
        [StringFormatMethod("messageFormat")]
        public static ISpan LogMessage(
            [NotNull] this ISpan span,
            string messageFormat,
            [NotNull] params object[] parameters)
        {
            var logFields = new KeyValuePair<string, object>[parameters.Length + 1];
            logFields[0] = new KeyValuePair<string, object>(MessageFormatKey, messageFormat);
            for (int i = 0; i < parameters.Length; i++)
            {
                string key;
                if (i < SavedMessageParameterKeyCount)
                {
                    key = SavedMessageParameterKeys[i];
                }
                else
                {
                    key = MessageParameterKeyPrefix + i;
                }
                logFields[i + 1] = new KeyValuePair<string, object>(key, parameters[i]);
            }

            return span
                .Log(logFields);
        }

        private static readonly string MessageFormatKey = "message.format";

        /// <summary>
        /// Should be appended with the integer number of the parameter
        /// </summary>
        private static readonly string MessageParameterKeyPrefix = "message.";

        private static readonly int SavedMessageParameterKeyCount = 30;

        private static readonly string[] SavedMessageParameterKeys = Enumerable.Range(0, SavedMessageParameterKeyCount)
            .Select(index => MessageParameterKeyPrefix + index)
            .ToArray();
    }
}