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

namespace Phwang.Go
{
    public class GoProtocolClass
    {
        //#define GO_PROTOCOL_GAME_INFO 'G'
        //#define GO_PROTOCOL_TIME_INFO 'T'
        //#define GO_PROTOCOL_CHAT_INFO 'C'
        public const char GO_PROTOCOL_MOVE_COMMAND = 'M';
        public const char GO_PROTOCOL_PASS_COMMAND = 'P';
        public const char GO_PROTOCOL_BACKWARD_COMMAND = 'b';
        public const char GO_PROTOCOL_DOUBLE_BACKWARD_COMMAND = 'B';
        public const char GO_PROTOCOL_FORWARD_COMMAND = 'f';
        public const char GO_PROTOCOL_DOUBLE_FORWARD_COMMAND = 'F';
        //#define GO_PROTOCOL_RESIGN_COMMAND 'R'
        //#define GO_PROTOCOL_CONFIRM_COMMAND 'C'
        //#define GO_PROTOCOL_CONTINUE_COMMAND 'c'
    }
}
