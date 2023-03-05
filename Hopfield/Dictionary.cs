using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hopfield
{
    public class Dictionary 
    {
        private string? _char;
        private double[] _letter = new double[25];

        public string Char
        {
            get { return _char; }
            set
            {
                _char = value;
                OnPropertyChanged("Char");
            }
        }
        public double[] Letter
        {
            get { return _letter; }
            set
            {
                _letter = value;
                OnPropertyChanged("Letter");
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
