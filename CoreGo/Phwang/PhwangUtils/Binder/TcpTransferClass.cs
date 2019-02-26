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
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    public class TcpTransferClass
    {
        private string ObjectName = "TcpTransferClass";

        //void* theReceiveObject;
        public NetworkStream theStream { get; }
        int theIndex;

        //pthread_t theReceiveThread;
        //pthread_t theReceiveThread2;
        //pthread_t theTransmitThread;
        //void* theTransmitQueue;
        //void* theReceiveQueue;


        public TcpTransferClass(NetworkStream stream_val/*, void (* receive_callback_val)(void*, void*, void*), void* receive_object_val*/)
        {
            this.theStream = stream_val;
            //this->theReceiveCallback = receive_callback_val;
            //this->theReceiveObject = receive_object_val;

            //this->theReceiveQueue = phwangMallocSuspendedQueue(TP_TRANSFER_CLASS_RECEIVE_QUEUE_SIZE);
            //this->theTransmitQueue = phwangMallocSuspendedQueue(TP_TRANSFER_CLASS_TRANSMIT_QUEUE_SIZE);

        }

        private void debug(bool on_off_val, string str0_val, string str1_val)
        {
            if (on_off_val)
                this.logit(str0_val, str1_val);
        }

        private void logit(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangLogit(this.ObjectName + "::" + str0_val, str1_val);
        }

        private void abend(string str0_val, string str1_val)
        {
            PhwangUtils.AbendClass.phwangAbend(this.ObjectName + "::" + str0_val, str1_val);
        }
    }
}
