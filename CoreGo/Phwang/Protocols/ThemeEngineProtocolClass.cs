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
    public class ThemeEngineProtocolClass
    {
        public const int BASE_MGR_PROTOCOL_TRANSPORT_PORT_NUMBER = 8005;

        public const int THEME_ROOM_ID_SIZE = 4;
        public const int ENGINE_BASE_ID_SIZE = 4;

        public const string THEME_ENGINE_PROTOCOL_COMMAND_IS_SETUP_BASE = "B";
        public const string THEME_ENGINE_PROTOCOL_RESPOND_IS_SETUP_BASE = "b";
        public const string THEME_ENGINE_PROTOCOL_COMMAND_IS_PUT_BASE_DATA = "D";
        public const string THEME_ENGINE_PROTOCOL_RESPOND_IS_PUT_BASE_DATA = "d";
    }
}
