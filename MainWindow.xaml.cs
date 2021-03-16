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
            myModel = new Model(GameField.Columns, GameField.Rows);
            DataContext = new ViewModel.ViewModel(myModel);
            GameView.AddElements(GameField);
        }

        private void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (myModel.GameOver())
            {
                MessageBox.Show("GameOver");
                myModel.NewGame();
            }
            switch (e.Key)
            {
                case Key.Right:
                    myModel.GameRight();
                    break;
                case Key.Up:
                    myModel.GameUp();
                    break;
                case Key.Down:
                    myModel.GameDown();
                    break;
                case Key.Left:
                    myModel.GameLeft();
                    break;
            }
        }
    }
}
