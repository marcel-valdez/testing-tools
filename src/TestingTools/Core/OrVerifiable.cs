using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingTools.Core
{
    internal class VerifiableOr<T> : IVerifiable<T>
    {
        IVerifiable<T> mLeftPredicament;
        IVerifiable<T> mRightPredicament;
        public VerifiableOr(IVerifiable<T> leftPredicament, IVerifiable<T> rightPredicament)
        {
            this.mLeftPredicament = leftPredicament;
            this.mRightPredicament = rightPredicament;
        }

        #region IVerifiable<T> Members
        public void Now()
        {
            try
            {
                this.mLeftPredicament.Now();
            }
            catch (Exception)
            {
                try
                {
                    this.mRightPredicament.Now();
                }
                catch (Exception)
                {                    
                    throw;
                }
            }
        }
        #endregion

        #region IAssertion<T> Members
        public T Target
        {
            get
            {
                return mRightPredicament.Target;
            }
        }
        #endregion
    }
}
