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
    public class SessionClass
    {
        private string objectName = "SessionClass";

        private LinkClass linkObject { get; }
        private PhwangUtils.ListEntryClass listEntryObject;
        private int sessionId { get; set; }
        private string sessionIdStr { get; set; }
        private string browserThemeIdStr { get; set; }
        private GroupClass groupObject { get; set; }
        PhwangUtils.ListQueueClass pendingDownLinkDataQueue;

        public LinkClass LinkObject() { return this.linkObject; }
        public int SessionId() { return this.sessionId; }
        public string SessionIdStr() { return this.sessionIdStr; }
        public string BrowserThemeIdStr() { return this.browserThemeIdStr; }
        public GroupClass GroupObject() { return this.groupObject; }
        PhwangUtils.ListQueueClass PendingDownLinkDataQueue() { return this.pendingDownLinkDataQueue; }

        public SessionClass(LinkClass link_object_val)
        {
            this.linkObject = link_object_val;
            this.pendingDownLinkDataQueue = new PhwangUtils.ListQueueClass(false, 0);
        }

        public void BindListEntry(PhwangUtils.ListEntryClass list_entry_objectg_val)
        {
            this.listEntryObject = list_entry_objectg_val;
            this.sessionId = this.listEntryObject.Id();
            this.sessionIdStr = PhwangUtils.EncodeNumberClass.EncodeNumber(this.sessionId, Protocols.FabricFrontEndProtocolClass.FABRIC_SESSION_ID_SIZE);
        }

        public void BindGroup(GroupClass group_object_val)
        {
            this.groupObject = group_object_val;
        }

        public void SetBrowserThemeIdStr(string str_val)
        {
            this.browserThemeIdStr = str_val;
        }

        public void EnqueuePendingDownLinkData(string data_val)
        {
            this.pendingDownLinkDataQueue.EnqueueData(data_val);
        }

        public string GetPendingDownLinkData()
        {
            return (string) this.pendingDownLinkDataQueue.DequeueData();
        }

        public int GetPendingDownLinkDataCount()
        {
            return this.pendingDownLinkDataQueue.GetQueueLength();
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
