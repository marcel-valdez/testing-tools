using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingTools.Core
{
    internal class AndConnector<TRight, TLeft> : IAssertion<TRight>, IVerifiable<TRight>
    {
        private IVerifiable<TLeft> mLeftPredicament;
        private TRight mTarget;
        public AndConnector(IVerifiable<TLeft> leftPredicament, TRight rightTarget)
        {
            this.mLeftPredicament = leftPredicament;
            this.mTarget = rightTarget;
        }

        #region IVerifiable<T> Members

        void IVerifiable<TRight>.Now()
        {
            this.mLeftPredicament.Now();
        }

        #endregion

        #region IAssertion<T> Members

        public TRight Target
        {
            get
            {
                return this.mTarget;
            }
        }
        #endregion
    }
}
