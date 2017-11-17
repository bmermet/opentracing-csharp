using System;

namespace OpenTracing
{
    /// <summary>
    /// Builds a new <see cref="ISpan" />.
    /// </summary>
    public interface ISpanBuilder
    {
        /// <summary>
        /// A shorthand for <see cref="AddReference(string, ISpanContext)" /> using <see cref="References.ChildOf"/>.
        /// </summary>
        ISpanBuilder AsChildOf(ISpan parent);

        /// <summary>
        /// A shorthand for <see cref="AddReference(string, ISpanContext)" /> using <see cref="References.ChildOf"/>.
        /// </summary>
        ISpanBuilder AsChildOf(ISpanContext parent);

        /// <summary>
        /// A shorthand for <see cref="AddReference(string, ISpanContext)" /> using <see cref="References.FollowsFrom"/>.
        /// </summary>
        ISpanBuilder FollowsFrom(ISpan parent);

        /// <summary>
        /// A shorthand for <see cref="AddReference(string, ISpanContext)" /> using <see cref="References.FollowsFrom"/>.
        /// </summary>
        ISpanBuilder FollowsFrom(ISpanContext parent);

        /// <summary>
        /// Add a reference from the span being built to a distinct (usually parent) span.
        /// May be called multiple times to represent multiple such references.
        /// </summary>
        /// <param name="referenceType">The reference type, typically one of the constants defined in <see cref="References"/>.</param>
        /// <param name="referencedContext">The <see cref="ISpanContext"/> being referenced;
        /// e.g., for a <see cref="References.ChildOf"/> referenceType, the referencedContext is the parent.</param>
        ISpanBuilder AddReference(string referenceType, ISpanContext referencedContext);

        /// <summary>
        /// Adds a tag to the span.
        /// </summary>
        /// <param name="key">If there is a pre-existing tag set for <paramref name="key"/>, it is overwritten.</param>
        /// <param name="value">The value to be stored.</param>
        /// <returns>The current <see cref="ISpan"/> instance for chaining.</returns>
        ISpanBuilder WithTag(string key, bool value);

        /// <summary>
        /// Adds a tag to the span.
        /// </summary>
        /// <param name="key">If there is a pre-existing tag set for <paramref name="key"/>, it is overwritten.</param>
        /// <param name="value">The value to be stored.</param>
        /// <returns>The current <see cref="ISpan"/> instance for chaining.</returns>
        ISpanBuilder WithTag(string key, double value);

        /// <summary>
        /// Adds a tag to the span.
        /// </summary>
        /// <param name="key">If there is a pre-existing tag set for <paramref name="key"/>, it is overwritten.</param>
        /// <param name="value">The value to be stored.</param>
        /// <returns>The current <see cref="ISpan"/> instance for chaining.</returns>
        ISpanBuilder WithTag(string key, int value);

        /// <summary>
        /// Adds a tag to the span.
        /// </summary>
        /// <param name="key">If there is a pre-existing tag set for <paramref name="key"/>, it is overwritten.</param>
        /// <param name="value">The value to be stored.</param>
        /// <returns>The current <see cref="ISpan"/> instance for chaining.</returns>
        ISpanBuilder WithTag(string key, string value);

        /// <summary>
        /// Specify a timestamp of when the span was started.
        /// </summary>
        /// <param name="startTimestamp">An explicit start timestamp for the span.</param>
        ISpanBuilder WithStartTimestamp(DateTimeOffset startTimestamp);

        /// <summary>
        /// Do not create an implicit <see cref="References.ChildOf"/> reference to the <see cref="IScopeManager.Active"/>.
        /// </summary>
        /// <returns></returns>
        ISpanBuilder IgnoreActiveSpan();

        /// <summary>
        /// Returns a newly started and activated <see cref="IScope"/>.
        /// For detailed information, see <see cref="StartActive(bool)"/>
        /// <seealso cref="IScopeManager"/>
        /// <seealso cref="IScope"/>
        /// <seealso cref="StartActive(bool)"/>
        /// </summary>
        /// <returns>A <see cref="IScope"/> already registered via the <see cref="ITracer.ScopeManager"/></returns>
        /// <example>
        /// The returned <see cref="IScope"/> implements <see cref="IDisposable"/> and can be used within a using statement.
        /// <code>
        ///     using (IScope scope = tracer.BuildSpan("...").StartActive()) {
        ///         // (Do work)
        ///         scope.Span.SetTag( ... );  // etc, etc
        ///     }
        ///     // the <see cref="ISpan"/> finishes automatically when the <see cref="IScope"/> is disposed,
        ///     // following the default behavior of <see cref="IScopeManager.Activate(ISpan, bool)"/>
        /// </code>
        /// </example>
        /// <remarks>
        /// <see cref="StartActive()"/> is a shorthand for
        /// <c> tracer.scopeManager().activate(spanBuilder.startManual()) </c>
        /// </remarks>
        IScope StartActive();

        /// <summary>
        /// Returns a newly started and activated <see cref="IScope"/>.
        /// If the <see cref="ITracer"/>'s <see cref="IScopeManager.Active"/> is not null, and
        /// no explicit references are added via <see cref="AddReference(string, ISpanContext)"/>, and
        /// <see cref="IgnoreActiveSpan()"/> is not invoked, then an inferred <see cref="References.ChildOf"/>
        /// reference is created to the <see cref="IScopeManager.Active"/>'s <see cref="ISpanContext"/> when either
        /// <see cref="StartManual"/> or <see cref="StartActive()"/> is invoked.
        /// <seealso cref="IScopeManager"/>
        /// <seealso cref="IScope"/>
        /// </summary>
        /// <param name="finishSpanOnClose">Whether the <see cref="ISpan"/> should automatically be finished when <see cref="IScope.Dispose()"/> is called </param>
        /// <returns>A <see cref="IScope"/> already registered via the <see cref="ITracer.ScopeManager"/></returns>
        /// <example>
        /// The returned <see cref="IScope"/> implements <see cref="IDisposable"/> and can be used within a using statement.
        /// <code>
        ///     using (IScope scope = tracer.BuildSpan("...").StartActive(false)) {
        ///         // (Do work)
        ///         scope.Span.SetTag( ... );  // etc, etc
        ///     }
        ///     // The <see cref="ISpan"/> does not finish automatically when the <see cref="IScope"/> is disposed as
        ///     // 'finishOnClose' is false
        /// </code>
        /// </example>
        /// <remarks>
        /// <see cref="StartActive(bool)"/> is a shorthand for
        /// <c> tracer.scopeManager().activate(spanBuilder.startManual(), finishSpanOnClose) </c>
        /// </remarks>
        IScope StartActive(bool finishSpanOnClose);

        /// <summary>
        /// Like <see cref="StartActive()"/>, but the returned <see cref="ISpan"/> has not been registered via the
        /// <see cref="IScopeManager"/>.
        /// <seealso cref=StartActive()"/>
        /// </summary>
        /// <returns>The newly-started Span instance, which has *not* been automatically registered
        /// via the <see cref="IScopeManager"/></returns>
        ISpan StartManual();
    }
}