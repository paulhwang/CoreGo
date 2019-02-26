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
    public class GoRootClass
    {
        private string objectName = "GoRootClass";

        private GoConfigClass configObject { get; }
        private GoBoardClass boardObject { get; }
        private GoGameClass gameObject { get; }
        private GoParseClass parseObject { get; }
        private GoFightClass fightObject { get; }

        public GoConfigClass ConfigObject() { return this.configObject; }
        public GoBoardClass BoardObject() { return this.boardObject; }
        public GoGameClass GameObject() { return this.gameObject; }
        public GoParseClass ParseObject() { return this.parseObject; }
        public GoFightClass FightObject() { return this.fightObject; }

        public GoRootClass()
        {
            this.configObject = new GoConfigClass(this);
            this.boardObject = new GoBoardClass(this);
            this.gameObject = new GoGameClass(this);
            this.fightObject = new GoFightClass(this);
            this.parseObject = new GoParseClass(this);
        }

        public string DoSetup(string input_data_val)
        {
            this.configObject.ConfigIt(input_data_val);
            return null;
        }

        public string ProcessInputData(string input_data_val)
        {
            this.parseObject.ParseInputData(input_data_val);
            this.boardObject.EncodeBoard();
            this.debugIt(true, "transmitBoardData", this.boardObject.BoardOutputBuffer());
            return this.boardObject.BoardOutputBuffer();
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
