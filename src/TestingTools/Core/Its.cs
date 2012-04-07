namespace TestingTools.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Its<TTarget>
    {
        public static IAssertion<TMember> Member<TMember>(Func<TTarget, TMember> getter)
        {
            return new ItsAssertable<TTarget, TMember>(getter);
        }
    }
}