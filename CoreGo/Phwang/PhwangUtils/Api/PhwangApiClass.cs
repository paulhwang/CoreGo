using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    public class PhwangApiClass
    {
        public void phwangLogit(string str0_val, string str1_val)
        {
            Debug.WriteLine(str0_val + " " + str1_val);
        }

        public void phwangAbend(string str0_val, string str1_val)
        {
            Debug.WriteLine(str0_val + " " + str1_val);
        }

    }
}
