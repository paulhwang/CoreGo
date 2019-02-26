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
    public class GoGroupClass
    {
        private string objectName = "GoGroupClass";
        GoGroupListClass theGroupListObject;
        private int maxX { get; set; }
        private int minX { get; set; }
        private int maxY { get; set; }
        private int minY { get; set; }
        private int indexNumber { get; set; }
        int stoneCount;
        private int myColor { get; }
        int hisColor { get; set; }
        private bool[,] existMatrix { get; }
        private bool[,] deadMatrix { get; }

        public GoGroupListClass GroupListObject() { return this.theGroupListObject; }
        public GoConfigClass ConfigObject() { return this.theGroupListObject.ConfigObject(); }
        public int HisColor() { return this.hisColor; }
        public int MyColor() { return this.myColor; }
        public int StoneCount() { return this.stoneCount; }
        public int IndexNumber() { return this.indexNumber; }
        public bool ExistMatrix(int x_val, int y_val) { return this.existMatrix[x_val, y_val]; }
        public void SetIndexNumber(int val) { this.indexNumber = val; }
        public void SetGroupListObject(GoGroupListClass group_list_val) { this.theGroupListObject = group_list_val; }


        public GoGroupClass(GoGroupListClass group_list_object_val)
        {
            this.theGroupListObject = group_list_object_val;
            this.indexNumber = this.theGroupListObject.GroupCount();
            this.myColor = this.theGroupListObject.MyColor();
            this.stoneCount = 0;

            this.existMatrix = new bool[GoDefineClass.MAX_BOARD_SIZE, GoDefineClass.MAX_BOARD_SIZE];
            this.deadMatrix = new bool[GoDefineClass.MAX_BOARD_SIZE, GoDefineClass.MAX_BOARD_SIZE];
            this.hisColor = (this.myColor == GoDefineClass.GO_EMPTY_STONE)
                ? GoDefineClass.GO_EMPTY_STONE
                : GoDefineClass.GetOppositeColor(this.myColor);
        }

        public void InsertStoneToGroup(int x_val, int y_val, bool dead_val)
        {
            if (this.existMatrix[x_val, y_val])
            {
                this.abendIt("insertStoneToGroup", "stone already exists in group");
            }

            if (this.stoneCount == 0)
            {
                this.maxX = x_val;
                this.minX = x_val;
                this.maxY = y_val;
                this.minY = y_val;
            }
            else
            {
                if (x_val > this.maxX)
                {
                    this.maxX = x_val;
                }
                if (x_val < this.minX)
                {
                    this.minX = x_val;
                }
                if (y_val > this.maxY)
                {
                    this.maxY = y_val;
                }
                if (y_val < this.minY)
                {
                    this.minY = y_val;
                }
            }

            this.stoneCount++;
            this.existMatrix[x_val, y_val] = true;
            this.deadMatrix[x_val, y_val] = dead_val;
        }

        public bool IsCandidateGroup(int x_val, int y_val)
        {
            int i = this.minX;
            while (i <= this.maxX)
            {
                int j = this.minY;
                while (j <= this.maxY)
                {
                    if (this.existMatrix[i, j])
                    {
                        //this.debug(false, "isCandidateGroup", "(" + x_val + "," + y_val + ") (" + i + "," + j + ")");
                        if (GoStaticClass.isNeighborStone(i, j, x_val, y_val))
                        {
                            return true;
                        }
                    }
                    j += 1;
                }
                i += 1;
            }
            return false;
        }

        public void MergeWithOtherGroup(GoGroupClass group2)
        {
            this.debugIt(false, "mergeWithOtherGroup", "");
            int i = group2.minX;
            while (i <= group2.maxX)
            {
                int j = group2.minY;
                while (j <= group2.maxY)
                {
                    if (group2.existMatrix[i, j])
                    {
                        //this.debug(false, "mergeWithOtherGroup", "i=" + i + " j=" + j);
                        if (this.existMatrix[i, j])
                        {
                            this.abendIt("mergeWithOtherGroup", "already exist");
                        }
                        this.existMatrix[i, j] = group2.existMatrix[i, j];
                        this.stoneCount++;

                        group2.existMatrix[i, j] = false;
                        group2.stoneCount--;
                    }
                    j += 1;
                }
                i += 1;
            }
            if (group2.stoneCount != 0)
            {
                this.abendIt("mergeWithOtherGroup", "theStoneCount");
            }

            if (this.maxX < group2.maxX)
            {
                this.maxX = group2.maxX;
            }
            if (this.minX > group2.minX)
            {
                this.minX = group2.minX;
            }
            if (this.maxY < group2.maxY)
            {
                this.maxY = group2.maxY;
            }
            if (this.minY > group2.minY)
            {
                this.minY = group2.minY;
            }

            if (group2.theGroupListObject.GroupArray(group2.indexNumber) != group2)
            {
                this.abendIt("mergeWithOtherGroup", "group2");
            }
        }

        public bool GroupHasAir()
        {
            int i = this.minX;
            while (i <= this.maxX)
            {
                int j = this.minY;
                while (j <= this.maxY)
                {
                    if (this.existMatrix[i, j])
                    {
                        if (this.theGroupListObject.FightObject().RootObject().BoardObject().StoneHasAir(i, j))
                        {
                            return true;
                        }
                    }
                    j += 1;
                }
                i += 1;
            }
            return false;
        }

        public void RemoveDeadStoneFromBoard()
        {
            int i = this.minX;
            while (i <= this.maxX)
            {
                int j = this.minY;
                while (j <= this.maxY)
                {
                    if (this.existMatrix[i, j])
                    {
                        this.theGroupListObject.FightObject().BoardObject().SetBoardArray(i, j, GoDefineClass.GO_EMPTY_STONE);
                        //this.debug(false, "removeDeadStoneFromBoard", "(" + i + "," + j + ")");
                    }
                    j += 1;
                }
                i += 1;
            }
        }

        public void MarkLastDeadInfo()
        {
             this.theGroupListObject.BoardObject().SetLastDeadStone(this.maxX, this.maxY);

            if (this.maxX != this.minX)
            {
                this.abendIt("MarkLastDeadInfo", "bad x");
            }
            if (this.maxY != this.minY)
            {
                this.abendIt("MarkLastDeadInfo", "bad y");
            }
            if (!this.existMatrix[this.maxX, this.maxY])
            {
                this.abendIt("MarkLastDeadInfo", "exist_matrix");
            }
        }

        public void AbendGroup()
        {
            int count = 0;
            int board_size = this.ConfigObject().BoardSize();
            for (int i = 0; i < board_size; i++)
            {
                for (int j = 0; j < board_size; j++)
                {
                    if (this.existMatrix[i, j])
                    {
                        count++;
                    }
                }
            }
            if (this.stoneCount != count)
            {
                this.abendIt("AbendGroup", "stone count");
            }
        }

        public void AbendOnGroupConflict(GoGroupClass other_group_val)
        {
            int board_size = this.ConfigObject().BoardSize();
            for (int i = 0; i < board_size; i++)
            {
                for (int j = 0; j < board_size; j++)
                {
                    if (this.existMatrix[i, j])
                    {
                        if (other_group_val.existMatrix[i, j])
                        {
                            this.abendIt("AbendOnGroupConflict", "stone  exists in 2 groups");
                            //this->abend("abendOnGroupConflict", "stone (" + i + "," + j + ") exists in 2 groups: (" + this.myColor() + ":" + this.indexNumber() + ":" + this.stoneCount() + ") ("
                            //    + other_group_val.myColor() + ":" + other_group_val.indexNumber() + ":" + other_group_val.stoneCount() + ")");
                        }
                    }
                }
            }
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
