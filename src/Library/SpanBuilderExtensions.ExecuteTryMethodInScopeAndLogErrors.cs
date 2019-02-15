using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTracing.Contrib.SemanticConventions
{
    using JetBrains.Annotations;

    using OpenTracing.Util;
    
    public static partial class SpanBuilderExtensions
    {
        public delegate T TryMethodDelegate<out T>(out bool failed);
        public delegate void TryMethodDelegate(out bool failed);
        public delegate Task<T> AsyncTryMethodDelegate<T>(out bool failed);
        public delegate Task AsyncTryMethodDelegate(out bool failed);

        public static T ExecuteTryMethodInScopeAndLogFailures<T>(
            this ISpanBuilder spanBuilder,
            TryMethodDelegate<T> action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                var result = action(out bool failed);
                if (failed)
                {
                    scope.Span.SetTag(KnownTagNames.Error, true);
                }

                return result;
            }
        }

        public static async Task<T> ExecuteTryMethodInScopeAndLogFailuresAsync<T>(
            this ISpanBuilder spanBuilder,
            AsyncTryMethodDelegate<T> action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                var result = await action(out bool failed);
                if (failed)
                {
                    scope.Span.SetTag(KnownTagNames.Error, true);
                }

                return result;
            }
        }

        public static void ExecuteTryMethodInScopeAndLogFailures(
            this ISpanBuilder spanBuilder,
            TryMethodDelegate action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                action(out bool failed);
                if (failed)
                {
                    scope.Span.SetTag(KnownTagNames.Error, true);
                }
            }
        }

        public static async Task ExecuteTryMethodInScopeAndLogFailuresAsync(
            this ISpanBuilder spanBuilder,
            AsyncTryMethodDelegate action)
        {
            using (IScope scope = spanBuilder.StartActive())
            {
                await action(out bool failed);
                if (failed)
                {
                    scope.Span.SetTag(KnownTagNames.Error, true);
                }
            }
        }
    }
}
