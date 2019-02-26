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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    class IpcPathClass
    {
        private string objectName = "IpcPathClass";

        private int PATH_ARRAY_LENGTH { get; } = 100;
        private IpcBaseClass IpcBase { get; }
        private IpcPathEntryClass[] PathEntryArray { get; }

        public IpcPathClass (IpcBaseClass base_var)
        {
            this.IpcBase = base_var;
            this.PathEntryArray = new IpcPathEntryClass[this.PATH_ARRAY_LENGTH];
        }

        public IpcTcpClass IpcTcp()
        {
            return this.IpcBase.IpcTcp;
        }

        public void TransmitData (int path_id_var, string data_var)
        {
            IpcPathEntryClass path_entry = GetPath(path_id_var);
            this.IpcTcp().TcpTransmitData(path_entry.TcpStream, data_var);
        }

        public string ReceiveData (int path_id_var)
        {
            IpcPathEntryClass path_entry = GetPath(path_id_var);
            if (path_entry == null)
            {
                this.abendIt("ReceiveData", "null path_entry");
                return null;
            }

            NetworkStream stream = path_entry.TcpStream;
            if (stream == null)
            {
                this.abendIt("ReceiveData", "null stream");
                return null;
            }

            string data = this.IpcTcp().TcpReceiveData(stream);
            return data;
        }

        private IpcPathEntryClass GetPath (int path_id_var)
        {
            if ((path_id_var < 0) || (this.PATH_ARRAY_LENGTH <= path_id_var))
            {
                return null;
            }
            return this.PathEntryArray[path_id_var];
        }

        public int AllocPath (NetworkStream stream_var)
        {
            
            IpcPathEntryClass path = new IpcPathEntryClass();
            path.TcpStream = stream_var;

            for (int i = 0; i < this.PATH_ARRAY_LENGTH; i++)
            {
                if (this.PathEntryArray[i] == null)
                {
                    this.PathEntryArray[i] = path;
                    return i;
                }
            }

            return -1;
        }

        public void FreePath (int path_id_var)
        {
            if ((path_id_var < 0) || (this.PATH_ARRAY_LENGTH <= path_id_var))
            {
                return;
            }
            this.PathEntryArray[path_id_var] = null;
        }

        private void debug(bool on_off_val, string str0_val, string str1_val)
        {
            if (on_off_val)
                this.logitIt(str0_val, str1_val);
        }

        private void logitIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangLogit(this.objectName + "::" + str0_val, str1_val);
        }

        private void abendIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangAbend(this.objectName + "::" + str0_val, str1_val);
        }
    }

    class IpcPathEntryClass
    {
        private string objectName = "IpcPathEntryClass";
        public ListQueueClass ReceiveQueue;
        private Thread ReceiveThread;
        public NetworkStream TcpStream;

        public IpcPathEntryClass ()
        {
            this.ReceiveQueue = new ListQueueClass(true, 1000);
            this.ReceiveThread = new Thread(ReceiveThreadFunc);
        }

        void ReceiveThreadFunc ()
        {

        }

        private void debug(bool on_off_val, string str0_val, string str1_val)
        {
            if (on_off_val)
                this.logitIt(str0_val, str1_val);
        }

        private void logitIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangLogit(this.objectName + "::" + str0_val, str1_val);
        }

        private void abendIt(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangAbend(this.objectName + "::" + str0_val, str1_val);
        }
    }

}
