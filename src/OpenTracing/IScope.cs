using System;

namespace OpenTracing
{
    /// <summary>
    /// A <see cref="IScope"/> formalizes the activation and deactivation of a <see cref="ISpan"/>, usually from a CPU standpoint.
    ///
    /// Many times a <see cref="ISpan"/> will be extant (in that <see cref="ISpan.Finish()"/> has not been called) despite being in a
    /// non-runnable state from a CPU/scheduler standpoint. For instance, a <see cref="ISpan"/> representing the client side of an
    /// RPC will be unfinished but blocked on IO while the RPC is still outstanding. A <see cref="IScope"/> defines when a given
    /// <see cref="ISpan"/> is scheduled and on the path.
    /// <see cref="IScope.Dispose()"/> mark the end of the active period for the current thread and <see cref="IScope"/>,
    /// updating the <see cref="IScopeManager.Active"/> in the process.
    /// NOTE: Calling <see cref="IScope.Dispose()"/> more than once on a single <see cref="IScope"/> instance leads to undefined
    /// behavior.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IScope : IDisposable
    {
        /// <summary>
        /// Returns the <see cref="ISpan"/> that's been scoped by this<see cref="IScope"/>
        /// </summary>
        ISpan Span { get; }
    }
}
