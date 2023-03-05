using System.Windows;
using System.Windows.Media;

namespace Hopfield
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HopfieldAI myHopfieldAi = new HopfieldAI();
        CustomButton[] _buttons = new CustomButton[HopfieldAI.MAXSIZE];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            stack.Children.Clear();
            txtBlock.Text = "";
            txtBox.Text = "";

            for (var i = 0; i < _buttons.Length; i++)
            {
                var btn1 = new CustomButton
                {
                    Width = 50,
                    Height = 50,
                };
                _buttons[i] = btn1;
                stack.Children.Add(_buttons[i]);
            }
        }

        private void SetNewLetter_Click(object sender, RoutedEventArgs e)
        {
            myHopfieldAi.NewLetter = myHopfieldAi.CreateLetter(_buttons);
            if(txtBox.Text != "")
            {
                myHopfieldAi.AddLetterInDictionary(myHopfieldAi.NewLetter, txtBox.Text);
                myHopfieldAi.CreateWeightMatrix(myHopfieldAi.NewLetter);
            }
            else
            {
                MessageBox.Show("Вы забыли ввести значение.");
            }
        }

        private void DetermineLetter_Click (object sender, RoutedEventArgs e)
        {
            myHopfieldAi.TestedLetter = myHopfieldAi.CreateLetter(_buttons);
            myHopfieldAi.CalculateResult();
            txtBlock.Text = myHopfieldAi.FoundResultLetter();
            for (var i = 0; i < _buttons.Length; i++)
            {
                if (myHopfieldAi.TestedLetter[i] == myHopfieldAi.ResultLetter[i])
                {
                    if (_buttons[i].CustomBtn.Background != Brushes.White)
                    {
                        _buttons[i].CustomBtn.Background = Brushes.Green;
                    }
                }
                else if (myHopfieldAi.TestedLetter[i] < 0 && myHopfieldAi.ResultLetter[i] > 0)
                {
                    _buttons[i].CustomBtn.Background = Brushes.Orange;
                }
                else
                {
                    _buttons[i].CustomBtn.Background = Brushes.Red;
                }
            }
        }

        private void DeleteDictionary(object sender, RoutedEventArgs e)
        {
            myHopfieldAi.DelDictionary();
            MessageBox.Show("Словарь удален.");
        }
    }
}
