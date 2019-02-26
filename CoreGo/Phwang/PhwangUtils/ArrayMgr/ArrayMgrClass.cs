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
    public class ArrayMgrClass
    {
        private string objectName = "ArrayMgrClass";

        private char arrayType { get; }
        private int arraySize { get; set; }
        private int maxArraySize { get; }
        private object[] objectArrayTable { get; set; }
        private string[] stringArrayTable { get; set; }
        private int[] intArrayTable { get; set; }
        private char [] charArrayTable { get; set; }

        public int ArraySize() { return this.arraySize; }
        public object[] ObjectArrayTable() { return this.objectArrayTable; }
        public string[] StringArrayTable() { return this.stringArrayTable; }
        public int[] IntArrayTable() { return this.intArrayTable; }
        public char[] CharArrayTable() { return this.charArrayTable; }


        public ArrayMgrClass(string owner_object_name_val, char array_type_val, int max_array_size_val)
        {
            this.arrayType = array_type_val;
            this.maxArraySize = max_array_size_val;
            this.allocateArrayTable();
        }
        private void allocateArrayTable()
        {
            switch (this.arrayType)
            {
                case 'o': // object
                    this.objectArrayTable = new object[this.maxArraySize];
                    break;

                case 's': // string
                    this.stringArrayTable = new string[this.maxArraySize];
                    break;

                case 'i': // integer
                    this.intArrayTable = new int[this.maxArraySize];
                    break;

                case 'c': // char
                    this.charArrayTable = new char[this.maxArraySize];
                    break;

                default:
                    this.abendIt("allocArrayTable", "bad type");
                    break;
            }
        }
        public void InsertObjectElement(object object_val)
        {
            int i = 0;
            while (i < this.arraySize)
            {
                if (this.objectArrayTable[i] == null)
                {
                    this.objectArrayTable[i] = object_val;
                    return;
                }
                i++;
            }

            if (this.arraySize < this.maxArraySize)
            {
                this.objectArrayTable[this.arraySize] = object_val;
                this.arraySize++;
                return;
            }

            this.abendIt("insertObjectElement", "table is full");
        }

        public void RemoveObjectElement(object object_val)
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
