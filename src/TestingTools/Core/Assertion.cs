namespace TestingTools.Core
{
    internal class Assertion<T> : IAssertion<T>
    {
      private readonly T mTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assertion&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        internal Assertion(T target)
        {
            this.mTarget = target;
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        public T Target
        {
            get
            {
                return this.mTarget;
            }
        }
    }
}