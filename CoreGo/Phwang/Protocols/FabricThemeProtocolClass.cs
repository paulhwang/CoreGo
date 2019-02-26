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

namespace Phwang.Protocols
{
    public class FabricThemeProtocolClass
    {
        public const int GROUP_ROOM_PROTOCOL_TRANSPORT_PORT_NUMBER = 8009;

        public const string FABRIC_THEME_PROTOCOL_COMMAND_IS_SETUP_ROOM = "R";
        public const string FABRIC_THEME_PROTOCOL_RESPOND_IS_SETUP_ROOM = "r";
        public const string FABRIC_THEME_PROTOCOL_COMMAND_IS_PUT_ROOM_DATA = "D";
        public const string FABRIC_THEME_PROTOCOL_RESPOND_IS_PUT_ROOM_DATA = "d";

        public const int FABRIC_GROUP_ID_SIZE = 4;
    }
}
