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
    public class GroupMgrClass
    {
        private string objectName = "GroupMgrClass";
        private const int FIRST_GROUP_ID = 5000;


        private FabricRootClass fabricRootObject { get; }
        private PhwangUtils.ListMgrClass listMgr { get; }

        public PhwangUtils.ListMgrClass ListMgr() { return this.listMgr; }

        public GroupMgrClass(FabricRootClass root_fabric_object_val)
        {
            this.fabricRootObject = root_fabric_object_val;
            this.listMgr = new PhwangUtils.ListMgrClass(this.objectName, FIRST_GROUP_ID);
        }

        public GroupClass MallocGroup(string theme_data_val)
        {
            GroupClass group = new GroupClass(theme_data_val);
            PhwangUtils.ListEntryClass list_entry = this.listMgr.MallocEntry(group);
            group.BindListEntry(list_entry);
            return group;
        }

        public void FreeLink(LinkClass link_val)
        {

        }

        public GroupClass GetGroupByGroupIdStr(string group_id_str_val)
        {
            int group_id = PhwangUtils.EncodeNumberClass.DecodeNumber(group_id_str_val);

            return this.GetGroupByGroupId(group_id);
        }

        public GroupClass GetGroupByGroupId(int group_id_val)
        {
            PhwangUtils.ListEntryClass list_entry = this.listMgr.GetEntryById(group_id_val);
            if (list_entry == null)
            {
                return null;
            }
            GroupClass room_object = (GroupClass)list_entry.Data();

            return room_object;
        }

        public LinkClass GetLinkById(int id_val)
        {
            PhwangUtils.ListEntryClass list_entry = this.listMgr.GetEntryById(id_val);
            if (list_entry == null)
            {
                return null;
            }
            LinkClass link = (LinkClass)list_entry.Data();

            return link;
        }

        public LinkClass GetLinkByMyName(string my_name_val)
        {
            LinkClass link = null;

            return link;
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
