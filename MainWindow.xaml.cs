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
        Model gameModel;
        GameView gameView;
        public MainWindow()
        {
            InitializeComponent();

            gameView = new GameView();
            gameView.AddElements(GameField);

            gameModel = new Model(GameField.Columns, GameField.Rows);
            DataContext = gameModel;
        }

        private void MyKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Right:
                    gameModel.GameMove(Move.Right);
                    break;
                case Key.Up:
                    gameModel.GameMove(Move.Up);
                    break;
                case Key.Down:
                    gameModel.GameMove(Move.Down);
                    break;
                case Key.Left:
                    gameModel.GameMove(Move.Left);
                    break;
            }
            if (gameModel.GameOver())
            {
                gameModel.Record = gameModel.Score;
                MessageBox.Show("GameOver");
                gameModel.NewGame();

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