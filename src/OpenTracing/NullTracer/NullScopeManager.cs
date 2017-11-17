namespace OpenTracing.NullTracer
{
    public class NullScopeManager : IScopeManager
    {
        public static NullScopeManager Instance = new NullScopeManager();

        public IScope Active => null;

        public IScope Activate(ISpan span)
        {
            return NullScope.Instance;
        }

        public IScope Activate(ISpan span, bool finishSpanOnClose)
        {
            return NullScope.Instance;
        }
    }
}
