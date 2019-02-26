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
    public class GoBoardClass
    {
        private string objectName = "GoBoardClass";

        private int[,] theBoardArray { get; }
        private int[,] theMarkedBoardArray { get; }
        private string theBoardOutputBuffer { get; set; }
        int theBlackCapturedStones { get; set; }
        int theWhiteCapturedStones { get; set; }
        int theLastDeadX { get; set; }
        int theLastDeadY { get; set; }
        private GoRootClass theRootObject { get; }

        public GoConfigClass ConfigObject() { return this.theRootObject.ConfigObject(); }
        public GoGameClass GameObject() { return this.theRootObject.GameObject(); }
        public string BoardOutputBuffer() { return this.theBoardOutputBuffer; }
        public int BoardArray(int x_val, int y_val) { return this.theBoardArray[x_val, y_val]; }
        public void AddBlackCapturedStones(int val) { this.theBlackCapturedStones += val; }
        public void AddWhiteCapturedStones(int val) { this.theWhiteCapturedStones += val; }
        public void SetBoardArray(int x_val, int y_val, int data_val) { this.theBoardArray[x_val, y_val] = data_val; }
        public void SetLastDeadStone(int x_val, int y_val) { this.theLastDeadX = x_val; this.theLastDeadY = y_val; }

        public GoBoardClass(GoRootClass root_object_val)
        {
            this.theRootObject = root_object_val;
            this.theBoardArray = new int[GoDefineClass.MAX_BOARD_SIZE, GoDefineClass.MAX_BOARD_SIZE];
            this.theMarkedBoardArray = new int[GoDefineClass.MAX_BOARD_SIZE, GoDefineClass.MAX_BOARD_SIZE];
            this.ResetBoardObjectData();
        }

        const char GO_PROTOCOL_GAME_INFO = 'G';

        public void EncodeBoard()
        {
            this.theBoardOutputBuffer = "";
            this.theBoardOutputBuffer = this.theBoardOutputBuffer + GO_PROTOCOL_GAME_INFO;
            this.theBoardOutputBuffer = this.theBoardOutputBuffer + PhwangUtils.EncodeNumberClass.EncodeNumber(this.GameObject().TotalMoves(), 3);
            this.theBoardOutputBuffer = this.theBoardOutputBuffer + PhwangUtils.EncodeNumberClass.EncodeNumber(this.GameObject().NextColor(), 1);

            int board_size = this.ConfigObject().BoardSize();
            for (int i = 0; i < board_size; i++)
            {
                for (int j = 0; j < board_size; j++)
                {
                    char c = '0';
                    switch (this.theBoardArray[i, j])
                    {
                        case 1: c = '1'; break;
                        case 2: c = '2'; break;
                    }
                    this.theBoardOutputBuffer = this.theBoardOutputBuffer + c;
                }
            }

            this.theBoardOutputBuffer = this.theBoardOutputBuffer + PhwangUtils.EncodeNumberClass.EncodeNumber(this.theBlackCapturedStones, 3);
            this.theBoardOutputBuffer = this.theBoardOutputBuffer + PhwangUtils.EncodeNumberClass.EncodeNumber(this.theWhiteCapturedStones, 3);

            this.theBoardOutputBuffer = this.theBoardOutputBuffer + PhwangUtils.EncodeNumberClass.EncodeNumber(this.theLastDeadX, 2);
            this.theBoardOutputBuffer = this.theBoardOutputBuffer + PhwangUtils.EncodeNumberClass.EncodeNumber(this.theLastDeadY, 2);

            this.debugIt(false, "encodeBoard", this.theBoardOutputBuffer);
        }

        public void AddStoneToBoard(int x_val, int y_val, int color_val)
        {
            if (!this.ConfigObject().IsValidCoordinates(x_val, y_val))
            {
                this.abendIt("addStoneToBoard", "bad coordinate");
                return;
            }

            this.theBoardArray[x_val, y_val] = color_val;
        }

        private bool isEmptySpace(int x_val, int y_val)
        {
            if (!this.theRootObject.ConfigObject().IsValidCoordinates(x_val, y_val))
            {
                return false;
            }
            if (this.theBoardArray[x_val, y_val] != GoDefineClass.GO_EMPTY_STONE)
            {
                return false;
            }
            return true;
        }

        public bool StoneHasAir(int x_val, int y_val)
        {
            if (this.isEmptySpace(x_val, y_val - 1))
            {
                return true;
            }
            if (this.isEmptySpace(x_val, y_val + 1))
            {
                return true;
            }
            if (this.isEmptySpace(x_val - 1, y_val))
            {
                return true;
            }
            if (this.isEmptySpace(x_val + 1, y_val))
            {
                return true;
            }
            return false;
        }

        public void ResetBoardObjectData()
        {
            int board_size = this.ConfigObject().BoardSize();
            for (int i = 0; i < board_size; i++)
            {
                for (int j = 0; j < board_size; j++)
                {
                    this.theBoardArray[i, j] = GoDefineClass.GO_EMPTY_STONE;
                    this.theMarkedBoardArray[i, j] = GoDefineClass.GO_EMPTY_STONE;
                }
            }
            this.theBlackCapturedStones = 0;
            this.theWhiteCapturedStones = 0;
            this.ClearLastDeadStone();
        }

        public void ResetMarkedBoardObjectData()
        {

        }

        public void ClearLastDeadStone()
        {
            this.theLastDeadX = 19;
            this.theLastDeadY = 19;
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
