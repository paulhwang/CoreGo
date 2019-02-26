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
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    public class BinderClass
    {
        private string objectName = "BinderClass";

        private ListQueueClass receiveQueue { get; }
        private string ownerObject { get; }
        //void* theReceiveObject;
        private NetworkStream networkStream { get; set; }
        private Thread receiveThread { get; set; }
        private Thread transmitThread { get; set; }
        //void* theTransmitQueue;


        public BinderClass(string owner_object_var)
        {
            this.ownerObject = owner_object_var;
            this.receiveQueue = new ListQueueClass(true, 0);
        }

        public bool BindAsTcpClient(string ip_addr_var, short port_var)
        {
            TcpClient client = new TcpClient(ip_addr_var, port_var);
            this.debugIt(true, "BindAsTcpClient", "connected!");
            this.networkStream = client.GetStream();
            createWorkingThreads();
            return true;
        }

        public bool BindAsTcpServer(short port_val)
        {
            PhwangUtils.TcpApiClass.MallocTcpServer(this, port_val, binderTcpServerAcceptFunc /*, this, binderTcpReceiveDataFunc, this*/, this.objectName);
           return true;
        }

        private void binderTcpServerAcceptFunc(object d_fabric_object_val, NetworkStream netwrok_stream_val)
        {
            this.debugIt(true, "binderTcpServerAcceptFunc", "accepted!");
            this.networkStream = netwrok_stream_val;
            this.createWorkingThreads();
        }

        private void createWorkingThreads()
        {
            this.receiveThread = new Thread(this.receiveThreadFunc);
            this.receiveThread.Start();

            this.transmitThread = new Thread(this.transmitThreadFunc);
            this.transmitThread.Start();
        }

        private void receiveThreadFunc()
        {
            this.debugIt(true, "receiveThreadFunc", "start");
            if (this.networkStream == null)
            {
                this.abendIt("receiveThreadFunc", "null networkStream");

            }
            string data;
            while (true)
            {
                data = PhwangUtils.TcpServerClass.TcpReceiveData(this.networkStream);
                if (data != null)
                {
                    this.debugIt(false, "receiveThreadFunc", "data = " + data);
                    this.receiveQueue.EnqueueData(data);
                }
                else
                {
                    this.abendIt("receiveThreadFunc", "data is null=====================================");
                    Thread.Sleep(1);
                }
            }
        }

        private void transmitThreadFunc()
        {
            this.debugIt(true, "transmitThreadFunc", "start");
            while (true)
            {
                Thread.Sleep(10000);
            }
        }

        public string ReceiveData()
        {
            string data = (string) this.receiveQueue.DequeueData();
            if (data != null)
            {
                this.debugIt(false, "ReceivData", "data = " + data);
            }
            return data;
        }

        public void TransmitRawData(string data_var)
        {
            this.debugIt(false, "TransmitData", "data = " + data_var);
            PhwangUtils.TcpServerClass.TcpTransmitData(this.networkStream, data_var);
        }

        public void TransmitData(string data_var)
        {
            this.debugIt(false, "TransmitData", "data = " + data_var);
            this.TransmitRawData(data_var);
        }

        private void debugIt(bool on_off_val, string str0_val, string str1_val)
        {
            if (on_off_val)
                this.logitIt(str0_val, str1_val);
        }

        private void logitIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangLogit(this.objectName + "(" +this.ownerObject + ")." + str0_val + "()", str1_val);
        }

        private void abendIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangAbend(this.objectName + "(" + this.ownerObject + ")." + str0_val + "()", str1_val);
        }
    }
}
