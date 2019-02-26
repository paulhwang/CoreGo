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

namespace Phwang.Theme
{
    public class RoomClass
    {
        private string objectName = "RoomClass";

        private PhwangUtils.ListEntryClass listEntryObject;
        private string groupIdStr { get; }
        private int roomId { get; set; }
        private string roomIdStr { get; set; }
        private string baseIdStr { get; set; }

        public string RoomIdStr() { return this.roomIdStr; }
        public string GroupIdStr() { return this.groupIdStr; }
        public string BaseIdStr() { return this.baseIdStr; }


        public RoomClass(string group_id_str_val)
        {
            this.groupIdStr = group_id_str_val;
        }

        public void BindListEntry(PhwangUtils.ListEntryClass list_entry_objectg_val)
        {
            this.listEntryObject = list_entry_objectg_val;
            this.roomId = this.listEntryObject.Id();
            this.roomIdStr = PhwangUtils.EncodeNumberClass.EncodeNumber(this.roomId, Protocols.FabricFrontEndProtocolClass.FABRIC_LINK_ID_SIZE);
        }

        public void PutBaseIdStr(string base_id_str_val)
        {
            this.baseIdStr = base_id_str_val;
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
