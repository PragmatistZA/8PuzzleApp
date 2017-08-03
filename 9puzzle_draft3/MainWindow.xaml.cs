using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using Microsoft.Win32;
using System.Media;
using System.Windows.Threading;

namespace _9puzzle_draft3
{
    public partial class MainWindow : Window
    {
        #region Notes

        /*  TODO:
         *  -Add intelligence (Decide on separate class or not, etc) - kid of done
         *  -Find way to detect impossible board states --Done!
         *  -Buy cigarettes (Done)
         *  - ...Buy more cigarettes
         */

        #endregion

        #region Instance Variables

        private int nrMoves = 0;

        public string _nrNull = "0";
        private string startString = "";
        private string selectedNumber = null;
        private string movesString = "";
        private string[] nrsArray = new string[9];
        private string[] goalArray = { "1", "2", "3", "4", "5", "6", "7", "8", "0" };
        private bool startFlag = false;
        private bool winFlag = false;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private List<string> nrsList = new List<string>();
        private List<string> movesMadeList = new List<string>();

        private StateSpaceSearch GenerateSolution;

        private Button selectedButton;
        private Button nullButton = null;
        private Button[] btnArray = new Button[9];

        private bool devTabFlag = false;
        private bool firstClick = false;

        #endregion

        #region Main

        public MainWindow()
        {
            InitializeComponent();
            btnBotRight.Focus();
            PopulateBoard();
            this.Height = 338;
        }

        #endregion

        #region Methods

        //keeps track of players and their scores
        private void LeaderBoard()
        {
            //erm, getting to this. 
        }

        //saves current contents of each block as a string, xxxxxxxx, and adds to moves made list
        private void AddBoardState()
        {
            string tempString = "";
            tempString = btnTopLeft.Content.ToString() + btnTopMid.Content.ToString() + btnTopRight.Content.ToString()
             + btnMidLeft.Content.ToString() + btnMiddle.Content.ToString() + btnMidRight.Content.ToString()
             + btnBotLeft.Content.ToString() + btnBotMid.Content.ToString() + btnBotRight.Content.ToString();
            movesMadeList.Add(tempString);
            lbxMovesMade.Items.Add(tempString);
            lbxMovesMade.Height += 20;
        }

        //Fills board state with array
        private void FillBoard()
        {
            //Sets button content to match nrArray 
            for (int i = 0; i < nrsArray.Length; i++)
            {
                btnArray[i].Content = nrsArray[i];
            }

            for (int i = 0; i < nrsArray.Length; i++)
            {
                movesString += nrsArray[i];
            }

            movesString += Environment.NewLine;

            btnBotRight.Focus();
        }

        private void WriteToArray()
        {
            //Sets array content to match buttons
            for (int i = 0; i < nrsArray.Length; i++)
            {
                nrsArray[i] = (string)btnArray[i].Content;
            }
        }

        //Creates random number list and fills grid
        private void PopulateBoard()
        {
            RandomNumberList randNrsList = new RandomNumberList(1, 9, false);

            nrsList = randNrsList.nrsList;

            //fills 2D array with randomized number list
            for (int i = 0; i < 9; i++)
            {
                nrsArray[i] = nrsList[i];
                lblArray.Content += nrsList[i];
            }

            //Board board = new Board(nrsArray);

            if (!checkSolvable())
            {
                PopulateBoard();
            }
            else
            {
                //Binds Buttons in grid to btnArray string array
                btnArray[0] = btnTopLeft;
                btnArray[1] = btnTopMid;
                btnArray[2] = btnTopRight;
                btnArray[3] = btnMidLeft;
                btnArray[4] = btnMiddle;
                btnArray[5] = btnMidRight;
                btnArray[6] = btnBotLeft;
                btnArray[7] = btnBotMid;
                btnArray[8] = btnBotRight;

                //calls method to fill grid content with randomized numbers array
                FillBoard();

                //dev tab start board state string 
                startString = lblArray.Content.ToString();
            }
        }

