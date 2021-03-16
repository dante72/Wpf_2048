using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_2048
{
    public class Model
    {
        public ObservableCollection<string> observableCollection { get; private set; }
        private Random rnd;
        private int[,] prev_matrix;
        private int[,] matrix;
        public Model(int columns, int rows)
        {
            matrix = new int[rows, columns];
            rnd = new Random();
            NewGame();

            observableCollection = new ObservableCollection<string> { };
            UpdateObservableCollection();
        }

        public void NewGame()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = 0;
            AddTwoOrFour();
        }
        private void UpdateObservableCollection()
        {
            observableCollection.Clear();
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] != 0)
                        observableCollection.Add(matrix[i, j].ToString());
                     else
                        observableCollection.Add("");
        }

        private void GameAction()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                        MoveItemInRowEnd(i, j);
                }
                SummSameItemsInRow(i);
            }
        }

        void MoveItemInRowEnd(int row, int index)
        {
            for (int k = index; k > 0; k--)
            {
                int c = matrix[row, k];
                matrix[row, k] = matrix[row, k - 1];
                matrix[row, k - 1] = c;
            }
        }

        void SummSameItemsInRow(int row)
        {
            for (int k = matrix.GetLength(1) - 2; k >= 0; k--)
            {
                if (matrix[row, k] == matrix[row, k + 1])
                {
                    matrix[row, k] *= 2;
                    matrix[row, k + 1] = 0;
                    MoveItemInRowEnd(row, k + 1);
                }
            }
        }

        void MatrixTransposition()
        {
            int[,] tmp = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    tmp[i, j] = matrix[j, i];

            matrix = tmp;
        }

        void MatrixFlip()
        {
            int[,] tmp = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    tmp[i, j] = matrix[i, matrix.GetLength(1) - j - 1];

            matrix = tmp;
        }
        int[,] Copy(int [,] matrix)
        {
            int[,] tmp_matrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    tmp_matrix[i, j] = matrix[i, j];

            return tmp_matrix;
        }

        bool Equals(int[,] matrix1, int[,] matrix2)
        {
            if (!(matrix1.GetLength(0) == matrix2.GetLength(0) && matrix1.GetLength(1) == matrix2.GetLength(1)))
                return false;

            for (int i = 0; i < matrix1.GetLength(0); i++)
                for (int j = 0; j < matrix2.GetLength(1); j++)
                    if (matrix1[i, j] != matrix2[i, j])
                        return false;
            return true;
        }

        public void GameRight()
        {
            prev_matrix = Copy(matrix);

            GameAction();

            if (!Equals(matrix, prev_matrix))
                AddTwoOrFour();

            UpdateObservableCollection();
        }

        public bool GameOver()
        {
            int[,] prev = Copy(matrix);
            GameUp();
            GameDown();
            GameLeft();
            GameRight();
            if (Equals(matrix, prev))
                return true;
            else
            {
                matrix = prev;
                return false;
            }

        }

        public void GameUp()
        {
            prev_matrix = Copy(matrix);

            MatrixTransposition();
            MatrixFlip();
            GameAction();
            MatrixFlip();
            MatrixTransposition();

            if (!Equals(matrix, prev_matrix))
                AddTwoOrFour();

            UpdateObservableCollection();
        }

        public void GameDown()
        {
            prev_matrix = Copy(matrix);
            MatrixTransposition();
            GameAction();
            MatrixTransposition();

            if (!Equals(matrix, prev_matrix))
                AddTwoOrFour();

            UpdateObservableCollection();
        }
        public void GameLeft()
        {
            prev_matrix = Copy(matrix);
            MatrixFlip();
            GameAction();
            MatrixFlip();

            if (!Equals(matrix, prev_matrix))
                AddTwoOrFour();

            UpdateObservableCollection();
        }
        public void AddTwoOrFour()
        {   
            int i, j, tenPercent;
            do
            {
                i = rnd.Next(0, matrix.GetLength(0));
                j = rnd.Next(0, matrix.GetLength(1));

            } while (matrix[i, j] != 0);

            tenPercent = rnd.Next(0, 10);

            if (tenPercent == 0)
                matrix[i, j] = 4;
            else
                matrix[i, j] = 2;

        }
    }
}

