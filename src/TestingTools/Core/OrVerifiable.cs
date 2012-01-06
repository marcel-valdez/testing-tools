using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingTools.Core
{
    internal class VerifiableOr<T> : IVerifiable<T>
    {
        IVerifiable<T> mFirstPredicament;
        IVerifiable<T> mSecondPredicament;
        public VerifiableOr(IVerifiable<T> firstPredicament, IVerifiable<T> secondPredicament)
        {
            this.mFirstPredicament = firstPredicament;
            this.mSecondPredicament = secondPredicament;
        }

        #region IVerifiable<T> Members
        public void Now()
        {
            Exception ex = null;
            try
            {
                this.mFirstPredicament.Now();
            }
            catch (Exception x)
            {
                ex = x;
            }

            if (ex == null)
            {
                this.mSecondPredicament.Now();
            }
            else
            {
                throw ex;
            }
        }
        #endregion

        #region IAssertion<T> Members
        public T Target
        {
            get
            {
                return mFirstPredicament.Target;   
            }
        }
        #endregion
    }
}
