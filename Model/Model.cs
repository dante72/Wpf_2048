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

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrix[i, j] = 0;
            AddTwoOrFour();

            observableCollection = new ObservableCollection<string> { };

            UpdateObservableCollection();
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
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] == 0 || j != 0 && matrix[i, j] == matrix[i, j - 1])
                        for (int k = j; k > 0; k--)
                        {
                            int c = matrix[i, k];
                            matrix[i, k] = matrix[i, k - 1];
                            matrix[i, k - 1] = c;

                            if (matrix[i, j] == matrix[i, j - 1])
                            {
                                matrix[i, j] *= 2;
                                matrix[i, j - 1] = 0;
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

