namespace OpenTracing.Contrib.SemanticConventions
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for managing a <see cref="IScope"/>. Methods that handle creation of the <see cref="IScope"/> extend <see cref="ISpanBuilder"/>.
    /// 
    /// Starts an active span and captures the result state (e.g. Exceptions/success/fail)
    ///
    /// TODO: For TryX type methods, we should expose a way to signal that the result means fail (is fail a semantics concept or only error?)
    /// </summary>
    [PublicAPI]
    public static class ScopeExtensions
    {
        public static async Task ExecuteInScopeAsync(
            [NotNull] this ISpanBuilder spanBuilder,
            [NotNull] Func<Task> action)
        {
            using (IScope scope = spanBuilder.StartActive(true))
            {
                try
                {
                    await action().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    scope.Span.LogError(e);
                    throw;
                }
            }
        }

        public static async Task ExecuteInScopeAsync(
            [NotNull] this ISpanBuilder spanBuilder,
            [NotNull] Func<ISpan, Task> action)
        {
            throw new NotImplementedException();
        }

        public static async Task<T> ExecuteInScopeAsync<T>(
            [NotNull] this ISpanBuilder spanBuilder,
            [NotNull] Func<Task<T>> action)
        {
            throw new NotImplementedException();
        }

        public static async Task<T> ExecuteInScopeAsync<T>(
            [NotNull] this ISpanBuilder spanBuilder,
            // TODO: Parity, add overloads
            [NotNull] Func<ISpan, Task<T>> action)
        {
            using (IScope scope = spanBuilder.StartActive(true))
            {
                try
                {
                    return await action(scope.Span).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    scope.Span.LogError(e);
                    throw;
                }
            }
        }
    }
}