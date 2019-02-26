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
    public class DThemeParserClass
    {
        private string objectName = "DThemeParserClass";

        private DThemeClass dThemeObject { get; }

        public ThemeRootClass ThemeRootObject() { return this.dThemeObject.ThemeRootObject(); }
        public UThemeClass UThemeObject() { return this.ThemeRootObject().UThemeObject(); }
        public RoomMgrClass RoomMgrObject() { return this.ThemeRootObject().RoomMgrObject(); }

        public DThemeParserClass(DThemeClass d_theme_object_val)
        {
            this.dThemeObject = d_theme_object_val;
        }

        public void ParseInputPacket(string data_val)
        {
            this.debugIt(true, "ParseInputPacket", data_val);
            string command = data_val.Substring(0, 1);
            string data = data_val.Substring(1);

            if (command == Protocols.FabricThemeProtocolClass.FABRIC_THEME_PROTOCOL_COMMAND_IS_SETUP_ROOM)
            {
                this.processSetupRoom(data);
                return;
            }

            if (command == Protocols.FabricThemeProtocolClass.FABRIC_THEME_PROTOCOL_COMMAND_IS_PUT_ROOM_DATA)
            {
                this.processPutRoomData(data);
                return;
            }

            this.abendIt("ParseInputPacket", data_val);

        }

        private void processSetupRoom(string input_data_val)
        {
            this.debugIt(true, "processSetupRoom", input_data_val);

            string group_id_index = input_data_val.Substring(0, Protocols.FabricThemeProtocolClass.FABRIC_GROUP_ID_SIZE);
            string input_data = input_data_val.Substring(Protocols.FabricThemeProtocolClass.FABRIC_GROUP_ID_SIZE);

            RoomClass room = this.RoomMgrObject().MallocRoom(group_id_index);
            if (room == null)
            {
                string downlink_data;
                this.abendIt("processSetupRoom", "null room");
                //downlink_data = data_ptr = (char*)phwangMalloc(ROOM_MGR_DATA_BUFFER_SIZE + 4, "DTSr");
                //*data_ptr++ = FABRIC_THEME_PROTOCOL_RESPOND_IS_SETUP_ROOM;
                //strcpy(data_ptr, "null room");
                //this->transmitFunction(downlink_data);
                return;
            }
            /*
            data_val += GROUP_MGR_PROTOCOL_GROUP_ID_INDEX_SIZE;

            uplink_data = data_ptr = (char*)phwangMalloc(ROOM_MGR_DATA_BUFFER_SIZE + 4, "DTSR");
            *data_ptr++ = THEME_ENGINE_PROTOCOL_COMMAND_IS_SETUP_BASE;

            memcpy(data_ptr, room->roomIdIndex(), ROOM_MGR_PROTOCOL_ROOM_ID_INDEX_SIZE);
            data_ptr += ROOM_MGR_PROTOCOL_ROOM_ID_INDEX_SIZE;

            strcpy(data_ptr, data_val);
            */
            string uplink_data = Protocols.ThemeEngineProtocolClass.THEME_ENGINE_PROTOCOL_COMMAND_IS_SETUP_BASE;
            uplink_data = uplink_data + room.RoomIdStr() + input_data;
            this.UThemeObject().TransmitData(uplink_data);
        }

        private void processPutRoomData(string input_data_val)
        {
            this.debugIt(true, "processPutRoomData", input_data_val);

            string room_id_str = input_data_val.Substring(0, Protocols.ThemeEngineProtocolClass.THEME_ROOM_ID_SIZE);
            string input_data = input_data_val.Substring(Protocols.ThemeEngineProtocolClass.THEME_ROOM_ID_SIZE);
            RoomClass room = this.RoomMgrObject().GetRoomByRoomIdStr(room_id_str);
            if (room == null)
            {
                this.abendIt("processPutRoomData", "null room");
                return;
            }

            string uplink_data = Protocols.ThemeEngineProtocolClass.THEME_ENGINE_PROTOCOL_COMMAND_IS_PUT_BASE_DATA;
            uplink_data = uplink_data + room.BaseIdStr() + input_data;
            this.UThemeObject().TransmitData(uplink_data);
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
