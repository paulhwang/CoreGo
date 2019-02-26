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
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    class IpcApiClass
    {
        private IpcBaseClass IpcBase { get; }

        public IpcApiClass (IpcBaseClass base_var)
        {
            this.IpcBase = base_var;
        }

        public IpcPathClass IpcPath ()
        {
            return this.IpcBase.IpcPath;
        }

        public IpcTcpClass IpcTcp()
        {
            return this.IpcBase.IpcTcp;
        }

        public int ApiTcpServer (string ip_addr_var, int port_var)
        {
             int path_id = this.IpcTcp().TcpServer(ip_addr_var, port_var);
             return path_id;
        }

        public int ApiTcpClient (string ip_addr_var, int port_var)
        {
            int path_id = this.IpcTcp().TcpClient(ip_addr_var, port_var);
            return path_id;
        }

        public void ApiTcpTransmitData (int path_id_var, string data_var)
        {
            //Utils.DebugClass.DebugIt("ApiTcpTransmitData", data_var);
            this.IpcPath().TransmitData(path_id_var, data_var);
        }

        public string ApiTcpReceiveData (int path_id_var)
        {
             string data = this.IpcPath().ReceiveData(path_id_var);
            //Utils.DebugClass.DebugIt("ApiTcpReceiveData: data=", data);
            return data;
        }
    }
}

