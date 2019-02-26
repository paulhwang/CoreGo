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

namespace Phwang.PhwangUtils
{
    public class ListEntryClass
    {
        private int theId { get; set; }
        private object theData { get; set; }
        private int theIndex { get; set; }

        public int Id() { return this.theId; }
        public object Data() { return this.theData; }
        public int Index() { return this.theIndex; }

        public void SetData(int id_val, object data_val, int index_val)
        {
            this.theId = id_val;
            this.theData = data_val;
            this.theIndex = index_val;
        }

    }
}
