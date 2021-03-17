using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_2048
{
    public class Model: ViewModel.BaseViewModel
    {
        public ObservableCollection<string> observableCollection { get; private set; }
        public int Score
        {
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
            get { return _score; }
        }

        public int Record
        {
            set
            {
                if (value > GetRecord())
                {
                    SetRecord(value);
                    OnPropertyChanged(nameof(Record));
                }
            }
            get { return GetRecord(); }
        }

        private Random rnd;
        private int[,] prev_matrix;
        private int[,] matrix;
        int _score;

        private int GetRecord()
        {
            int record = 0;
            try
            {
                StreamReader sr = new StreamReader(nameof(Record));
                int.TryParse(sr.ReadLine(), out record);
                sr.Close();
            }
            catch
            {
                SetRecord(record);
            }

            return record;
        }

        private void SetRecord(int record)
        {
            StreamWriter sw = new StreamWriter(nameof(Record));
            sw.WriteLine(record);
            sw.Close();
        }
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
            Score = 0;

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

        private int MoveItemsRightAndSumm()
        {
            int score = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                        MoveItemInRowEnd(i, j);
                }
                score += SummSameItemsInRow(i);
            }

            return score;
        }

        private void MoveItemInRowEnd(int row, int index)
        {
            for (int k = index; k > 0; k--)
            {
                int c = matrix[row, k];
                matrix[row, k] = matrix[row, k - 1];
                matrix[row, k - 1] = c;
            }
        }
        private int SummSameItemsInRow(int row)
        {
            int score = 0;
            for (int k = matrix.GetLength(1) - 2; k >= 0; k--)
            {
                if (matrix[row, k] == matrix[row, k + 1])
                {
                    
                    matrix[row, k] *= 2;
                    matrix[row, k + 1] = 0;
                    score += matrix[row, k];
                    MoveItemInRowEnd(row, k + 1);  
                }
            }
            return score;
        }
        private void MatrixTransposition()
        {
            int[,] tmp = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    tmp[i, j] = matrix[j, i];

            matrix = tmp;
        }

        private void MatrixFlip()
        {
            int[,] tmp = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    tmp[i, j] = matrix[i, matrix.GetLength(1) - j - 1];

            matrix = tmp;
        }
        private int[,] Copy(int [,] matrix)
        {
            int[,] tmp_matrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    tmp_matrix[i, j] = matrix[i, j];

            return tmp_matrix;
        }

        private bool Equals(int[,] matrix1, int[,] matrix2)
        {
            if (!(matrix1.GetLength(0) == matrix2.GetLength(0) && matrix1.GetLength(1) == matrix2.GetLength(1)))
                return false;

            for (int i = 0; i < matrix1.GetLength(0); i++)
                for (int j = 0; j < matrix2.GetLength(1); j++)
                    if (matrix1[i, j] != matrix2[i, j])
                        return false;
            return true;
        }
        public void GameMove(Move move) => GameMove(move, true);
        private void GameMove(Move move, bool observe)
        {
            prev_matrix = Copy(matrix);
            int score = 0;

            switch (move)
            {
                case Move.Right:
                    score = MoveItemsRightAndSumm();
                    break;

                case Move.Left:
                    MatrixFlip();
                    score = MoveItemsRightAndSumm();
                    MatrixFlip();
                    break;

                case Move.Up:
                    MatrixTransposition();
                    MatrixFlip();
                    score = MoveItemsRightAndSumm();
                    MatrixFlip();
                    MatrixTransposition();
                    break;

                case Move.Down:
                    MatrixTransposition();
                    score = MoveItemsRightAndSumm();
                    MatrixTransposition();
                    break;
            }

            if (!Equals(matrix, prev_matrix))
            {
                AddTwoOrFour();
                if (observe)
                {
                    Score += score;
                    UpdateObservableCollection();
                }
            }        
        }
        public bool GameOver()
        {
            int[,] prev = Copy(matrix);

            GameMove(Move.Up, false);
            GameMove(Move.Down, false);
            GameMove(Move.Left, false);
            GameMove(Move.Right, false);

            if (Equals(matrix, prev))
                return true;
            else
            {
                matrix = prev;
                return false;
            }
        }
        private void AddTwoOrFour()
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

