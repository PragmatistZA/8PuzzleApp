using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9puzzle_draft3
{
    class StateSpaceSearch
    {
        private string[] startState = new string[9];
        private string[] currentState = new string[9];
        private string[] previousState = new string[9];
        private string[] goalState = new string[9];


        public Queue<Node<string[]>> solutionQueue;
        public Stack<Node<string[]>> solutionStack;

        public Node<string[]> bestNode;

        public Board previousBoard;
        public Board bestBoard;

        private string[] possibleState = new string[9];

        private Random rand = new Random();

        public StateSpaceSearch(string[] startState, string[] goalState)
        {
            for (int i = 0; i < startState.Length; i++)
            {
                this.startState[i] = startState[i];
                currentState[i] = startState[i];
                this.goalState[i] = goalState[i];
                possibleState[i] = null;
                previousState[i] = null;
            }
            solutionQueue = new Queue<Node<string[]>>();
            solutionStack = new Stack<Node<string[]>>();
            bestNode = new Node<string[]>(this.startState);
            solutionStack.Push(bestNode);
        }

        public void FindSolution()
        {
            string[] bestBoardState = new string[9];
            string[] initialState = new string[9];

            Board currentBoard = new Board(currentState, goalState);

            for (int i = 0; i < currentState.Length; i++)
            {
                initialState[i] = currentState[i];
                bestBoardState[i] = null;
            }
            Board[] possibleBoards = new Board[4];
            for (int i = 0; i < possibleBoards.Length; i++)
            {
                possibleBoards[i] = null;
            }
            switch (currentBoard.blankSpace)
            {
                case 0:
                    possibleBoards[0] = new Board(moveLeft(initialState), goalState);
                    possibleBoards[2] = new Board(moveUp(initialState), goalState);
                    break;
                case 1:
                    possibleBoards[1] = new Board(moveRight(initialState), goalState);
                    possibleBoards[0] = new Board(moveLeft(initialState), goalState);
                    possibleBoards[2] = new Board(moveUp(initialState), goalState);
                    break;
                case 2:
                    possibleBoards[1] = new Board(moveRight(initialState), goalState);
                    possibleBoards[2] = new Board(moveUp(initialState), goalState);
                    break;
                case 3:
                    possibleBoards[0] = new Board(moveLeft(initialState), goalState);
                    possibleBoards[2] = new Board(moveUp(initialState), goalState);
                    possibleBoards[3] = new Board(moveDown(initialState), goalState);
                    break;
                case 4:
                    possibleBoards[1] = new Board(moveRight(initialState), goalState);
                    possibleBoards[0] = new Board(moveLeft(initialState), goalState);
                    possibleBoards[2] = new Board(moveUp(initialState), goalState);
                    possibleBoards[3] = new Board(moveDown(initialState), goalState);
                    break;
                case 5:
                    possibleBoards[2] = new Board(moveUp(initialState), goalState);
                    possibleBoards[3] = new Board(moveDown(initialState), goalState);
                    possibleBoards[1] = new Board(moveRight(initialState), goalState);
                    break;
                case 6:
                    possibleBoards[0] = new Board(moveLeft(initialState), goalState);
                    possibleBoards[3] = new Board(moveDown(initialState), goalState);
                    break;
                case 7:
                    possibleBoards[1] = new Board(moveRight(initialState), goalState);
                    possibleBoards[0] = new Board(moveLeft(initialState), goalState);
                    possibleBoards[3] = new Board(moveDown(initialState), goalState);
                    break;
                case 8:
                    possibleBoards[3] = new Board(moveDown(initialState), goalState);
                    possibleBoards[1] = new Board(moveRight(initialState), goalState);
                    break;
                default:
                    break;
            }
            List<Board> hBoards = new List<Board>();
            List<double> hMinValueList = new List<double>();
            List<double> values = new List<double>();

            for (int i = 0; i < possibleBoards.Length; i++)
            {
                hBoards.Add(possibleBoards[i]);
                if (previousBoard != null)
                {
                    if (possibleBoards[i] != null && (possibleBoards[i].ToStringBoardState() != previousBoard.ToStringBoardState()))
                    {
                        hMinValueList.Add(possibleBoards[i].boardHeuristic);
                        values.Add(possibleBoards[i].boardHeuristic);
                    }
                    else
                    {
                        hMinValueList.Add(99);
                        values.Add(99);
                    }
                }
                else
                {
                    if (possibleBoards[i] != null)
                    {
                        hMinValueList.Add(possibleBoards[i].boardHeuristic);
                        values.Add(possibleBoards[i].boardHeuristic);
                    }
                    else
                    {
                        hMinValueList.Add(99);
                        values.Add(99);
                    }
                }
            }

            double min = values.Min();
            int indexMin = values.IndexOf(min);
            int indexMin2;
            int random;
            values[indexMin] = 99;
            if (values.Contains(min))
            {
                indexMin2 = values.IndexOf(min);
                random = rand.Next(1, 3);
                if (random == 1)
                {
                    for (int i = 0; i < hBoards[indexMin2].boardState.Length; i++)
                    {
                        bestBoardState[i] = hBoards[indexMin2].boardState[i];
                        previousState[i] = currentState[i];
                        currentState[i] = bestBoardState[i];
                        initialState[i] = bestBoardState[i];
                        currentBoard.boardState[i] = bestBoardState[i];
                    }
                }
                else
                {
                    for (int i = 0; i < hBoards[hMinValueList.IndexOf(hMinValueList.Min())].boardState.Length; i++)
                    {
                        bestBoardState[i] = hBoards[hMinValueList.IndexOf(hMinValueList.Min())].boardState[i];
                        previousState[i] = currentState[i];
                        currentState[i] = bestBoardState[i];
                        initialState[i] = bestBoardState[i];
                        currentBoard.boardState[i] = bestBoardState[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < hBoards[hMinValueList.IndexOf(hMinValueList.Min())].boardState.Length; i++)
                {
                    bestBoardState[i] = hBoards[hMinValueList.IndexOf(hMinValueList.Min())].boardState[i];
                    previousState[i] = currentState[i];
                    currentState[i] = bestBoardState[i];
                    initialState[i] = bestBoardState[i];
                    currentBoard.boardState[i] = bestBoardState[i];
                }
            }
            previousBoard = new Board(previousState, goalState);
            bestBoard = new Board(bestBoardState, goalState);
            bestNode = new Node<string[]>(bestBoardState);
            solutionStack.Push(bestNode);
            solutionQueue.Enqueue(bestNode);
        }

        private string[] moveRight(string[] possibleState)
        {
            string[] boardState = new string[9];
            for (int i = 0; i < possibleState.Length; i++)
            {
                boardState[i] = possibleState[i];
            }
            string tempPos = "";
            int position = 0;
            Board testBoard = new Board(possibleState, goalState);
            position = testBoard.blankSpace;
            switch (position)
            {
                case 0:
                    break;
                case 1:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 1];
                    boardState[position - 1] = tempPos;
                    break;
                case 2:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 1];
                    boardState[position - 1] = tempPos;
                    break;
                case 3:
                    break;
                case 4:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 1];
                    boardState[position - 1] = tempPos;
                    break;
                case 5:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 1];
                    boardState[position - 1] = tempPos;
                    break;
                case 6:
                    break;
                case 7:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 1];
                    boardState[position - 1] = tempPos;
                    break;
                case 8:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 1];
                    boardState[position - 1] = tempPos;
                    break;
                default:
                    break;
            }
            return boardState;
        }

        private string[] moveLeft(string[] possibleState)
        {
            string[] boardState = new string[9];
            for (int i = 0; i < possibleState.Length; i++)
            {
                boardState[i] = possibleState[i];
            }
            string tempPos = "";
            int position = 0;
            Board testBoard = new Board(possibleState, goalState);
            position = testBoard.blankSpace;
            switch (position)
            {
                case 0:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 1];
                    boardState[position + 1] = tempPos;
                    break;
                case 1:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 1];
                    boardState[position + 1] = tempPos;
                    break;
                case 2:
                    break;
                case 3:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 1];
                    boardState[position + 1] = tempPos;
                    break;
                case 4:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 1];
                    boardState[position + 1] = tempPos;
                    break;
                case 5:
                    break;
                case 6:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 1];
                    boardState[position + 1] = tempPos;
                    break;
                case 7:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 1];
                    boardState[position + 1] = tempPos;
                    break;
                case 8:
                    break;
                default:
                    break;
            }
            return boardState;
        }

        private string[] moveUp(string[] possibleState)
        {
            string[] boardState = new string[9];
            for (int i = 0; i < possibleState.Length; i++)
            {
                boardState[i] = possibleState[i];
            }
            string tempPos = "";
            int position = 0;
            Board testBoard = new Board(possibleState, goalState);
            position = testBoard.blankSpace;
            switch (position)
            {
                case 0:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 3];
                    boardState[position + 3] = tempPos;
                    break;
                case 1:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 3];
                    boardState[position + 3] = tempPos;
                    break;
                case 2:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 3];
                    boardState[position + 3] = tempPos;
                    break;
                case 3:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 3];
                    boardState[position + 3] = tempPos;
                    break;
                case 4:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 3];
                    boardState[position + 3] = tempPos;
                    break;
                case 5:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position + 3];
                    boardState[position + 3] = tempPos;
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                default:
                    break;
            }
            return boardState;
        }

        private string[] moveDown(string[] possibleState)
        {
            string[] boardState = new string[9];
            for (int i = 0; i < possibleState.Length; i++)
            {
                boardState[i] = possibleState[i];
            }
            Board testBoard = new Board(possibleState, goalState);
            string tempPos = "";
            int position = 0;
            position = testBoard.blankSpace;
            switch (position)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 3];
                    boardState[position - 3] = tempPos;
                    break;
                case 4:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 3];
                    boardState[position - 3] = tempPos;
                    break;
                case 5:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 3];
                    boardState[position - 3] = tempPos;
                    break;
                case 6:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 3];
                    boardState[position - 3] = tempPos;
                    break;
                case 7:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 3];
                    boardState[position - 3] = tempPos;
                    break;
                case 8:
                    tempPos = boardState[position];
                    boardState[position] = boardState[position - 3];
                    boardState[position - 3] = tempPos;
                    break;
                default:
                    break;
            }
            return boardState;
        }
    }
}
