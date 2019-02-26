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
    class IpcTestClass
    {
        private string objectName = "IpcTestClass";

        public static void TestServer (string ip_addr_var, int port_var)
        {
            Thread server_thread = new Thread(IpcTestClass.RunAsServer);
            server_thread.Start(new IpAddrPort(ip_addr_var, port_var));
            Thread.Sleep(1000);

            //while (true)
            {
                //GetacSwrdcUtil.DebugIt("Main", "waiting");
                Thread.Sleep(1000);
            }
        }
        public static void TestClient (string ip_addr_var, int port_var)
        {
            Thread client_thread = new Thread(IpcTestClass.RunAsClient);
            client_thread.Start(new IpAddrPort(ip_addr_var, port_var));

            //while (true)
            {
                //GetacSwrdcUtil.DebugIt("Main", "waiting");
                Thread.Sleep(1000);
            }
        }

        public static void TestIpc (string ip_addr_var, int port_var)
        {
            Thread server_thread = new Thread(IpcTestClass.RunAsServer);
            server_thread.Start(new IpAddrPort(ip_addr_var, port_var));
            Thread.Sleep(1000);

            Thread client_thread = new Thread(IpcTestClass.RunAsClient);
            client_thread.Start(new IpAddrPort(ip_addr_var, port_var));

            //while (true)
            {
                //GetacSwrdcUtil.DebugIt("Main", "waiting");
                Thread.Sleep(1000);
            }
        }


        public static void RunAsServer(object ip_addr_port_var)
        {
            IpAddrPort ip_addr_port = (IpAddrPort)ip_addr_port_var;

            IpcBaseClass ipc_base = new IpcBaseClass();
            IpcApiClass ipc_api = ipc_base.IpcApi;

            int path_id = ipc_api.ApiTcpServer(ip_addr_port.IpAddr, ip_addr_port.Port);
            if (path_id == -1)
            {
                return;
            }
 
            while (true)
            {
                string data = ipc_api.ApiTcpReceiveData(path_id);
                //this.debugIt(true, "TestServer receive:", data);
                //Thread.Sleep(100);
            }
        }

        public static void RunAsClient(object ip_addr_port_var)
        {
            IpAddrPort ip_addr_port = (IpAddrPort)ip_addr_port_var;

            IpcBaseClass ipc_base = new IpcBaseClass();
            IpcApiClass ipc_api = ipc_base.IpcApi;

            int path_id = ipc_api.ApiTcpClient(ip_addr_port.IpAddr, ip_addr_port.Port);
            if (path_id == -1)
            {
                //this.debugIt(true, "TestClient", "***** path_id == -1");
                return;
            }

            Thread.Sleep(1000);
            for (int i = 0; i < 5; i++)
            {
                ipc_api.ApiTcpTransmitData(path_id, "hello from phwang");
            }
        }

        private void debugIt(bool on_off_val, string str0_val, string str1_val)
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
    class IpAddrPort
    {
        public string IpAddr { get; }
        public int Port { get; }

        public IpAddrPort(string ip_addr_var, int port_var)
        {
            this.IpAddr = ip_addr_var;
            this.Port = port_var;
        }
    }
}
