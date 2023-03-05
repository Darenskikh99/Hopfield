using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hopfield
{
    class HopfieldAI : INotifyPropertyChanged
    {
        public const int MAXSIZE = 25;

        List<Dictionary> dictionaries= new List<Dictionary>();

        private double[] _newLetter = new double[MAXSIZE];
        private double[] _testedLetter = new double[MAXSIZE];
        private double[] _resultLetter = new double[MAXSIZE];
        private double[,] _weightMatrix = new double[MAXSIZE, MAXSIZE];


        public double[] NewLetter
        {
            get { return _newLetter; }
            set
            {
                _newLetter = value;
                OnPropertyChanged("NewLetter");
            }
        }

        public double[] TestedLetter
        {
            get { return _testedLetter; }
            set
            {
                _testedLetter = value;
                OnPropertyChanged("TestedLetter");
            }
        }

        public double[] ResultLetter
        {
            get { return _resultLetter; }
            set
            {
                _resultLetter = value;
                OnPropertyChanged("ResultLetter");
            }
        }

        public double[,] WeightMatrix
        {
            get { return _weightMatrix; }
            set
            {
                _weightMatrix = value;
                OnPropertyChanged("WeightMatrix");
            }
        }

        public double[] CreateLetter(CustomButton[] btn)
        {
            double[] newLetter = new double[MAXSIZE];
            for (var i = 0; i < MAXSIZE; i++)
            {
                if (btn[i].Index == true)
                {
                    newLetter[i] = 1;
                }
                else
                {
                    newLetter[i] = -1;
                }
            }
            return newLetter;
        }

        public void CreateWeightMatrix(double[] newLetter)
        {
            for(var i = 0; i < MAXSIZE; i++)
            {
                for(var j = 0; j < MAXSIZE; j++)
                {
                    if(i== j)
                    {
                        WeightMatrix[i,j] = 0;
                    }
                    else
                    {
                        WeightMatrix[i, j] = WeightMatrix[i, j] + newLetter[i] * newLetter[j] / MAXSIZE;
                    }
                }
            }
        }

        public void CalculateResult()
        {
            for(var i = 0; i < MAXSIZE; i++)
            {
                for(var j = 0; j < MAXSIZE; j++)
                {
                    ResultLetter[i] = ResultLetter[i] + WeightMatrix[i,j] * TestedLetter[j];
                }
            }

            for(var i = 0; i < MAXSIZE; i++)
            {
                if (ResultLetter[i] >= 0)
                {
                    ResultLetter[i] = 1;
                }
                else
                {
                    ResultLetter[i] = -1;
                }
            }
        }

        public void AddLetterInDictionary(double[] newLetter, string letter)
        {
            Dictionary dictionary = new Dictionary();
            dictionary.Letter = newLetter;
            dictionary.Char = letter;
            dictionaries.Add(dictionary);
        }

        public string FoundResultLetter()
        {
            string answer = "Not found";
            
            foreach(var dictionary in dictionaries)
            {
                bool flag = true;
                var i = 0;
                while(flag && i < MAXSIZE)
                {
                    if(ResultLetter[i] != dictionary.Letter[i])
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                if(i == MAXSIZE)
                {
                    answer = dictionary.Char;
                }
            }
            return answer;
        }

        public void DelDictionary()
        {
            dictionaries.Clear();
            for(var i = 0; i < MAXSIZE; i++)
            {
                TestedLetter[i] = 0;
                ResultLetter[i] = 0;
                NewLetter[i] = 0;
                for(var j = 0; j < MAXSIZE; j++)
                {
                    WeightMatrix[i, j] = 0;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
