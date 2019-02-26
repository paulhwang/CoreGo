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

namespace Phwang.Fabric
{
    public class UFabricClass
    {
        private string objectName = "UFabricClass";

        const string FABRIC_THEME_PROTOCOL_COMMAND_IS_SETUP_ROOM = "R";
        //#define FABRIC_THEME_PROTOCOL_RESPOND_IS_SETUP_ROOM 'r'
        //#define FABRIC_THEME_PROTOCOL_COMMAND_IS_PUT_ROOM_DATA 'D'
        //#define FABRIC_THEME_PROTOCOL_RESPOND_IS_PUT_ROOM_DATA 'd'

        private FabricRootClass fabricRootObject { get; }
        private UFabricParserClass uFabricParserObject { get; }
        public PhwangUtils.BinderClass binderObject { get; set; }
        private Thread receiveThread { get; set; }

        public FabricRootClass FabricRootObject() { return this.fabricRootObject; }


        public UFabricClass(FabricRootClass fabric_root_class_val)
        {
            this.debugIt(true, "UFabricClass", "init start");
            this.fabricRootObject = fabric_root_class_val;
            this.uFabricParserObject = new UFabricParserClass(this);
            this.binderObject = new PhwangUtils.BinderClass(this.objectName);
            this.binderObject.BindAsTcpServer(Protocols.FabricThemeProtocolClass.GROUP_ROOM_PROTOCOL_TRANSPORT_PORT_NUMBER);

            this.receiveThread = new Thread(this.receiveThreadFunc);
            this.receiveThread.Start();

            this.debugIt(true, "UFabricClass", "init done");
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
                this.uFabricParserObject.parseInputPacket(data);

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
