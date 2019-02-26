using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    public class TcpApiClass
    {
        public static TcpServerClass MallocTcpServer(Object caller_object_val,
                                short port_val,
                                TcpServerClass.TcpAcceptDelegate accept_callback_func_val,
                                 /*
                                void (* accept_callback_func_val)(void*, void*),
                                void* accept_callback_parameter_val,
                                void (* receive_callback_func_val)(void*, void*, void*),
                                void* receive_callback_parameter_val*/
                                string who_val)
        {
            TcpServerClass tp_server_object = new TcpServerClass(
                    caller_object_val,
                    port_val,
                    accept_callback_func_val,
                    /*,
                    accept_callback_parameter_val,
                    receive_callback_func_val,
                    receive_callback_parameter_val*/
                    who_val);

            if (tp_server_object != null) {
                tp_server_object.StartServerThread();
            }
            return tp_server_object;
        }
    }
}
