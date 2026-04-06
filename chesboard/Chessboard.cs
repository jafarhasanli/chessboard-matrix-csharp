using System;
using System.Collections.Generic;
using System.Text;

namespace chesboard
{
    public class Chessboard
    {
        public class IndexOutOfRangeException : Exception
        {
            public IndexOutOfRangeException() : base("Index is out of range.") { }
        }

        public class InvalidOperationException : Exception
        {
            public InvalidOperationException() : base("Invalid matrix operation.") { }
            public InvalidOperationException(string message) : base(message) { }
        }

        private int rows;
        private int cols;
        private List<int> data;

        public int Rows => rows;
        public int Cols => cols;

        public Chessboard(int rows, int cols, Func<int, int, int> initializeFunc = null)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Rows and columns must be positive.");

            this.rows = rows;
            this.cols = cols;
            data = new List<int>();

            InitializeData(initializeFunc);
        }

        private void InitializeData(Func<int, int, int> initializeFunc)
        {
            data.Clear();

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    if (IsStoredCell(i, j))
                    {
                        int value = initializeFunc != null ? initializeFunc(i - 1, j - 1) : 0;
                        data.Add(value);
                    }
                }
            }
        }

        private bool IsStoredCell(int i, int j)
        {
            return (i + j) % 2 == 0;
        }

        private int GetInternalIndex(int i, int j)
        {
            int index = 0;

            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= cols; c++)
                {
                    if (IsStoredCell(r, c))
                    {
                        if (r == i && c == j)
                            return index;

                        index++;
                    }
                }
            }

            throw new Exception("Internal indexing error.");
        }

        public int getEnteries(int i, int j)
        {
            if (i < 1 || i > Rows || j < 1 || j > Cols)
                throw new IndexOutOfRangeException();

            if (!IsStoredCell(i, j))
                return 0;

            int idx = GetInternalIndex(i, j);
            return data[idx];
        }

        public void SetEntry(int i, int j, int value)
        {
            if (i < 1 || i > Rows || j < 1 || j > Cols)
                throw new IndexOutOfRangeException();

            if (!IsStoredCell(i, j))
            {
                if (value != 0)
                    throw new InvalidOperationException("This position must always be zero.");
                return;
            }

            int idx = GetInternalIndex(i, j);
            data[idx] = value;
        }

        public void Add(Chessboard other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Rows != other.Rows || Cols != other.Cols)
                throw new InvalidOperationException("Matrices must have the same dimensions for addition.");

            for (int i = 0; i < data.Count; i++)
            {
                data[i] += other.data[i];
            }
        }

        public void multiply(Chessboard other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Cols != other.Rows)
                throw new InvalidOperationException("Number of columns of first matrix must equal number of rows of second matrix.");

            Chessboard result = new Chessboard(Rows, other.Cols);

            for (int i = 1; i <= Rows; i++)
            {
                for (int j = 1; j <= other.Cols; j++)
                {
                    int sum = 0;

                    for (int k = 1; k <= Cols; k++)
                    {
                        sum += this.getEnteries(i, k) * other.getEnteries(k, j);
                    }

                    if ((i + j) % 2 == 0)
                    {
                        result.SetEntry(i, j, sum);
                    }
                }
            }

            this.rows = result.rows;
            this.cols = result.cols;
            this.data = new List<int>(result.data);
        }

        public void printMatrix()
        {
            for (int i = 1; i <= Rows; i++)
            {
                for (int j = 1; j <= Cols; j++)
                {
                    Console.Write(getEnteries(i, j) + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}