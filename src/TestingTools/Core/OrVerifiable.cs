using System;

namespace TestingTools.Core
{
    internal class VerifiableOr<T> : IVerifiable<T>, ICommandable
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

        #region ICommandable Members

        public void Command(object command)
        {
            if (this.mLeftPredicament is ICommandable)
            {
                (this.mLeftPredicament as ICommandable).Command(command);
            }

            if (this.mRightPredicament is ICommandable)
            {

                (this.mRightPredicament as ICommandable).Command(command);
            }
        }
        #endregion
    }
}
