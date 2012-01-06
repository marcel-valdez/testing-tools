namespace TestingTools.Core
{
    using System;

    internal class Verifiable<T> : IVerifiable<T>
    {
        private IAssertion<T> mAssertion;
        private Action<T> mPredicate;

        internal Verifiable(IAssertion<T> assertion, Action<T> predicate)
        {
            this.mAssertion = assertion;
            this.mPredicate = predicate;
        }

        /// <summary>
        /// Ejecuta todos los predicados encadenados, empezando por el primero, hasta el último.
        /// </summary>
        public void Now()
        {
            if (this.mAssertion is IVerifiable<T>)
            {
                (this.mAssertion as IVerifiable<T>).Now();
            }

            this.mPredicate(this.mAssertion.Target);
        }



        #region IAssertion<T> Members
        /// <summary>
        /// Gets the target being tested.
        /// </summary>
        public T Target
        {
            get
            {
                return this.mAssertion.Target;
            }
        }
        #endregion
    }
}
