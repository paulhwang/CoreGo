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

    public static class GroupProtocolClass
    {
        //#define GROUP_MGR_DATA_BUFFER_SIZE 512

        public const int GROUP_MGR_PROTOCOL_GROUP_ID_SIZE = 4;
        public const int GROUP_MGR_PROTOCOL_GROUP_INDEX_SIZE = 4;
        public const int GROUP_MGR_PROTOCOL_GROUP_ID_INDEX_SIZE = (GROUP_MGR_PROTOCOL_GROUP_ID_SIZE + GROUP_MGR_PROTOCOL_GROUP_INDEX_SIZE);

    }

    public static class PortProtocolClass
    {
        //#define GROUP_ROOM_PROTOCOL_TRANSPORT_PORT_NUMBER 8009
        //#define BASE_MGR_PROTOCOL_TRANSPORT_PORT_NUMBER 8005
        //#define BASE_MGR_PROTOCOL_TRANSPORT_PORT_NUMBER1 8007

    }

    public class FabricRootClass
    {
        private string objectName = "FabricRootClass";

        private UFabricClass uFabricObject { get; }
        private DFabricClass dFabricObject { get; }
        private LinkMgrClass linkMgrObject { get; }
        private GroupMgrClass groupMgrObject { get; }
        private NameListClass nameListObject { get; }

        public UFabricClass UFabricObject() { return this.uFabricObject; }
        public DFabricClass DFabricObject() { return this.dFabricObject; }
        public LinkMgrClass LinkMgrObject() { return this.linkMgrObject; }
        public GroupMgrClass GroupMgrObject() { return this.groupMgrObject; }
        public NameListClass NameListObject() { return this.nameListObject; }

        public FabricRootClass()
        {
            this.debugIt(true, "FabricRootClass", "init start");
            this.uFabricObject = new UFabricClass(this);
            this.dFabricObject = new DFabricClass(this);
            this.linkMgrObject = new LinkMgrClass(this);
            this.groupMgrObject = new GroupMgrClass(this);
            this.nameListObject = new NameListClass(this);

            this.StartWatchDogThread();

            this.debugIt(true, "FabricRootClass", "init done");
        }

        private void StartWatchDogThread()
        {

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
