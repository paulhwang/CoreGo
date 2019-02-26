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
    public class LinkClass
    {
        private string objectName = "LinkClass";

        private PhwangUtils.ListEntryClass listEntryObject;
        private int theLinkId { get; set; }
        private string theLinkIdStr { get; set; }
        private string myName { get; }
        private SessionMgrClass sessionMgrObject { get; }
        private PhwangUtils.ListQueueClass pendingSessionSetupQueue { get; }
        private PhwangUtils.ListQueueClass pendingSessionSetupQueue3 { get; }

        public string MyName() { return this.myName; }
        public int LinkId() { return this.theLinkId; }
        public string LinkIdStr() { return this.theLinkIdStr; }
        public SessionMgrClass SessionMgrObject() { return this.sessionMgrObject; }

        public int GetSessionArrayMaxIndex() { return this.sessionMgrObject.GetSessionArrayMaxIndex(); }
        public PhwangUtils.ListEntryClass[] GetSessionArrayEntryTable() { return this.sessionMgrObject.GetSessionArrayEntryTable(); }

        public LinkClass(string my_name_val)
        {
            this.myName = my_name_val;

            this.pendingSessionSetupQueue = new PhwangUtils.ListQueueClass(false, 0);
            this.pendingSessionSetupQueue3 = new PhwangUtils.ListQueueClass(false, 0);
            this.sessionMgrObject = new SessionMgrClass(this);
        }

        public void BindListEntry(PhwangUtils.ListEntryClass list_entry_objectg_val)
        {
            this.listEntryObject = list_entry_objectg_val;
            this.theLinkId = this.listEntryObject.Id();
            this.theLinkIdStr = PhwangUtils.EncodeNumberClass.EncodeNumber(this.theLinkId, Protocols.FabricFrontEndProtocolClass.FABRIC_LINK_ID_SIZE);
        }

        public SessionClass MallocSession()
        {
            return this.sessionMgrObject.MallocSession();
        }

        public void SetPendingSessionSetup(string link_session_id_str_val, string theme_data_val)
        {
            string data = link_session_id_str_val + theme_data_val;
            this.pendingSessionSetupQueue.EnqueueData(data);
            /*
            char* buf, *data_ptr;

            buf = data_ptr = (char*)malloc(LINK_MGR_DATA_BUFFER_SIZE);
            memcpy(data_ptr, session_id_index_val, SESSION_MGR_PROTOCOL_SESSION_ID_INDEX_SIZE);
            data_ptr += SESSION_MGR_PROTOCOL_SESSION_ID_INDEX_SIZE;
            strcpy(data_ptr, theme_data_val);
            phwangEnqueue(this->thePendingSessionSetupQueue, buf);
            */
        }

        public void SetPendingSessionSetup3(string browser_theme_id_str_val, string session_id_str_val, string theme_data_val)
        {
            string data = browser_theme_id_str_val + session_id_str_val + theme_data_val;
            this.pendingSessionSetupQueue3.EnqueueData(data);
            /*
            char* buf, *data_ptr;

            buf = data_ptr = (char*)malloc(LINK_MGR_DATA_BUFFER_SIZE);
            memcpy(data_ptr, session_id_index_val, SESSION_MGR_PROTOCOL_SESSION_ID_INDEX_SIZE);
            data_ptr += SESSION_MGR_PROTOCOL_SESSION_ID_INDEX_SIZE;
            strcpy(data_ptr, theme_data_val);
            phwangEnqueue(this->thePendingSessionSetupQueue3, buf);
            */

        }
        public string getPendingSessionSetup()
        {
            return (string) this.pendingSessionSetupQueue.DequeueData();
        }

        public string getPendingSessionSetup3()
        {
            return (string) this.pendingSessionSetupQueue3.DequeueData();
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
