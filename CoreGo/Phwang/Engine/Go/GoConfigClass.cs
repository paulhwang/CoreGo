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
    public class GoConfigClass
    {
        private string objectName = "GoConfigClass";

        private GoRootClass theRootObject { get; }
        private int boardSize { get; set; }
        private int handicapPoint { get; set; }
        private int komiPoint { get; set; }

        public int BoardSize() { return this.boardSize; }
        public int HandicapPoint() { return this.handicapPoint; }
        public int KomiPoint() { return this.komiPoint; }

        public GoConfigClass(GoRootClass root_object_val)
        {
            this.theRootObject = root_object_val;
        }

        public void ConfigIt(string input_data_val)
        {
            string len_str = input_data_val.Substring(0,3);
            string board_size_str = input_data_val.Substring(3, 2);
            string handicap_str = input_data_val.Substring(5, 2);
            string komi_str = input_data_val.Substring(7, 2);

            this.boardSize = PhwangUtils.EncodeNumberClass.DecodeNumber(board_size_str);
            this.handicapPoint = PhwangUtils.EncodeNumberClass.DecodeNumber(handicap_str);
            this.komiPoint = PhwangUtils.EncodeNumberClass.DecodeNumber(komi_str);


            int len = input_data_val.Length;//to be deleted
            string name = input_data_val.Substring(10);//to be deleted
        }

        private bool isValidCoordinate(int coordinate_val)
        {
            return (0 <= coordinate_val) && (coordinate_val < this.boardSize);
        }
        public bool IsValidCoordinates(int x_val, int y_val)
        {
            return this.isValidCoordinate(x_val) && this.isValidCoordinate(y_val);
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
