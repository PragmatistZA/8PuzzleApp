using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9puzzle_draft3
{
    class RandomNumberList
    {
        #region Notes

        /*  TODO:
         *  -Make the numbers list randomized a property instead of returning from method. (Keep getting stack overflow goddamnit) -- FIXED!
         *  -Wine to celebrate
         */

        #endregion

        #region Global Variables and Accessor/Mutator Properties

        public int lowerBound { get; set; }
        public int upperBound { get; set; }
        public bool repeatingNumbers { get; set; }
        public List<string> nrsList { get; set; }

        #endregion

        #region Constructors

        //Default Constructor
        public RandomNumberList()
        {
            lowerBound = 0;
            upperBound = 0;
            repeatingNumbers = false;
            nrsList.Clear();
        }

        //Overload Contstructor with parameters for bounds to randomize numbers between
        public RandomNumberList(int lowerBound, int upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
            Randomize();
        }

        //Overload Constructor with bool to check if randomized array should contain repeating numbers
        public RandomNumberList(int lowerBound, int upperBound, bool repeatingNumbers)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
            this.repeatingNumbers = repeatingNumbers;
            Randomize();
        }

        //Populates list with randomized numbers within arguments from constructor
        public List<string> Randomize()
        {
            string tempRandNum = "";
            nrsList = new List<string>();
            Random randNum = new Random();
            do
            {
                tempRandNum = Convert.ToString(randNum.Next(lowerBound, upperBound));
                if (!repeatingNumbers)
                {
                    if (!nrsList.Contains(tempRandNum))
                    {
                        nrsList.Add(tempRandNum);
                    }
                }
                else if (repeatingNumbers)
                {
                    nrsList.Add(tempRandNum);
                }

            } while (nrsList.Count < 8);

            nrsList.Add("0");

            return nrsList;
        }

        #endregion
    }
}
