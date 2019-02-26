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
using System.Text;
using System.Threading.Tasks;

namespace Phwang.PhwangUtils
{
    class IpcBaseClass
    { 
        public IpcApiClass IpcApi { get; }
        public IpcPathClass IpcPath { get; }
        public IpcTcpClass IpcTcp { get; }

        public IpcBaseClass ()
        {
            this.IpcApi = new IpcApiClass(this);
            this.IpcTcp = new IpcTcpClass(this);
            this.IpcPath = new IpcPathClass(this);
        }
    }
}