        //Checks if randomly generated board state is solvable
        private bool checkSolvable()
        {
            int[] arr = new int[9];
            for (int i = 0; i < nrsArray.Length; i++)
            {
                arr[i] = Convert.ToInt32(nrsArray[i]);
            }
            int inv_count = getInvCount(arr);
            if (inv_count%2 == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //enables all button blocks
        private void EnableAll()
        {
            btnTopLeft.IsEnabled = true;
            btnTopMid.IsEnabled = true;
            btnTopRight.IsEnabled = true;
            btnMidLeft.IsEnabled = true;
            btnMiddle.IsEnabled = true;
            btnMidRight.IsEnabled = true;
            btnBotLeft.IsEnabled = true;
            btnBotMid.IsEnabled = true;
            btnBotRight.IsEnabled = true;
        }

        //checks current board state against goal state and shows a message if the game has been won
        private void CheckWin()
        {
            if ((string)btnTopLeft.Content == goalArray[0] &&
                (string)btnTopMid.Content == goalArray[1] &&
                (string)btnTopRight.Content == goalArray[2] &&
                (string)btnMidLeft.Content == goalArray[3] &&
                (string)btnMiddle.Content == goalArray[4] &&
                (string)btnMidRight.Content == goalArray[5] &&
                (string)btnBotLeft.Content == goalArray[6] &&
                (string)btnBotMid.Content == goalArray[7] &&
                (string)btnBotRight.Content == goalArray[8])
            {
                MessageBox.Show("Game solved in " + nrMoves + " moves!");

                lblMoveCount.Content = nrMoves;
                winFlag = true;
            }
            nrMoves += 1;
            lblMoveCount.Content = nrMoves;
        }

        //Checks if move user wants to make is valid
        private void CheckMove(Button btn)
        {
            selectedButton.Content = _nrNull;
            btn.Content = selectedNumber;
            WriteToArray();
            selectedNumber = null;
            firstClick = false;
            EnableAll();
            CheckWin();

            movesString += btnTopLeft.Content.ToString() + btnTopMid.Content.ToString() + btnTopRight.Content.ToString()
                         + btnMidLeft.Content.ToString() + btnMiddle.Content.ToString() + btnMidRight.Content.ToString()
                         + btnBotLeft.Content.ToString() + btnBotMid.Content.ToString() + btnBotRight.Content.ToString()
                         + Environment.NewLine;
            AddBoardState();
        }

        //not sure if this needs to be here, too scared to remove
        private void CheckSelect(Button btn)
        {
            firstClick = true;
            EnableAll();
            btn.IsEnabled = false;
            selectedNumber = btn.Content.ToString();
            selectedButton = btn;
        }

        //counts number of inversions for a block
        public int getInvCount(int[] arr)
        {
            arr = arr.Reverse().Skip(1).Reverse().ToArray();
            int inv_count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        inv_count++;
                    }
                }
            }
            return inv_count;
        }

        //Returns blank space's position in array
        private int getBlankSpace()
        {
            int blankSpace = 0;
            if ((string)btnTopLeft.Content == _nrNull)
            {
                blankSpace = 0;
            }
            else if ((string)btnTopMid.Content == _nrNull)
            {
                blankSpace = 1;
            }
            else if ((string)btnTopRight.Content == _nrNull)
            {
                blankSpace = 2;
            }
            else if ((string)btnMidLeft.Content == _nrNull)
            {
                blankSpace = 3;
            }
            else if ((string)btnMiddle.Content == _nrNull)
            {
                blankSpace = 4;
            }
            else if ((string)btnMidRight.Content == _nrNull)
            {
                blankSpace = 5;
            }
            else if ((string)btnBotLeft.Content == _nrNull)
            {
                blankSpace = 6;
            }
            else if ((string)btnBotMid.Content == _nrNull)
            {
                blankSpace = 7;
            }
            else if ((string)btnBotRight.Content == _nrNull)
            {
                blankSpace = 8;
            }
            //for debugging
            else
            {
                MessageBox.Show("Error with getBlankSpace method.");
            }
            return blankSpace;
        }

        //this was a bad idea. Should probably remove.
        private void setNullButton()
        {
            if ((string)btnTopLeft.Content == _nrNull)
            {
                nullButton = btnTopLeft;
            }
            else if ((string)btnTopMid.Content == _nrNull)
            {
                nullButton = btnTopMid;
            }
            else if ((string)btnTopRight.Content == _nrNull)
            {
                nullButton = btnTopRight;
            }
            else if ((string)btnMidLeft.Content == _nrNull)
            {
                nullButton = btnMidLeft;
            }
            else if ((string)btnMiddle.Content == _nrNull)
            {
                nullButton = btnMiddle;
            }
            else if ((string)btnMidRight.Content == _nrNull)
            {
                nullButton = btnMidRight;
            }
            else if ((string)btnBotLeft.Content == _nrNull)
            {
                nullButton = btnBotLeft;
            }
            else if ((string)btnBotMid.Content == _nrNull)
            {
                nullButton = btnBotMid;
            }
            else if ((string)btnMidRight.Content == _nrNull)
            {
                nullButton = btnMidRight;
            }
        }

