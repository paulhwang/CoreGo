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

namespace Phwang.Theme
{
    public class UThemeClass
    {
        private string objectName = "UThemeClass";

        private ThemeRootClass themeRootObject { get; }
        private UThemeParserClass uThemeParserObject { get; }
        public PhwangUtils.BinderClass binderObject { get; set; }
        private Thread receiveThread { get; set; }

        public ThemeRootClass ThemeRootObject() { return this.themeRootObject; }

        public UThemeClass(ThemeRootClass theme_root_object_val)
        {
            this.themeRootObject = theme_root_object_val;
            this.uThemeParserObject = new UThemeParserClass(this);
            this.binderObject = new PhwangUtils.BinderClass(this.objectName);
            this.binderObject.BindAsTcpServer(Protocols.ThemeEngineProtocolClass.BASE_MGR_PROTOCOL_TRANSPORT_PORT_NUMBER);

            this.receiveThread = new Thread(this.receiveThreadFunc);
            this.receiveThread.Start();
        }

        private void receiveThreadFunc()
        {
            this.debugIt(true, "receiveThreadFunc", "start");

            string data;
            while (true)
            {
                data = this.binderObject.ReceiveData();
                if (data == null)
                {
                    this.abendIt("receiveThreadFunc", "null data");
                    continue;
                }
                this.debugIt(true, "receiveThreadFunc", "data = " + data);
                this.uThemeParserObject.ParseInputPacket(data);

            }
        }

        public void TransmitData(string data_val)
        {
            this.binderObject.TransmitData(data_val);
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
