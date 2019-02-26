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
    public class NameListClass
    {
        private string objectName = "NameListClass";
        const int NAME_LIST_CLASS_NAME_LIST_TAG_SIZE = Protocols.FabricFrontEndProtocolClass.WEB_FABRIC_PROTOCOL_NAME_LIST_TAG_SIZE;
        const int NAME_LIST_CLASS_MAX_NAME_LIST_TAG = 999;

        private FabricRootClass fabricRootObject { get; }
        private int nameListTag { get; set; }
        private string nameListTagStr { get; set; }
        private string nameList { get; set; }

        public string NameListTagStr() { return this.nameListTagStr; }
        public string NameList() { return this.nameList; }

        public NameListClass(FabricRootClass root_fabric_object_val)
        {
            this.fabricRootObject = root_fabric_object_val;
        }
        public void UpdateNameList()
        {
            LinkMgrClass link_list_mgr = this.fabricRootObject.LinkMgrObject();

            int max_index = link_list_mgr.ListMgr().MaxIndex();
            PhwangUtils.ListEntryClass[] list_entry_array = link_list_mgr.ListMgr().EntryTableArray();

            this.nameListTag++;
            if (this.nameListTag > NAME_LIST_CLASS_MAX_NAME_LIST_TAG)
            {
                this.nameListTag = 1;
            }
            this.nameListTagStr = PhwangUtils.EncodeNumberClass.EncodeNumber(this.nameListTag, NAME_LIST_CLASS_NAME_LIST_TAG_SIZE);

            this.nameList = "";
            for (int i = max_index; i >= 0; i--)
            {
                if (list_entry_array[i] != null)
                {
                    if (this.nameList.Length == 0)
                    {
                        this.nameList = PhwangUtils.EncodeNumberClass.EncodeNumber(this.nameListTag, NAME_LIST_CLASS_NAME_LIST_TAG_SIZE);
                    }
                    else
                    {
                        this.nameList = this.nameList + ",";
                    }
                    LinkClass link = (LinkClass) list_entry_array[i].Data();
                    this.nameList = this.nameList + '"' + link.MyName() + '"';
                }
            }

            this.debugIt(true, "updateNameList", this.nameList);
        }

        public string GetNameList(int tag_val)
        {
            if (this.nameListTag == tag_val)
            {
                return null;
            }
            return this.nameList;
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
