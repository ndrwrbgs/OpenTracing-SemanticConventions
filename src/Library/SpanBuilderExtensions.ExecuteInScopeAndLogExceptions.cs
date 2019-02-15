using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTracing.Contrib.SemanticConventions
{
    using JetBrains.Annotations;

    using OpenTracing.Util;

    [PublicAPI]
    public static partial class SpanBuilderExtensions
    {
        public static T ExecuteInScopeAndLogExceptions<T>(
            this ISpanBuilder spanBuilder,
            Func<T> action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                return scope.Span.LogExceptions(action);
            }
        }

        public static async Task<T> ExecuteInScopeAndLogExceptionsAsync<T>(
            this ISpanBuilder spanBuilder,
            Func<Task<T>> action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                return await scope.Span.LogExceptions(action);
            }
        }

        public static void ExecuteInScopeAndLogExceptions(
            this ISpanBuilder spanBuilder,
            Action action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                scope.Span.LogExceptions(action);
            }
        }

        public static async Task ExecuteInScopeAndLogExceptionsAsync(
            this ISpanBuilder spanBuilder,
            Func<Task> action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                await scope.Span.LogExceptions(action);
            }
        }
    }
}
