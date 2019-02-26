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

namespace Phwang.Fabric
{
    public class GroupSessionMgrClass
    {
        private string objectName = "GroupSessionClass";
        private const int MAX_SESSION_ARRAY_SIZE = 10;

        private GroupClass groupObject { get; }
        private PhwangUtils.ArrayMgrClass arrayMgrObject { get; }


        public GroupSessionMgrClass(GroupClass group_object_val)
        {
            this.groupObject = group_object_val;
            this.arrayMgrObject = new PhwangUtils.ArrayMgrClass(this.objectName, 'o', MAX_SESSION_ARRAY_SIZE);
        }

        public void InsertSession(SessionClass session_val)
        {
            this.arrayMgrObject.InsertObjectElement(session_val);
        }
        public void RemoveSession(SessionClass session_val)
        {
            this.arrayMgrObject.RemoveObjectElement(session_val);
        }

        public int GetSessionArraySize()
        {
            return this.arrayMgrObject.ArraySize();
        }

        public object[] GetSessionArray()
        {
            return this.arrayMgrObject.ObjectArrayTable();
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
