namespace TestingTools.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal interface ICommandable
    {
        void Command(object command);
    }
}
