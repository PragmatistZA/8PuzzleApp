using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9puzzle_draft3
{
    class Board
    {
        public string[] boardState { get; set; }
        public string[] goalState = new string[9];
        public double boardHamming { get; set; }
        public double boardManhattan { get; set; }
        public int blankSpace { get; set; }
        public double boardHeuristic { get; set; } 

        private HammingPriority hammingPriority = new HammingPriority();
        private ManhattanDistance manhattanDistance = new ManhattanDistance();

        private string _nrNull = "0";

        public Board()
        {
            boardState = new string[9];
            for (int i = 0; i < boardState.Length; i++)
            {
                boardState[i] = null;
                goalState[i] = null;
            }
            blankSpace = 0;
            boardHamming = 0;
            boardManhattan = 0;
            boardHeuristic = 0;
        }

        public Board(string[] startArr, string[] goalArr)
        {
            boardState = new string[9];
            for (int i = 0; i < startArr.Length; i++)
            {
                boardState[i] = startArr[i];
                goalState[i] = goalArr[i];
            }
            getBlankSpace();
            setBoardHamming();
            setBoardManhattan();
            setBoardHeuristic();
        }

        public void setBoardHamming()
        {
            boardHamming = hammingPriority.getHamming(boardState, goalState);
        }

        public void setBoardManhattan()
        {
            boardManhattan = manhattanDistance.getManhattan(boardState, goalState);
        }

        public void setBoardHeuristic()
        {
            if (boardHamming != 0)
            {
                boardHeuristic = boardManhattan / boardHamming;
                //boardHeuristic = boardHamming;
            }
            else
            {
                boardHeuristic = boardManhattan - 1;
                //boardHeuristic = boardHamming;
            }
        }

        public void getBlankSpace()
        {
            if (boardState[0] == _nrNull)
            {
                blankSpace = 0;
            }
            else if (boardState[1] == _nrNull)
            {
                blankSpace = 1;
            }
            else if (boardState[2] == _nrNull)
            {
                blankSpace = 2;
            }
            else if (boardState[3] == _nrNull)
            {
                blankSpace = 3;
            }
            else if (boardState[4] == _nrNull)
            {
                blankSpace = 4;
            }
            else if (boardState[5] == _nrNull)
            {
                blankSpace = 5;
            }
            else if (boardState[6] == _nrNull)
            {
                blankSpace = 6;
            }
            else if (boardState[7] == _nrNull)
            {
                blankSpace = 7;
            }
            else if (boardState[8] == _nrNull)
            {
                blankSpace = 8;
            }
        }

        public string ToStringBoardState()
        {
            string toString = "";
            for (int i = 0; i < boardState.Length; i++)
            {
                toString += boardState[i];
            }
            return toString;
        }
    }
}
