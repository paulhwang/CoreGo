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
    public class RoomMgrClass
    {
        private string objectName = "RoomMgrClass";
        private const int FIRST_ROOM_ID = 7000;

        private ThemeRootClass themeRootObject { get; }
        private PhwangUtils.ListMgrClass listMgr { get; }

        public RoomMgrClass(ThemeRootClass theme_root_object_val)
        {
            this.themeRootObject = theme_root_object_val;
            this.listMgr = new PhwangUtils.ListMgrClass(this.objectName, FIRST_ROOM_ID);
        }
        public RoomClass MallocRoom(string group_id_str_val)
        {
            RoomClass room = new RoomClass(group_id_str_val);
            PhwangUtils.ListEntryClass list_entry = this.listMgr.MallocEntry(room);
            room.BindListEntry(list_entry);
            return room;
        }

        public RoomClass GetRoomByRoomIdStr(string room_id_str_val)
        {
            int room_id = PhwangUtils.EncodeNumberClass.DecodeNumber(room_id_str_val);

            return this.GetRoomByRoomId(room_id);
         }

        public RoomClass GetRoomByRoomId(int id_val)
        {
            PhwangUtils.ListEntryClass list_entry = this.listMgr.GetEntryById(id_val);
            if (list_entry == null)
            {
                return null;
            }
            RoomClass room_object = (RoomClass)list_entry.Data();

            return room_object;
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
