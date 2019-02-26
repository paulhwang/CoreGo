using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.FrontEnd
{
    public class FrontEndRootClass
    {
        public FrontEndFabricClass frontEndFabricObject { get; }

        public FrontEndRootClass()
        {
            this.frontEndFabricObject = new FrontEndFabricClass(this);
        }
    }
}
