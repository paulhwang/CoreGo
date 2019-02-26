/*
 ******************************************************************************
 *                                       
 *  Copyright (c) 2018 phwang. All rights reserved.
 *
 ******************************************************************************
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Phwang.Fabric
{
    public class DFabricClass
    {
        private string objectName = "DFabricClass";

        private FabricRootClass fabricRootObject { get; }
        private DFabricParserClass dFabricParserObject { get; }
        public PhwangUtils.BinderClass binderObject { get; set; }
        private Thread receiveThread { get; set; }

        public FabricRootClass FabricRootObject() { return this.fabricRootObject; }

        public DFabricClass(FabricRootClass fabric_root_class_val)
        {
            this.debugIt(true, "DFabricClass", "init start");
            this.fabricRootObject = fabric_root_class_val;
            this.dFabricParserObject = new DFabricParserClass(this);
            this.binderObject = new PhwangUtils.BinderClass(this.objectName);
            this.binderObject.BindAsTcpServer(Protocols.FabricFrontEndProtocolClass.LINK_MGR_PROTOCOL_TRANSPORT_PORT_NUMBER);

            this.receiveThread = new Thread(this.receiveThreadFunc);
            this.receiveThread.Start();

            this.debugIt(true, "DFabricClass", "init done");
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
                this.dFabricParserObject.parseInputPacket(data);

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
