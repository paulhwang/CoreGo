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
    public class BaseMgrClass
    {
        private string objectName = "BaseMgrClass";
        private const int FIRST_BASE_ID = 9000;

        private EngineRootClass engineRootObject { get; }
        private PhwangUtils.ListMgrClass listMgr { get; }

        public PhwangUtils.ListMgrClass ListMgr() { return this.listMgr; }

        public BaseMgrClass(EngineRootClass engine_root_object_val)
        {
            this.engineRootObject = engine_root_object_val;
            this.listMgr = new PhwangUtils.ListMgrClass(this.objectName, FIRST_BASE_ID);
        }

        public BaseClass MallocGoBase(string room_id_val)
        {
            BaseClass go_base = new BaseClass(room_id_val);
            PhwangUtils.ListEntryClass list_entry = this.listMgr.MallocEntry(go_base);
            go_base.BindListEntry(list_entry);
            return go_base;
        }

        public void FreeGoBase(BaseClass link_val)
        {

        }
        public BaseClass GetBaseByIdStr(string base_id_str_val)
        {
            int base_id = PhwangUtils.EncodeNumberClass.DecodeNumber(base_id_str_val);

            return this.GetBaseById(base_id);
        }

        public BaseClass GetBaseById(int id_val)
        {
            PhwangUtils.ListEntryClass list_entry = this.listMgr.GetEntryById(id_val);
            if (list_entry == null)
            {
                return null;
            }
            BaseClass base_object = (BaseClass)list_entry.Data();

            return base_object;
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
