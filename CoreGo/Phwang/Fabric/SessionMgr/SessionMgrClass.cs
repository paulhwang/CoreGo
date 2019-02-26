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
    public class SessionMgrClass
    {
        private string objectName = "SessionMgrClass";
        private const int FIRST_SESSION_ID = 3000;

        private LinkClass linkObject { get; }
        private PhwangUtils.ListMgrClass listMgr { get; }

        //public PhwangUtils.ListMgrClass ListMgr() { return this.listMgr; }
        public int GetSessionArrayMaxIndex() { return this.listMgr.MaxIndex(); }
        public PhwangUtils.ListEntryClass[] GetSessionArrayEntryTable() { return this.listMgr.EntryTableArray(); }

        public SessionMgrClass(LinkClass link_object_val)
        {
            this.linkObject = link_object_val;
            this.listMgr = new PhwangUtils.ListMgrClass(this.objectName, FIRST_SESSION_ID);
        }

        public SessionClass MallocSession()
        {
            SessionClass session = new SessionClass(this.linkObject);
            PhwangUtils.ListEntryClass list_entry = this.listMgr.MallocEntry(session);
            session.BindListEntry(list_entry);
            return session;
        }

        public SessionClass GetSessionByIdStr(string session_id_str_val)
        {
            int session_id = PhwangUtils.EncodeNumberClass.DecodeNumber(session_id_str_val);

            return this.GetSessionBySessionId(session_id);
        }

        public SessionClass GetSessionBySessionId(int id_val)
        {
            PhwangUtils.ListEntryClass list_entry = this.listMgr.GetEntryById(id_val);
            if (list_entry == null)
            {
                return null;
            }
            SessionClass session = (SessionClass)list_entry.Data();

            return session;
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
