
namespace OpenTracing
{
    /// <summary>
    /// The <see cref="IScopeManager"/> interface abstracts both the activation of <see cref="ISpan"/> instances (via
    /// <see cref="Activate(ISpan, bool)"/> and access to an active <see cref="ISpan"/>/<see cref="IScope"/>
    /// (via <see cref="Active"/>).
    /// <see cref="IScope"/>
    /// <see cref="ITracer.ScopeManager"/>
    /// </summary>
    public interface IScopeManager
    {
        /// <summary>
        /// Make a <see cref="ISpan"/> instance active.
        /// </summary>
        /// <param name="span">The <see cref="ISpan"/> that should become the <see cref="Active"/>
        /// <returns>A <see cref="IScope"/> instance to control the end of the active period for the <see cref="ISpan"/>. It is a
        /// programming error to neglect to call <see cref="IScope.Dispose()"/> on the returned instance.</returns>
        IScope Activate(ISpan span);

        /// <summary>
        /// Make a <see cref="ISpan"/> instance active.
        /// </summary>
        /// <param name="span">The <see cref="ISpan"/> that should become the <see cref="Active"/>
        /// <param name="finishSpanOnClose">Whether span should automatically be finished when <see cref="IScope.Dispose()"/>is called</param>
        /// <returns>A <see cref="IScope"/> instance to control the end of the active period for the <see cref="ISpan"/>. It is a
        /// programming error to neglect to call <see cref="IScope.Dispose()"/> on the returned instance.</returns>
        IScope Activate(ISpan span, bool finishSpanOnClose);

        /// <summary>
        /// Return the currently active <see cref="IScope"/> which can be used to access the currently active
        /// <see cref="IScope.Span"/>, or null if none could be found.
        /// If there is an non null <see cref="IScope"/>, its wrapped <see cref="ISpan"/> becomes an implicit parent of any
        /// newly-created <see cref="ISpan"/> at <see cref="ISpanBuilder.StartActive()"/> time (rather than at
        /// <see cref="ITracer.BuildSpan(string)"/> time).  
        /// </summary>
        IScope Active { get; }
    }
}
