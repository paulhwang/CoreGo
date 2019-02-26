/*
 ******************************************************************************
 *                                       
 *  Copyright (c) 2018 phwang. All rights reserved.
 *
 ******************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phwang.FrontEnd
{
    public class FrontEndJobClass
    {
        private string objectName = "FrontEndJobClass";

        public string ajaxIdStr { get; }
        private ManualResetEvent theSignal { get; }
        private string theData { get; set; }

        public FrontEndJobClass(string ajax_id_str_val)
        {
            this.ajaxIdStr = ajax_id_str_val;
            this.theSignal = new ManualResetEvent(false);
        }

        public string ReadData()
        {
            this.theSignal.WaitOne();
            string data = this.theData;
            if (data == null)
            {
                this.abendIt("ReceiveData", "null data");
            }
            return data;
        }

        public void WriteData(string data_val)
        {
            this.theData = data_val;
            this.theSignal.Set();
        }

        private void debugIt(bool on_off_val, string str0_val, string str1_val)
        {
            if (on_off_val)
                this.logitIt(str0_val, str1_val);
        }

        private void logitIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangLogit(this.objectName + "." + str0_val + "()", str1_val);
        }

        private void abendIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangAbend(this.objectName + "." + str0_val + "()", str1_val);
        }
    }
}
