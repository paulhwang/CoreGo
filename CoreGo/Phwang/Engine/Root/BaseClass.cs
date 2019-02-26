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
    public class BaseClass
    {
        private string objectName = "BaseClass";

        private PhwangUtils.ListEntryClass listEntryObject;
        private string roomIdStr { get; }
        private int baseId { get; set; }
        private string baseIdStr { get; set; }
        private Go.GoRootClass goRootObject { get; set; }

        public string RoomIdStr() { return this.roomIdStr; }
        public string BaseIdStr() { return this.baseIdStr; }

        public BaseClass(string room_id_str_val)
        {
            this.roomIdStr = room_id_str_val;
        }

        public void BindListEntry(PhwangUtils.ListEntryClass list_entry_objectg_val)
        {
            this.listEntryObject = list_entry_objectg_val;
            this.baseId = this.listEntryObject.Id();
            this.baseIdStr = PhwangUtils.EncodeNumberClass.EncodeNumber(this.baseId, Protocols.ThemeEngineProtocolClass.ENGINE_BASE_ID_SIZE);
        }

        public string SetupBase(string input_data_val)
        {
            string input_data = input_data_val.Substring(1);

            switch (input_data_val[0]) {
                case 'G':
                    this.goRootObject = new Go.GoRootClass();
                    return this.goRootObject.DoSetup(input_data);

                default:
                    string err_msg = "command " + input_data_val[0] + " not supported";
                    this.abendIt("ProcessInputData", err_msg);
                    return err_msg;
            }
        }

        public string ProcessInputData(string input_data_val)
        {
            string input_data = input_data_val.Substring(1);

            switch (input_data_val[0]) {
                case 'G':
                    string output_data = this.goRootObject.ProcessInputData(input_data);
                    return output_data;

                default:
                    string err_msg = "command " + input_data_val[0] + " not supported";
                    this.abendIt("ProcessInputData", err_msg);
                    return err_msg;
            }
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
