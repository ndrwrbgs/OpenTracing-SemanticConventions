namespace OpenTracing.Contrib.SemanticConventions
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    using OpenTracing.Util;

    /// <summary>
    /// Extensions for managing a <see cref="IScope"/>. Methods that handle creation of the <see cref="IScope"/> extend <see cref="ISpanBuilder"/>.
    /// </summary>
    [PublicAPI]
    public static class SpanExtensions
    {
        public static async Task LogExceptionsAsync(
            this ISpan span,
            Func<Task> action)
        {
            try
            {
                await action().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                span.LogError(e);
                throw;
            }
        }
        public static void LogExceptions(
            this ISpan span,
            Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                span.LogError(e);
                throw;
            }
        }
        public static async Task<T> LogExceptionsAsync<T>(
            this ISpan span,
            Func<Task<T>> action)
        {
            try
            {
                return await action().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                span.LogError(e);
                throw;
            }
        }
        public static T LogExceptions<T>(
            this ISpan span,
            Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                span.LogError(e);
                throw;
            }
        }
    }
}