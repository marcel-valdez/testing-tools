namespace TestingTools.Core
{

    internal class AndConnector<TRight, TLeft> : IAssertion<TRight>, IVerifiable<TRight>, ICommandable
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

        #region ICommandable Members

        public void Command(object command)
        {
            if (this.mLeftPredicament is ICommandable)
            {
                (this.mLeftPredicament as ICommandable).Command(command);
            }
        }

        #endregion
    }
}
