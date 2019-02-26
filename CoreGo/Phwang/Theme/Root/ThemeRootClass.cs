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

namespace Phwang.Theme
{
    public class ThemeRootClass
    {
        private string objectName = "ThemeRootClass";

        private UThemeClass uThemeObject { get; }
        private DThemeClass dThemeObject { get; }
        private RoomMgrClass roomMgrObject { get; }

        public UThemeClass UThemeObject() { return this.uThemeObject; }
        public DThemeClass DThemeObject() { return this.dThemeObject; }

        public RoomMgrClass RoomMgrObject() { return this.roomMgrObject; }


        public ThemeRootClass()
        {
            this.uThemeObject = new UThemeClass(this);
            this.dThemeObject = new DThemeClass(this);
            this.roomMgrObject = new RoomMgrClass(this);
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
