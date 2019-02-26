/*
 ******************************************************************************
 *                                       
 *  Copyright (c) 2018 phwang. All rights reserved.
 *
 ******************************************************************************
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Phwang.Fabric
{
    public class DFabricParserClass
    {
        private string objectName = "DFabricParserClass";
        private string RESPONSE_IS_GET_LINK_DATA_NAME_LIST = Protocols.FabricFrontEndProtocolClass.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_NAME_LIST;

        private DFabricClass dFabricObject { get; }
        private DFabricResponseClass dFabricResponseObject { get; }

        public FabricRootClass FabricRootObject() { return this.dFabricObject.FabricRootObject(); }
        private UFabricClass UFabricObject() { return this.FabricRootObject().UFabricObject(); }
        private LinkMgrClass LinkMgrObject() { return this.FabricRootObject().LinkMgrObject(); }
        private GroupMgrClass GroupMgrObject() { return this.FabricRootObject().GroupMgrObject(); }

        public DFabricParserClass(DFabricClass dfabric_object_val)
        {
            this.dFabricObject = dfabric_object_val;
            this.dFabricResponseObject = new DFabricResponseClass(this);
        }

        [DataContract]
        public class AjaxFabricRequestFormatClass
        {
            [DataMember]
            public string command { get; set; }

            [DataMember]
            public int packet_id { get; set; }

            [DataMember]
            public string data { get; set; }
        }

        public void parseInputPacket(string input_data_val)
        {
            string adax_id = input_data_val.Substring(0, Protocols.FabricFrontEndProtocolClass.AJAX_MAPING_ID_SIZE);
            string toDes = input_data_val.Substring(Protocols.FabricFrontEndProtocolClass.AJAX_MAPING_ID_SIZE);
            AjaxFabricRequestFormatClass ajax_fabric_request;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(toDes)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(AjaxFabricRequestFormatClass));
                ajax_fabric_request = (AjaxFabricRequestFormatClass)deseralizer.ReadObject(ms);
                this.debugIt(true, "ParseAjaxPacket", "input_data_var = " + input_data_val);
                this.debugIt(true, "ParseAjaxPacket", "command = " + ajax_fabric_request.command);
                this.debugIt(true, "ParseAjaxPacket", "data = " + ajax_fabric_request.data);
            }

            string response_data;
            if (ajax_fabric_request.command == "setup_link")
            {
                response_data = this.processSetupLinkRequest(ajax_fabric_request.data);
            }
            else if (ajax_fabric_request.command == "get_link_data")
            {
                response_data = this.processGetLinkDataRequest(ajax_fabric_request.data);
            }
            else if (ajax_fabric_request.command == "get_name_list")
            {
                response_data = this.processGetNameListRequest(ajax_fabric_request.data);
            }
            else if (ajax_fabric_request.command == "setup_session")
            {
                response_data = this.processSetupSessionRequest(ajax_fabric_request.data);
            }
            else if (ajax_fabric_request.command == "setup_session2")
            {
                response_data = this.processSetupSession2Request(ajax_fabric_request.data);
            }
            else if (ajax_fabric_request.command == "setup_session3")
            {
                response_data = this.processSetupSession3Request(ajax_fabric_request.data);
            }
            else if (ajax_fabric_request.command == "put_session_data")
            {
                response_data = this.processPutSessionDataRequest(ajax_fabric_request.data);
            }
            else if (ajax_fabric_request.command == "get_session_data")
            {
                response_data = this.processGetSessionDataRequest(ajax_fabric_request.data);
            }
            else
            {
                response_data = "command " + ajax_fabric_request.command + " not supported";
            }
            this.dFabricObject.TransmitData(adax_id + response_data);
        }

        [DataContract]
        public class SetupLinkRequestFormatClass
        {
            [DataMember]
            public string my_name { get; set; }

            [DataMember]
            public string password { get; set; }
        }

        private string processSetupLinkRequest(string input_data_val)
        {
            this.debugIt(true, "processSetupLinkRequest", "input_data_val = " + input_data_val);
            SetupLinkRequestFormatClass format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(SetupLinkRequestFormatClass));
                format_data = (SetupLinkRequestFormatClass)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processSetupLinkRequest", "my_name = " + format_data.my_name);
                this.debugIt(true, "processSetupLinkRequest", "password = " + format_data.password);
            }

            LinkClass link = this.LinkMgrObject().MallocLink(format_data.my_name);
            string response_data = this.dFabricResponseObject.GenerateSetupLinkResponse(link.LinkIdStr(), link.MyName());
            return response_data;
        }

        [DataContract]
        public class GetLinkDataRequestFormat
        {
            [DataMember]
            public int link_id { get; set; }
        }

        private string processGetLinkDataRequest(string input_data_val)
        {
            this.debugIt(true, "processGetLinkDataRequest", "input_data_val = " + input_data_val);
            GetLinkDataRequestFormat format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(GetLinkDataRequestFormat));
                format_data = (GetLinkDataRequestFormat)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processGetLinkDataRequest", "link_id = " + format_data.link_id);
            }

            LinkClass link = this.LinkMgrObject().GetLinkById(format_data.link_id);

            string downlink_data = RESPONSE_IS_GET_LINK_DATA_NAME_LIST + this.FabricRootObject().NameListObject().NameListTagStr();

            int max_session_table_array_index = link.GetSessionArrayMaxIndex();
            PhwangUtils.ListEntryClass[] session_table_array = link.GetSessionArrayEntryTable();
            string pending_session_data = "";
            for (int i = 0; i <= max_session_table_array_index; i++)
            {
                PhwangUtils.ListEntryClass list_entry = session_table_array[i];
                SessionClass session = (SessionClass)list_entry.Data();
                if (session != null)
                {
                   if (session.GetPendingDownLinkDataCount() > 0)
                    {
                        downlink_data = downlink_data + Protocols.FabricFrontEndProtocolClass.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_DATA + link.LinkIdStr() + session.SessionIdStr();
                    }
                }
            }

            /*

            int max_session_table_array_index = phwnagListMgrGetMaxIndex(link->sessionListMgrObject(), "DFabricClass::processGetLinkDataRequest()");
            SessionClass** session_table_array = (SessionClass**)phwangListMgrGetEntryTableArray(link->sessionListMgrObject());
            for (int i = 0; i <= max_session_table_array_index; i++)
            {
                SessionClass* session = session_table_array[i];
                if (session)
                {
                    char* pending_downlink_data = session->getPendingDownLinkData();
                    if (pending_downlink_data)
                    {
                        *data_ptr++ = WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_DATA;
                        session->enqueuePendingDownLinkData(pending_downlink_data);
                        strcpy(data_ptr, link->linkIdIndex());
                        data_ptr += LINK_MGR_PROTOCOL_LINK_ID_INDEX_SIZE;
                        strcpy(data_ptr, session->sessionIdIndex());
                        data_ptr += LINK_MGR_PROTOCOL_SESSION_ID_INDEX_SIZE;
                        this->debug(true, "==================processGetLinkData getPendingDownLinkData", downlink_data);
                    }
                }
            }
            */




            string pending_session_setup = "";
            string pending_session_str = link.getPendingSessionSetup();
            if (pending_session_str != null)
            {
                pending_session_setup = pending_session_setup + Protocols.FabricFrontEndProtocolClass.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_SESSION;
                pending_session_setup = pending_session_setup + pending_session_str;
            }

            string pending_session_str3 = link.getPendingSessionSetup3();
            if (pending_session_str3 != null)
            {
                pending_session_setup = pending_session_setup + Protocols.FabricFrontEndProtocolClass.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_SESSION3;
                pending_session_setup = pending_session_setup + pending_session_str3;
            }

            downlink_data = downlink_data + pending_session_setup;

            string response_data = this.dFabricResponseObject.GenerateGetLinkDataResponse(link.LinkIdStr(), downlink_data, pending_session_setup);
            return response_data;
        }

        [DataContract]
        public class GetNameListRequestFormat
        {
            [DataMember]
            public int link_id { get; set; }
            public int name_list_tag { get; set; }
        }

        private string processGetNameListRequest(string input_data_val)
        {
            this.debugIt(true, "processGetNameListRequest", "input_data_val = " + input_data_val);
            GetNameListRequestFormat format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(GetNameListRequestFormat));
                format_data = (GetNameListRequestFormat)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processGetNameListRequest", "link_id = " + format_data.link_id);
            }

            LinkClass link = this.LinkMgrObject().GetLinkById(format_data.link_id);

            string name_list = this.FabricRootObject().NameListObject().GetNameList(format_data.name_list_tag);

            string response_data = this.dFabricResponseObject.GenerateGetNameListResponse(link.LinkIdStr(), name_list);
            return response_data;
        }

        [DataContract]
        public class SetupSessionRequestFormat
        {
            [DataMember]
            public string link_id { get; set; }

            [DataMember]
            public string his_name { get; set; }

            [DataMember]
            public string theme_data { get; set; }
        }

        private string processSetupSessionRequest(string input_data_val)
        {
            this.debugIt(true, "processSetupSessionRequest", "input_data_val = " + input_data_val);
            SetupSessionRequestFormat format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(SetupSessionRequestFormat));
                format_data = (SetupSessionRequestFormat)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processSetupSessionRequest", "link_id = " + format_data.link_id);
                this.debugIt(true, "processSetupSessionRequest", "his_name = " + format_data.his_name);
                this.debugIt(true, "processSetupSessionRequest", "theme_data = " + format_data.theme_data);
            }

            string theme_id_str = format_data.theme_data.Substring(0, Protocols.FabricFrontEndProtocolClass.BROWSER_THEME_ID_SIZE);
            string theme_data = format_data.theme_data.Substring(Protocols.FabricFrontEndProtocolClass.BROWSER_THEME_ID_SIZE);

            LinkClass link = this.LinkMgrObject().GetLinkByIdStr(format_data.link_id);
            SessionClass session = link.MallocSession();
            session.SetBrowserThemeIdStr(theme_id_str);
            GroupClass group = this.GroupMgrObject().MallocGroup(theme_data);
            if (group == null)
            {
                return this.errorProcessSetupSession(format_data.link_id, "null group");
            }
            group.InsertSession(session);
            session.BindGroup(group);

            if (format_data.his_name == link.MyName())
            {
                this.mallocRoom(group, theme_data);
            }
            else
            {
                LinkClass his_link = this.LinkMgrObject().GetLinkByMyName(format_data.his_name);
                if (his_link == null)
                {
                    return this.errorProcessSetupSession(format_data.link_id, "his_link does not exist");
                }
                SessionClass his_session = his_link.MallocSession();
                if (his_session == null)
                {
                    return this.errorProcessSetupSession(format_data.link_id, "null his_session");
                }

                group.InsertSession(his_session);
                his_session.BindGroup(group);

                his_link.SetPendingSessionSetup(his_link.LinkIdStr() + his_session.SessionIdStr(), theme_data);
            }
            //char* data_ptr;
            //char* downlink_data = data_ptr = (char*)phwangMalloc(LINK_MGR_DATA_BUFFER_SIZE + 4, "DFS1");
            //*data_ptr++ = WEB_FABRIC_PROTOCOL_RESPOND_IS_SETUP_SESSION;
            //memcpy(data_ptr, ajax_id, WEB_FABRIC_PROTOCOL_AJAX_ID_SIZE);
            //data_ptr += WEB_FABRIC_PROTOCOL_AJAX_ID_SIZE;
            //strcpy(data_ptr, session->sessionIdIndex());
            //this->transmitFunction(tp_transfer_object_val, downlink_data);

            string response_data = this.dFabricResponseObject.GenerateSetupSessionResponse(link.LinkIdStr(), session.SessionIdStr());
            return response_data;
        }

        private string errorProcessSetupSession(string link_id_val, string error_msg_val)
        {
            return error_msg_val;
        }

        [DataContract]
        public class SetupSession2RequestFormat
        {
            [DataMember]
            public int link_id { get; set; }

            [DataMember]
            public int session_id { get; set; }

            [DataMember]
            public string theme_id_str { get; set; }

            [DataMember]
            public string accept { get; set; }

            [DataMember]
            public string theme_data { get; set; }
        }

        private string processSetupSession2Request(string input_data_val)
        {
            this.debugIt(true, "processSetupSession3Request", "input_data_val = " + input_data_val);
            SetupSession2RequestFormat format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(SetupSession2RequestFormat));
                format_data = (SetupSession2RequestFormat)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processSetupSession3Request", "link_id = " + format_data.link_id);
                this.debugIt(true, "processSetupSession3Request", "session_id = " + format_data.session_id);
                this.debugIt(true, "processSetupSession3Request", "theme_id = " + format_data.theme_id_str);
                this.debugIt(true, "processSetupSession3Request", "accept = " + format_data.accept);
                this.debugIt(true, "processSetupSession3Request", "theme_data = " + format_data.theme_data);
            }

            LinkClass link = this.LinkMgrObject().GetLinkById(format_data.link_id);
            if (link == null)
            {
                return errorProcessSetupSession2(format_data.link_id, "null link");
            }
            SessionClass session = link.SessionMgrObject().GetSessionBySessionId(format_data.session_id);
            if (session == null)
            {
                return errorProcessSetupSession2(format_data.link_id, "null session");
            }
            session.SetBrowserThemeIdStr(format_data.theme_id_str);
            GroupClass group = session.GroupObject();
            if (group == null)
            {
                return errorProcessSetupSession2(format_data.link_id, "null group");
            }
            this.mallocRoom(group, format_data.theme_data);

            string response_data = this.dFabricResponseObject.GenerateSetupSession2Response(link.LinkIdStr(), session.SessionIdStr(), session.BrowserThemeIdStr());
            return response_data;
        }

        private string errorProcessSetupSession2(int link_id_val, string error_msg_val)
        {
            return error_msg_val;
        }

        [DataContract]
        public class SetupSession3RequestFormat
        {
            [DataMember]
            public int link_id { get; set; }

            [DataMember]
            public int session_id { get; set; }
        }

        private string processSetupSession3Request(string input_data_val)
        {
            this.debugIt(true, "processSetupSession3Request", "input_data_val = " + input_data_val);
            SetupSession3RequestFormat format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(SetupSession3RequestFormat));
                format_data = (SetupSession3RequestFormat)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processSetupSession3Request", "link_id = " + format_data.link_id);
                this.debugIt(true, "processSetupSession3Request", "session_id = " + format_data.session_id);
            }

            LinkClass link = this.LinkMgrObject().GetLinkById(format_data.link_id);
            SessionClass session = link.SessionMgrObject().GetSessionBySessionId(format_data.session_id);
            if (session == null)
            {
                return errorProcessSetupSession3(format_data.link_id, "null session");
            }

            string response_data = this.dFabricResponseObject.GenerateSetupSession3Response(link.LinkIdStr(), session.SessionIdStr(), session.BrowserThemeIdStr());
            return response_data;
        }

        private string errorProcessSetupSession3(int link_id_val, string error_msg_val)
        {
            return error_msg_val;
        }

        [DataContract]
        public class PutSessionDataRequestFormat
        {
            [DataMember]
            public int link_id { get; set; }

            [DataMember]
            public int session_id { get; set; }

            [DataMember]
            public int xmt_seq { get; set; }

            [DataMember]
            public string data { get; set; }
        }

        private string processPutSessionDataRequest(string input_data_val)
        {
            this.debugIt(true, "processPutSessionDataRequest", "input_data_val = " + input_data_val);
            PutSessionDataRequestFormat format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(PutSessionDataRequestFormat));
                format_data = (PutSessionDataRequestFormat)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processPutSessionDataRequest", "link_id = " + format_data.link_id);
                this.debugIt(true, "processPutSessionDataRequest", "session_id = " + format_data.session_id);
                this.debugIt(true, "processPutSessionDataRequest", "xmt_seq = " + format_data.xmt_seq);
                this.debugIt(true, "processPutSessionDataRequest", "data = " + format_data.data);
            }

            LinkClass link = this.LinkMgrObject().GetLinkById(format_data.link_id);
            SessionClass session = link.SessionMgrObject().GetSessionBySessionId(format_data.session_id);
            if (session == null)
            {
                return errorProcessPutSessionData(format_data.link_id, "null session");
            }
            string room_id_str = session.GroupObject().RoomIdStr();
            if (room_id_str == null)
            {
                return this.errorProcessPutSessionData(format_data.link_id, "null room");
            }

            /* transfer data up */
            string uplink_data = Protocols.FabricThemeProtocolClass.FABRIC_THEME_PROTOCOL_COMMAND_IS_PUT_ROOM_DATA;
            //uplink_data = uplink_data + room_id_str + link.LinkIdStr() + session.SessionIdStr();
            uplink_data = uplink_data + room_id_str + format_data.data;
            this.UFabricObject().TransmitData(uplink_data);

            /* send the response down */
            string response_data = this.dFabricResponseObject.GeneratePutSessionDataResponse(link.LinkIdStr(), session.SessionIdStr(), "job is done");
            return response_data;
        } 
        private string errorProcessPutSessionData(int link_id_val, string error_msg_val)
        {
            return error_msg_val;
        }

        [DataContract]
        public class GetSessionDataRequestFormat
        {
            [DataMember]
            public string link_id { get; set; }

            [DataMember]
            public string session_id { get; set; }
        }

        private string processGetSessionDataRequest(string input_data_val)
        {
            this.debugIt(true, "processGetSessionDataRequest", "input_data_val = " + input_data_val);
            GetSessionDataRequestFormat format_data;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(input_data_val)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(GetSessionDataRequestFormat));
                format_data = (GetSessionDataRequestFormat)deseralizer.ReadObject(ms);// //反序列化ReadObject
                this.debugIt(true, "processGetSessionDataRequest", "link_id = " + format_data.link_id);
                this.debugIt(true, "processGetSessionDataRequest", "session_id = " + format_data.session_id);
            }

            LinkClass link = this.LinkMgrObject().GetLinkByIdStr(format_data.link_id);
            if (link == null)
            {
                return errorProcessGetSessionData(format_data.link_id, "null link");
            }

            SessionClass session = link.SessionMgrObject().GetSessionByIdStr(format_data.session_id);
            if (session == null)
            {
                return errorProcessGetSessionData(format_data.session_id, "null session");
            }

            string data = session.GetPendingDownLinkData();

            /* send the response down */
            string response_data = this.dFabricResponseObject.GenerateGetSessionDataResponse(link.LinkIdStr(), session.SessionIdStr(), data);
            return response_data;
        }

        private string errorProcessGetSessionData(string link_id_val, string error_msg_val)
        {
            return error_msg_val;
        }

        private void mallocRoom(GroupClass group_val, string theme_info_val)
        {
            string uplink_data = Protocols.FabricThemeProtocolClass.FABRIC_THEME_PROTOCOL_COMMAND_IS_SETUP_ROOM;
            uplink_data = uplink_data + group_val.GroupIdStr();
            uplink_data = uplink_data + theme_info_val;
            this.FabricRootObject().UFabricObject().TransmitData(uplink_data);

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
