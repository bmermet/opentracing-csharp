namespace OpenTracing.NullTracer
{
    public class NullScope : IScope
    {
        public static NullScope Instance = new NullScope();

        public ISpan Span => NullSpan.Instance;

        public void Dispose()
        {
        }
    }
}
