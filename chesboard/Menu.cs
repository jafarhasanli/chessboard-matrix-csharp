using System;

namespace chesboard
{
    public class Menu
    {
        public void Run()
        {
            Chessboard chesbord = null;

            while (true)
            {
                Console.WriteLine("*****************************************");
                Console.WriteLine("\nMenu");
                Console.WriteLine("1) Create Chessboard");
                Console.WriteLine("2) Get value at position");
                Console.WriteLine("3) Set value at position");
                Console.WriteLine("4) Add another matrix");
                Console.WriteLine("5) Multiply by another matrix");
                Console.WriteLine("6) Print matrix");
                Console.WriteLine("7) Exit");
                Console.Write("Enter your option: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("ERROR - Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        CreateMatrix(ref chesbord);
                        break;

                    case 2:
                        if (CheckMatrixExists(chesbord))
                        {
                            GetEntryUI(chesbord);
                        }
                        break;

                    case 3:
                        if (CheckMatrixExists(chesbord))
                        {
                            SetEntryUI(chesbord);
                        }
                        break;

                    case 4:
                        if (CheckMatrixExists(chesbord))
                        {
                            Chessboard other = CreateChessboardFromInput();
                            try
                            {
                                chesbord.Add(other);
                                Console.WriteLine("Matrices added successfully.");
                            }
                            catch (Chessboard.InvalidOperationException ex)
                            {
                                Console.WriteLine("ERROR - " + ex.Message);
                            }
                        }
                        break;

                    case 5:
                        if (CheckMatrixExists(chesbord))
                        {
                            Chessboard other = CreateChessboardFromInput();
                            try
                            {
                                chesbord.multiply(other);
                                Console.WriteLine("Matrices multiplied successfully.");
                            }
                            catch (Chessboard.InvalidOperationException ex)
                            {
                                Console.WriteLine("ERROR - " + ex.Message);
                            }
                        }
                        break;

                    case 6:
                        if (CheckMatrixExists(chesbord))
                        {
                            chesbord.printMatrix();
                        }
                        break;

                    case 7:
                        Console.WriteLine("You exited the program.");
                        return;

                    default:
                        Console.WriteLine("Please choose a valid option.");
                        break;
                }
            }
        }

        private bool CheckMatrixExists(Chessboard chesbord)
        {
            if (chesbord == null)
            {
                Console.WriteLine("Please create a chessboard matrix first.");
                return false;
            }
            return true;
        }

        private void CreateMatrix(ref Chessboard chesbord)
        {
            Console.Write("Enter number of rows: ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
            {
                Console.WriteLine("Invalid row count.");
                return;
            }

            Console.Write("Enter number of columns: ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
            {
                Console.WriteLine("Invalid column count.");
                return;
            }

            chesbord = CreateChessboard(rows, cols);
            Console.WriteLine("Chessboard matrix created successfully.");
        }

        private void GetEntryUI(Chessboard chesbord)
        {
            Console.Write("Enter row index (1-based): ");
            int rowind = int.Parse(Console.ReadLine());

            Console.Write("Enter column index (1-based): ");
            int colind = int.Parse(Console.ReadLine());

            try
            {
                int entry = chesbord.getEnteries(rowind, colind);
                Console.WriteLine($"Entry at ({rowind}, {colind}) = {entry}");
            }
            catch (Chessboard.IndexOutOfRangeException)
            {
                Console.WriteLine("ERROR - Index is out of range.");
            }
        }

        private void SetEntryUI(Chessboard chesbord)
        {
            Console.Write("Enter row index (1-based): ");
            int rowind = int.Parse(Console.ReadLine());

            Console.Write("Enter column index (1-based): ");
            int colind = int.Parse(Console.ReadLine());

            Console.Write("Enter new value: ");
            int value = int.Parse(Console.ReadLine());

            try
            {
                chesbord.SetEntry(rowind, colind, value);
                Console.WriteLine("Value set successfully.");
            }
            catch (Chessboard.IndexOutOfRangeException)
            {
                Console.WriteLine("ERROR - Index is out of range.");
            }
            catch (Chessboard.InvalidOperationException ex)
            {
                Console.WriteLine("ERROR - " + ex.Message);
            }
        }

        private Chessboard CreateChessboardFromInput()
        {
            Console.Write("Enter number of rows: ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Enter number of columns: ");
            int cols = int.Parse(Console.ReadLine());

            return CreateChessboard(rows, cols);
        }

        private Chessboard CreateChessboard(int rows, int cols)
        {
            int[,] numbers = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Enter number for position ({i + 1}, {j + 1}): ");
                    if (!int.TryParse(Console.ReadLine(), out int number))
                    {
                        Console.WriteLine("Invalid input. Defaulting to zero.");
                        number = 0;
                    }
                    numbers[i, j] = number;
                }
            }

            return new Chessboard(rows, cols, (i, j) => numbers[i, j]);
        }
    }
}