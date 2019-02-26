using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    static public class AbendClass
    {
        static public void phwangLogit(string str0_val, string str1_val)
        {
            Debug.WriteLine(str0_val + " " + str1_val);
        }

        static public void phwangAbend(string str0_val, string str1_val)
        {
            Debug.WriteLine("Abend+++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Debug.WriteLine(str0_val + " " + str1_val);
            Debug.WriteLine("Abend+++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Junk junk = null;
            junk.data = 1;
        }
        private class Junk
        {
            public int data;
        }
    }
}
