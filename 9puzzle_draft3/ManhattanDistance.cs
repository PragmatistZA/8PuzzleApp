using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9puzzle_draft3
{
    class ManhattanDistance
    {
        private string[] _startArr = new string[9];
        private string[] _goalArr = new string[9];

        public int getManhattan(string[] startArr, string[] goalArr)
        {
            for (int i = 0; i < startArr.Length; i++)
            {
                _startArr[i] = startArr[i];
                _goalArr[i] = goalArr[i];
            }

            int manhattan = 0;
            int arrayStartIndex = 0;
            int arrayGoalIndex = 0;

            for (int i = 0; i < _startArr.Length; i++)
            {
                arrayStartIndex = Array.IndexOf(_goalArr, Convert.ToString(i));
                arrayGoalIndex = Array.IndexOf(_startArr, Convert.ToString(i));

                if (arrayStartIndex == arrayGoalIndex)
                {
                    manhattan += 0;
                }
                else if (
                             (arrayStartIndex == 0 && arrayGoalIndex == 1) ||
                             (arrayStartIndex == 0 && arrayGoalIndex == 3) ||
                             (arrayStartIndex == 1 && arrayGoalIndex == 2) ||
                             (arrayStartIndex == 1 && arrayGoalIndex == 4) ||
                             (arrayStartIndex == 2 && arrayGoalIndex == 5) ||
                             (arrayStartIndex == 3 && arrayGoalIndex == 4) ||
                             (arrayStartIndex == 3 && arrayGoalIndex == 6) ||
                             (arrayStartIndex == 4 && arrayGoalIndex == 5) ||
                             (arrayStartIndex == 4 && arrayGoalIndex == 7) ||
                             (arrayStartIndex == 5 && arrayGoalIndex == 8) ||
                             (arrayStartIndex == 6 && arrayGoalIndex == 7) ||
                             (arrayStartIndex == 7 && arrayGoalIndex == 8) ||

                             (arrayGoalIndex == 0 && arrayStartIndex == 1) ||
                             (arrayGoalIndex == 0 && arrayStartIndex == 3) ||
                             (arrayGoalIndex == 1 && arrayStartIndex == 2) ||
                             (arrayGoalIndex == 1 && arrayStartIndex == 4) ||
                             (arrayGoalIndex == 2 && arrayStartIndex == 5) ||
                             (arrayGoalIndex == 3 && arrayStartIndex == 4) ||
                             (arrayGoalIndex == 3 && arrayStartIndex == 6) ||
                             (arrayGoalIndex == 4 && arrayStartIndex == 5) ||
                             (arrayGoalIndex == 4 && arrayStartIndex == 7) ||
                             (arrayGoalIndex == 5 && arrayStartIndex == 8) ||
                             (arrayGoalIndex == 6 && arrayStartIndex == 7) ||
                             (arrayGoalIndex == 7 && arrayStartIndex == 8)
                        )
                {
                    manhattan += 1;
                }

                else if (
                             (arrayStartIndex == 0 && arrayGoalIndex == 2) ||
                             (arrayStartIndex == 0 && arrayGoalIndex == 4) ||
                             (arrayStartIndex == 0 && arrayGoalIndex == 6) ||
                             (arrayStartIndex == 1 && arrayGoalIndex == 3) ||
                             (arrayStartIndex == 1 && arrayGoalIndex == 5) ||
                             (arrayStartIndex == 1 && arrayGoalIndex == 7) ||
                             (arrayStartIndex == 2 && arrayGoalIndex == 4) ||
                             (arrayStartIndex == 2 && arrayGoalIndex == 8) ||
                             (arrayStartIndex == 3 && arrayGoalIndex == 5) ||
                             (arrayStartIndex == 3 && arrayGoalIndex == 7) ||
                             (arrayStartIndex == 4 && arrayGoalIndex == 6) || 
                             (arrayStartIndex == 4 && arrayGoalIndex == 8) ||
                             (arrayStartIndex == 5 && arrayGoalIndex == 7) ||
                             (arrayStartIndex == 6 && arrayGoalIndex == 6) ||
                             (arrayStartIndex == 6 && arrayGoalIndex == 8) ||
                             
                             (arrayGoalIndex == 0 && arrayStartIndex == 2) ||
                             (arrayGoalIndex == 0 && arrayStartIndex == 4) ||
                             (arrayGoalIndex == 0 && arrayStartIndex == 6) ||
                             (arrayGoalIndex == 1 && arrayStartIndex == 3) ||
                             (arrayGoalIndex == 1 && arrayStartIndex == 5) ||
                             (arrayGoalIndex == 1 && arrayStartIndex == 7) ||
                             (arrayGoalIndex == 2 && arrayStartIndex == 4) ||
                             (arrayGoalIndex == 2 && arrayStartIndex == 8) ||
                             (arrayGoalIndex == 3 && arrayStartIndex == 5) ||
                             (arrayGoalIndex == 3 && arrayStartIndex == 7) ||
                             (arrayGoalIndex == 4 && arrayStartIndex == 6) ||
                             (arrayGoalIndex == 4 && arrayStartIndex == 8) ||
                             (arrayGoalIndex == 5 && arrayStartIndex == 7) ||
                             (arrayGoalIndex == 6 && arrayStartIndex == 6) ||
                             (arrayGoalIndex == 6 && arrayStartIndex == 8)
                        )
                {
                    manhattan += 2;
                }

                else if (
                             (arrayStartIndex == 0 && arrayGoalIndex == 5) ||
                             (arrayStartIndex == 0 && arrayGoalIndex == 7) ||
                             (arrayStartIndex == 1 && arrayGoalIndex == 6) ||
                             (arrayStartIndex == 1 && arrayGoalIndex == 8) ||
                             (arrayStartIndex == 2 && arrayGoalIndex == 3) ||
                             (arrayStartIndex == 2 && arrayGoalIndex == 7) ||
                             (arrayStartIndex == 3 && arrayGoalIndex == 8) ||
                             (arrayStartIndex == 5 && arrayGoalIndex == 6) ||
                             
                             (arrayGoalIndex == 0 && arrayStartIndex == 5) ||
                             (arrayGoalIndex == 0 && arrayStartIndex == 7) ||
                             (arrayGoalIndex == 1 && arrayStartIndex == 6) ||
                             (arrayGoalIndex == 1 && arrayStartIndex == 8) ||
                             (arrayGoalIndex == 2 && arrayStartIndex == 3) ||
                             (arrayGoalIndex == 2 && arrayStartIndex == 7) ||
                             (arrayGoalIndex == 3 && arrayStartIndex == 8) ||
                             (arrayGoalIndex == 5 && arrayStartIndex == 6)
                        )
                {
                    manhattan += 3;
                }

                else if (
                             (arrayStartIndex == 0 && arrayGoalIndex == 8) ||
                             (arrayStartIndex == 2 && arrayGoalIndex == 6) ||
                             (arrayGoalIndex == 0 && arrayStartIndex == 8) ||
                             (arrayGoalIndex == 2 && arrayStartIndex == 6)
                        )
                {
                    manhattan += 4;
                }

            }
            return manhattan;
        }
    }
}
