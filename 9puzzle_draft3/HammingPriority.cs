using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9puzzle_draft3
{
    class HammingPriority
    {
        private string[] _startArr = new string[9];
        private string[] _goalArr = new string[9];

        public int getHamming(string[] startArr, string[] goalArr)
        {
            for (int i = 0; i < startArr.Length; i++)
            {
                _startArr[i] = startArr[i];
                _goalArr[i] = goalArr[i];
            }

            int hamming = 0;

            for (int i = 0; i < _startArr.Length; i++)
            {
                if(_startArr[i] == _goalArr[i] && _startArr[i] != "0")
                {
                    hamming += 1;
                }
            }
            return hamming;
        }
    }
}
