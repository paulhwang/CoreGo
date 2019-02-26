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
using System.Threading.Tasks;

namespace Phwang.Engine
{
    public class EngineRootClass
    {
        private string objectName = "EngineRootClass";

        private DEngineClass dEngineObject { get; }
        private BaseMgrClass baseMgrObject { get; }

        public BaseMgrClass BaseMgrObject() { return this.baseMgrObject; }

        public EngineRootClass()
        {
            this.dEngineObject = new DEngineClass(this);
            this.baseMgrObject = new BaseMgrClass(this);
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
