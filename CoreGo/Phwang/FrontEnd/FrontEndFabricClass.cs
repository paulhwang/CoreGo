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
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Phwang.FrontEnd
{


    public class FrontEndFabricClass
    {
        private string objectName = "FrontEndFabricClass";

        private FrontEndRootClass frontEndRootObject { get; }
        private PhwangUtils.BinderClass binderObject { get; }
        private FrontEndJobMgrClass frontEndJobMgrObject { get; }
        private Thread receiveThread { get; set; }
        private bool stopReceiveThreadFlag { get; set; }

        public FrontEndFabricClass(FrontEndRootClass root_object_val)
        {
            this.debugIt(true, "FrontEndFabricClass", "init start");
            this.frontEndRootObject = root_object_val;
            this.stopReceiveThreadFlag = false;
            this.binderObject = new PhwangUtils.BinderClass(this.objectName);
            this.frontEndJobMgrObject = new FrontEndJobMgrClass(this);
            this.receiveThread = new Thread(this.receiveThreadFunc);
            this.receiveThread.Start();
            this.binderObject.BindAsTcpClient("127.0.0.1", Protocols.FabricFrontEndProtocolClass.LINK_MGR_PROTOCOL_TRANSPORT_PORT_NUMBER);
            this.debugIt(true, "FrontEndFabricClass", "init done");
        }

        private void receiveThreadFunc()
        {
            this.debugIt(true, "receiveThreadFunc", "====================start");
            while (true)
            {
                if (this.stopReceiveThreadFlag)
                {
                    break;
                }

                string received_data = this.binderObject.ReceiveData();
                string ajax_id_str = received_data.Substring(0, Protocols.FabricFrontEndProtocolClass.AJAX_MAPING_ID_SIZE);
                string response_data = received_data.Substring(Protocols.FabricFrontEndProtocolClass.AJAX_MAPING_ID_SIZE);

                FrontEndJobClass job_entry = this.frontEndJobMgrObject.GetJobObject(ajax_id_str);
                if (job_entry == null)
                {
                    this.abendIt("receiveDataFromFabric", "null ajax_entry");
                    continue;
                }

                job_entry.WriteData(response_data);
            }

            this.debugIt(true, "receiveThreadFunc", "exit");
        }

        public void StopReceiveThread()
        {
            this.stopReceiveThreadFlag = true;
        }

        public string ProcessAjaxRequestPacket(string input_data_val)
        {
            this.debugIt(true, "ProcessAjaxRequestPacket", "input_data_val = " + input_data_val);
            FrontEndJobClass job_entry = this.frontEndJobMgrObject.MallocJobObject();
            this.binderObject.TransmitData(job_entry.ajaxIdStr + input_data_val);
            string response_data = job_entry.ReadData();
            this.debugIt(true, "ProcessAjaxRequestPacket", "response_data = " + response_data);
            return response_data;
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
