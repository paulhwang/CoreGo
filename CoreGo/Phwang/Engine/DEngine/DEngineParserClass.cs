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
    public class DEngineParserClass
    {
        private string objectName = "DEngineParserClass";

        private DEngineClass dEngineObject;

        public EngineRootClass EngineRootObject() { return this.dEngineObject.EngineRootObject(); }
        public BaseMgrClass BaseMgrObject() { return this.EngineRootObject().BaseMgrObject(); }


    public DEngineParserClass(DEngineClass d_engine_object_val)
        {
            this.dEngineObject = d_engine_object_val;
        }

        public void ParseInputPacket(string input_data_val)
        {
            this.debugIt(true, "ParseInputPacket", "data=" + input_data_val);

            string command = input_data_val.Substring(0, 1);
            string input_data = input_data_val.Substring(1);


            if (command == Protocols.ThemeEngineProtocolClass.THEME_ENGINE_PROTOCOL_COMMAND_IS_SETUP_BASE)
            {
                this.processSetupBase(input_data);
                return;
            }

            if (command == Protocols.ThemeEngineProtocolClass.THEME_ENGINE_PROTOCOL_COMMAND_IS_PUT_BASE_DATA)
            {
                this.processPutBaseData(input_data);
                return;
            }

            this.abendIt("ParseInputPacket", input_data_val);
        }

        private void processSetupBase(string input_data_val)
        {
            this.debugIt(true, "processSetupBase", "data=" + input_data_val);

            string room_id_str = input_data_val.Substring(0, Protocols.ThemeEngineProtocolClass.THEME_ROOM_ID_SIZE);
            string input_data = input_data_val.Substring(Protocols.ThemeEngineProtocolClass.THEME_ROOM_ID_SIZE);

            BaseClass go_base_object = this.BaseMgrObject().MallocGoBase(room_id_str);
            if (go_base_object == null)
            {
                this.abendIt("processSetupBase", "null go_base");
                return;
            }

            string output_data = go_base_object.SetupBase(input_data);

            string downlink_data = Protocols.ThemeEngineProtocolClass.THEME_ENGINE_PROTOCOL_RESPOND_IS_SETUP_BASE;
            downlink_data = downlink_data + go_base_object.RoomIdStr() + go_base_object.BaseIdStr() + output_data;
            this.dEngineObject.TransmitData(downlink_data);
        }

        private void processPutBaseData(string input_data_val)
        {
            this.debugIt(true, "processPutBaseData", "data=" + input_data_val);
            string base_id_str = input_data_val.Substring(0, Protocols.ThemeEngineProtocolClass.ENGINE_BASE_ID_SIZE);
            string input_data = input_data_val.Substring(Protocols.ThemeEngineProtocolClass.ENGINE_BASE_ID_SIZE);

            BaseClass go_base_object = this.BaseMgrObject().GetBaseByIdStr(base_id_str);
            if (go_base_object == null)
            {
                this.abendIt("processPutBaseData", "null go_base");
                return;
            }

            string output_data = go_base_object.ProcessInputData(input_data);

            string downlink_data = Protocols.ThemeEngineProtocolClass.THEME_ENGINE_PROTOCOL_RESPOND_IS_PUT_BASE_DATA;
            downlink_data = downlink_data + go_base_object.RoomIdStr() + output_data;
            this.dEngineObject.TransmitData(downlink_data);
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
