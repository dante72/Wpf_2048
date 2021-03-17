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
using System.Windows.Shapes;

namespace Wpf_2048
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model myModel;
        public MainWindow()
        {
            InitializeComponent(); 
            GameView.AddElements(GameField);
            myModel = new Model(GameField.Columns, GameField.Rows);
            DataContext = myModel;
        }

        private void MyKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Right:
                    myModel.GameMove(Move.Right);
                    break;
                case Key.Up:
                    myModel.GameMove(Move.Up);
                    break;
                case Key.Down:
                    myModel.GameMove(Move.Down);
                    break;
                case Key.Left:
                    myModel.GameMove(Move.Left);
                    break;
            }
            if (myModel.GameOver())
            {
                myModel.Record = myModel.Score;
                MessageBox.Show("GameOver");
                myModel.NewGame();

            }
        }
    }

    public enum Move
    {
        Up = 0,
        Down,
        Left,
        Right
    }
}