        //methods that move blocks to the blank space if possible
        //so many switches, I regret my decisions in life
        private bool moveRight()
        {
            string tempPos = "";
            int position = 0;
            position = getBlankSpace();
            switch (position)
            {
                case 0:
                    SystemSounds.Beep.Play();
                    return false;
                case 1:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 1];
                    nrsArray[position - 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 2:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 1];
                    nrsArray[position - 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 3:
                    SystemSounds.Beep.Play();
                    return false;
                case 4:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 1];
                    nrsArray[position - 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 5:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 1];
                    nrsArray[position - 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 6:
                    SystemSounds.Beep.Play();
                    return false;
                case 7:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 1];
                    nrsArray[position - 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 8:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 1];
                    nrsArray[position - 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                default:
                    return false;
            }
        }

        private bool moveLeft()
        {
            string tempPos = "";
            int position = 0;
            position = getBlankSpace();
            switch (position)
            {
                case 0:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 1];
                    nrsArray[position + 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 1:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 1];
                    nrsArray[position + 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 2:
                    SystemSounds.Beep.Play();
                    return false;
                case 3:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 1];
                    nrsArray[position + 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 4:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 1];
                    nrsArray[position + 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 5:
                    SystemSounds.Beep.Play();
                    return false;
                case 6:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 1];
                    nrsArray[position + 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 7:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 1];
                    nrsArray[position + 1] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true; ;
                case 8:
                    SystemSounds.Beep.Play();
                    return false;
                default:
                    return false;
            }
        }

        private bool moveUp()
        {
            string tempPos = "";
            int position = 0;
            position = getBlankSpace();
            switch (position)
            {
                case 0:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 3];
                    nrsArray[position + 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 1:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 3];
                    nrsArray[position + 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 2:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 3];
                    nrsArray[position + 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 3:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 3];
                    nrsArray[position + 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 4:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 3];
                    nrsArray[position + 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 5:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position + 3];
                    nrsArray[position + 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 6:
                    SystemSounds.Beep.Play();
                    return false;
                case 7:
                    SystemSounds.Beep.Play();
                    return false;
                case 8:
                    SystemSounds.Beep.Play();
                    return false;
                default:
                    return false;
            }
        }

        private bool moveDown()
        {
            string tempPos = "";
            int position = 0;
            position = getBlankSpace();
            switch (position)
            {
                case 0:
                    SystemSounds.Beep.Play();
                    return false;
                case 1:
                    SystemSounds.Beep.Play();
                    return false;
                case 2:
                    SystemSounds.Beep.Play();
                    return false;
                case 3:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 3];
                    nrsArray[position - 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 4:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 3];
                    nrsArray[position - 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 5:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 3];
                    nrsArray[position - 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 6:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 3];
                    nrsArray[position - 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 7:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 3];
                    nrsArray[position - 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                case 8:
                    tempPos = nrsArray[position];
                    nrsArray[position] = nrsArray[position - 3];
                    nrsArray[position - 3] = tempPos;
                    FillBoard();
                    CheckWin();
                    return true;
                default:
                    return false;
            }
        }

        #endregion

        #region Buttons

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to leave?" + Environment.NewLine + "Game progress may be lost.", "Exit Game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Start New Game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow newWindow = new MainWindow();

                this.Close();
                newWindow.ShowDialog();
            }
            dispatcherTimer.Stop();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var path = "";
            string createText = btnTopLeft.Content.ToString() + btnTopMid.Content.ToString() + btnTopRight.Content.ToString()
                              + btnMidLeft.Content.ToString() + btnMiddle.Content.ToString() + btnMidRight.Content.ToString()
                              + btnBotLeft.Content.ToString() + btnBotMid.Content.ToString() + btnBotRight.Content.ToString() + Environment.NewLine;

            MessageBoxResult result = MessageBox.Show("Do you want to save your current game?" + Environment.NewLine + "(Please save game as .csv)", "Save Game", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (result == MessageBoxResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, movesString);
                path = saveFileDialog.FileName;
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            var path = "";
            string startGameString = "";
            string goalString = "";
            string[] goalState;
            string[] startState;
            List<string> startStateList = new List<string>();
            List<string> goalStateList = new List<string>();
            string[] tempArray = new string[9];
            char[] delimiterChars = { ' ', ',', '.', '"', '/'};
            char[] startCharArray;
            char[] goalCharArray;

            MessageBoxResult result = MessageBox.Show("Do you want to load a saved game?", "Load Game", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (result == MessageBoxResult.OK)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;

                    startGameString = File.ReadLines(path).First();
                    startCharArray = startGameString.ToCharArray();
                    for (int i = 0; i < startCharArray.Length; i++)
                    {
                        if (startCharArray[i] != 'B' && startCharArray[i] != '"' && startCharArray[i] != '/' && startCharArray[i] != ',')
                        {
                            startStateList.Add(Convert.ToString(startCharArray[i]));
                        }
                        else if (startCharArray[i] == 'B')
                        {
                            startStateList.Add("0");
                        }
                    }
                    startState = startStateList.ToArray();

                    for (int i = 0; i < startState.Length; i++)
                    {
                        nrsArray[i] = startState[i];
                    }

                    goalString = File.ReadLines(path).Last();
                    goalCharArray = goalString.ToCharArray();
                    for (int i = 0; i < goalCharArray.Length; i++)
                    {
                        if (goalCharArray[i] != 'B' && goalCharArray[i] != '"' && goalCharArray[i] != '/' && goalCharArray[i] != ',')
                        {
                            goalStateList.Add(Convert.ToString(goalCharArray[i]));
                        }
                        else if (goalCharArray[i] == 'B')
                        {
                            goalStateList.Add("0");
                        }
                    }
                    goalState = goalStateList.ToArray();

                    for (int i = 0; i < goalState.Length; i++)
                    {
                        goalArray[i] = goalState[i];
                    }

                    movesString = File.ReadAllText(path);

                    FillBoard();

                    MessageBox.Show("Game successfully loaded!");
                }
            }
        }

        private void btnTopLeft_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnTopMid.Content == _nrNull || (string)btnMidLeft.Content == _nrNull))
            {
                CheckSelect(btnTopLeft);
            }
            else if (firstClick && (string)btnTopLeft.Content == _nrNull)
            {
                CheckMove(btnTopLeft);
            }
        }

        private void btnTopMid_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnMiddle.Content == _nrNull || (string)btnTopLeft.Content == _nrNull || (string)btnTopRight.Content == _nrNull))
            {
                CheckSelect(btnTopMid);
            }
            else if (firstClick && (string)btnTopMid.Content == _nrNull)
            {
                if (selectedButton == btnMiddle || selectedButton == btnTopLeft || selectedButton == btnTopRight)
                {
                    CheckMove(btnTopMid);
                }
            }
        }

        private void btnTopRight_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnTopMid.Content == _nrNull || (string)btnMidRight.Content == _nrNull))
            {
                CheckSelect(btnTopRight);
            }
            else if (firstClick && (string)btnTopRight.Content == _nrNull)
            {
                if (selectedButton == btnTopMid || selectedButton == btnMidRight)
                {
                    CheckMove(btnTopRight);
                }
            }
        }

        private void btnMidLeft_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnMiddle.Content == _nrNull || (string)btnTopLeft.Content == _nrNull || (string)btnBotLeft.Content == _nrNull))
            {
                CheckSelect(btnMidLeft);
            }
            else if (firstClick && (string)btnMidLeft.Content == _nrNull)
            {
                if (selectedButton == btnMiddle || selectedButton == btnTopLeft || selectedButton == btnBotLeft)
                {
                    CheckMove(btnMidLeft);
                }
            }
        }

        private void btnMiddle_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnMidRight.Content == _nrNull || (string)btnMidLeft.Content == _nrNull || (string)btnTopMid.Content == _nrNull || (string)btnBotMid.Content == _nrNull))
            {
                CheckSelect(btnMiddle);
            }
            else if (firstClick && (string)btnMiddle.Content == _nrNull)
            {
                if (selectedButton == btnMidRight || selectedButton == btnMidLeft || selectedButton == btnTopMid || selectedButton == btnBotMid)
                {
                    CheckMove(btnMiddle);
                }
            }
        }

        private void btnMidRight_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnMiddle.Content == _nrNull || (string)btnTopRight.Content == _nrNull || (string)btnBotRight.Content == _nrNull))
            {
                CheckSelect(btnMidRight);
            }
            else if (firstClick && (string)btnMidRight.Content == _nrNull)
            {
                if (selectedButton == btnMiddle || selectedButton == btnTopRight || selectedButton == btnBotRight)
                {
                    CheckMove(btnMidRight);
                }
            }
        }

        private void btnBotLeft_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnBotMid.Content == _nrNull| (string)btnMidLeft.Content == _nrNull))
            {
                CheckSelect(btnBotLeft);
            }
            else if (firstClick && (string)btnBotLeft.Content == _nrNull)
            {
                if (selectedButton == btnBotMid || selectedButton == btnMidLeft)
                {
                    CheckMove(btnBotLeft);
                }
            }
        }

        private void btnBotMid_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnMiddle.Content == _nrNull || (string)btnBotLeft.Content == _nrNull || (string)btnBotRight.Content == _nrNull))
            {
                CheckSelect(btnBotMid);
            }
            else if (firstClick && (string)btnBotMid.Content == _nrNull)
            {
                CheckMove(btnBotMid);
            } 
        }

        private void btnBotRight_Click(object sender, RoutedEventArgs e)
        {
            if (!firstClick && ((string)btnMidRight.Content == _nrNull || (string)btnBotMid.Content == _nrNull))
            {
                CheckSelect(btnBotRight);
            }
            else if (firstClick && (string)btnBotRight.Content == _nrNull)
            {
                if (selectedButton == btnBotMid || selectedButton == btnMidRight)
                {
                    CheckMove(btnBotRight);
                }
            }
        }

        private void btnDev_Click(object sender, RoutedEventArgs e)
        {
            if (!devTabFlag)
            {
                this.Height = 600;
                devTabFlag = true;
            }
            else if (devTabFlag)
            {
                this.Height = 365;
                devTabFlag = false;     
            }  
        }

        private void btnForceNoSolution_Click(object sender, RoutedEventArgs e)
        {
            selectedButton = null;
            selectedNumber = null;
            firstClick = false;
            btnTopLeft.Content = "1";
            btnTopMid.Content = "2";
            btnTopRight.Content = "3";
            btnMidLeft.Content = "4";
            btnMiddle.Content = "5";
            btnMidRight.Content = "6";
            btnBotLeft.Content = "8";
            btnBotMid.Content = "7";
            btnBotRight.Content = _nrNull;
            CheckWin();
        }

        private void btnForceWinState_Click(object sender, RoutedEventArgs e)
        {
            selectedButton = null;
            selectedNumber = null;
            firstClick = false;
            btnTopLeft.Content = "1";
            btnTopMid.Content = "2";
            btnTopRight.Content = "3";
            btnMidLeft.Content = "4";
            btnMiddle.Content = "5";
            btnMidRight.Content = "6";
            btnBotLeft.Content = "7";
            btnBotMid.Content = "8";
            btnBotRight.Content = _nrNull;
            CheckWin();
        }

        private void btnSolution_Click(object sender, RoutedEventArgs e)
        {

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            dispatcherTimer.Start();

            if (!startFlag)
            {
                GenerateSolution = new StateSpaceSearch(nrsArray, goalArray);
                startFlag = true;
            }
            do
            {
                GenerateSolution.FindSolution();
                for (int i = 0; i < GenerateSolution.solutionStack.Peek().getInfo().Length; i++)
                {
                    nrsArray[i] = GenerateSolution.solutionStack.Peek().Value[i];
                }

                FillBoard();
                CheckWin();
                //if (GenerateSolution.solutionQueue.Count == 2000)
                //{
                //    MessageBox.Show("Invalid Board State");
                //    dispatcherTimer.Stop();
                //    break;
                //}
            } while (!winFlag);
            btnBotRight.Focus();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (GenerateSolution.solutionQueue.Count != 0)
            {
                for (int i = 0; i < GenerateSolution.solutionQueue.Peek().getInfo().Length; i++)
                {
                    nrsArray[i] = GenerateSolution.solutionQueue.Peek().Value[i];
                }

                GenerateSolution.solutionQueue.Dequeue();

                FillBoard();
            }
            else
            {
                dispatcherTimer.Stop();
                MessageBox.Show("Finished solution.");
            }
        }

        #endregion

        #region Key Handlers

        //allows use of arrow keys
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            setNullButton();
            e.Handled = true;
            if (e.Key == Key.Right)
            {
                moveRight();
            }
            else if (e.Key == Key.Left)
            {
                moveLeft();
            }
            else if (e.Key == Key.Up)
            {
                moveUp();
            }
            else if (e.Key == Key.Down)
            {
                moveDown();
            }
        }

        #endregion

        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }
    }
}
