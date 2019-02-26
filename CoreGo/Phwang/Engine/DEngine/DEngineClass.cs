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

namespace Phwang.Engine
{
    public class DEngineClass
    {
        private string objectName = "DEngineClass";

        private EngineRootClass engineRootObject { get; }
        private DEngineParserClass dEngineParserObject { get; }
        public PhwangUtils.BinderClass binderObject { get; set; }
        private Thread receiveThread { get; set; }

        public EngineRootClass EngineRootObject() { return this.engineRootObject; }

        public DEngineClass(EngineRootClass engine_root_object_val)
        {
            this.debugIt(true, "DEngineClass", "init start");
            this.engineRootObject = engine_root_object_val;
            this.dEngineParserObject = new DEngineParserClass(this);
            this.binderObject = new PhwangUtils.BinderClass(this.objectName);

            this.receiveThread = new Thread(this.receiveThreadFunc);
            this.receiveThread.Start();

            this.binderObject.BindAsTcpClient("127.0.0.1", Protocols.ThemeEngineProtocolClass.BASE_MGR_PROTOCOL_TRANSPORT_PORT_NUMBER);
            this.debugIt(true, "DEngineClass", "init done");
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
                this.dEngineParserObject.ParseInputPacket(data);

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
