using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace cli_life
{
    public class Cell
    {
        public bool IsAlive;
        public readonly List<Cell> neighbors = new List<Cell>();
        private bool _isAliveNext;

        public void DetermineNextLiveState()
        {
            int liveNeighbors = neighbors.Where(x => x.IsAlive).Count();
            if (IsAlive)
                _isAliveNext = liveNeighbors == 2 || liveNeighbors == 3;
            else
                _isAliveNext = liveNeighbors == 3;
        }

        public void Advance()
        {
            IsAlive = _isAliveNext;
        }

        public int NeighborCount()
        {
            return neighbors.Where(x => x.IsAlive).Count();
        }
    }

    public class Board
    {
        public readonly Cell[,] Cells;
        public readonly int CellSize;

        public int Columns { get { return Cells.GetLength(0); } }
        public int Rows { get { return Cells.GetLength(1); } }
        public int Width { get { return Columns * CellSize; } }
        public int Height { get { return Rows * CellSize; } }

        public Board(int width, int height, int cellSize, double liveDensity = .1)
        {
            CellSize = cellSize;

            Cells = new Cell[width / cellSize, height / cellSize];
            for (int x = 0; x < Columns; x++)
                for (int y = 0; y < Rows; y++)
                    Cells[x, y] = new Cell();

            ConnectNeighbors();
            Randomize(liveDensity);
        }

        readonly Random rand = new Random();
        public void Randomize(double liveDensity)
        {
            foreach (var cell in Cells)
                cell.IsAlive = rand.NextDouble() < liveDensity;
        }

        public void Advance()
        {
            foreach (var cell in Cells)
                cell.DetermineNextLiveState();
            foreach (var cell in Cells)
                cell.Advance();
        }
        private void ConnectNeighbors()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    int xL = (x > 0) ? x - 1 : Columns - 1;
                    int xR = (x < Columns - 1) ? x + 1 : 0;

                    int yT = (y > 0) ? y - 1 : Rows - 1;
                    int yB = (y < Rows - 1) ? y + 1 : 0;

                    Cells[x, y].neighbors.Add(Cells[xL, yT]);
                    Cells[x, y].neighbors.Add(Cells[x, yT]);
                    Cells[x, y].neighbors.Add(Cells[xR, yT]);
                    Cells[x, y].neighbors.Add(Cells[xL, y]);
                    Cells[x, y].neighbors.Add(Cells[xR, y]);
                    Cells[x, y].neighbors.Add(Cells[xL, yB]);
                    Cells[x, y].neighbors.Add(Cells[x, yB]);
                    Cells[x, y].neighbors.Add(Cells[xR, yB]);
                }
            }
        }
        public int CountLiveCells()
        {
            return Cells.Cast<Cell>().Count(cell => cell.IsAlive);
        }

        public int CountSymmetricElements()
        {
            int symmetricCount = 0; 
                for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (Cells[x, y].IsAlive == Cells[Columns - x - 1, y].IsAlive)
                    {
                        symmetricCount++;
                    }
                }
            }

            return symmetricCount;
        }

        public static void SaveState(Board board, String path)
        {
            String buf = "";
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    var cell = board.Cells[col, row];
                    if (cell.IsAlive)
                    {
                        buf += "*";
                    }
                    else
                    {
                        buf += " ";
                    }
                }
                buf += "\n";
            }
            File.WriteAllText(path, buf);
        }

        public static void LoadState(Board board, string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File {path} not found. Press any key to continue.");
                Console.ReadKey();
                return;
            }

            string[] lines = File.ReadAllLines(path);
            for (int y = 0; y < board.Rows && y < lines.Length; y++)
            {
                for (int x = 0; x < board.Columns && x < lines[y].Length; x++)
                {
                    board.Cells[x, y].IsAlive = lines[y][x] == '*';
                }
            }
        }

        public void Print()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Console.Write(Cells[x, y].IsAlive ? "#" : ".");
                }
                Console.WriteLine();
            }
        }
        public int CountElements()
        {
            return Columns * Rows;
        }

        // Классификация элементов, сопоставление с образцами
        public void ClassifyElements()
        {
            // Вам необходимо определить классификацию и образцы для сопоставления.
            // Здесь простой пример с использованием предопределенных образцов.
            var pattern1 = new bool[,] { { true, true }, { true, true } };
            var pattern2 = new bool[,] { { true, false }, { false, true } };

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    // Здесь вы можете проверить, соответствует ли текущий элемент какому-либо образцу
                    // и затем обработать результаты сопоставления.
                }
            }
        }

        // Исследование среднего времени (число поколений) перехода в стабильную фазу
        public double AverageTransitionToStablePhase(int iterations)
        {
            int totalGenerations = 0;

            for (int i = 0; i < iterations; i++)
            {
                int generations = 0;
                var tempBoard = new Board(Columns * CellSize, Rows * CellSize, CellSize);
                bool isStable = false;

                while (!isStable)
                {
                    tempBoard.Advance();
                    generations++;

                    // Здесь вам нужно определить условия стабильности и проверить,
                    // соответствует ли текущая доска этим условиям
                    // Если да, установите isStable = true
                }

                totalGenerations += generations;
            }

            return (double)totalGenerations / iterations;
        }

        // Исследование симметричности всей системы от числа поколений
        public double SymmetryInvestigation(int generations)
        {
            int totalSymmetricElements = 0;

            for (int i = 0; i < generations; i++)
            {
                Advance();
                totalSymmetricElements += CountSymmetricElements();
            }

            return (double)totalSymmetricElements / generations;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(60, 20, 1);
            bool exit = false;
            string savePath = @"C:\Users\Михаил\source\repos\mod-lab04-life\Life\saved_state.txt";
            string[] patterns = {
            @"C:\Users\Михаил\source\repos\mod-lab04-life\Life\blinker_state.txt",
            @"C:\Users\Михаил\source\repos\mod-lab04-life\Life\block_state.txt",
           @"C:\Users\Михаил\source\repos\mod-lab04-life\Life\glider_state.txt",
        };
           
            int currentPattern = 0;
            Board.LoadState(board, patterns[currentPattern]);


            while (!exit)
            {
                Console.Clear();
                board.Print();
                Console.WriteLine("Live cells: {0}", board.CountLiveCells());
                Console.WriteLine("Symmetric elements: {0}", board.CountSymmetricElements());

                Console.WriteLine("Press 'N' to load next pattern, 'S' to save, 'L' to load, 'Q' to quit or any other key to advance");

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.N:
                            currentPattern = (currentPattern + 1) % patterns.Length;
                            Board.LoadState(board, patterns[currentPattern]);
                            break;
                        case ConsoleKey.A:
                            int iterations = 100; // Задайте количество итераций для AverageTransitionToStablePhase
                            Console.WriteLine("Average transition to stable phase: {0}", board.AverageTransitionToStablePhase(iterations));
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);
                            break;
                        case ConsoleKey.Y:
                            int generations = 50; // Задайте количество поколений для SymmetryInvestigation
                            Console.WriteLine("Symmetry investigation: {0}", board.SymmetryInvestigation(generations));
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);
                            break;
                        case ConsoleKey.S:
                            Board.SaveState(board, savePath);
                            break;
                        case ConsoleKey.L:
                            Board.LoadState(board, savePath);
                            break;
                        case ConsoleKey.Q:
                            exit = true;
                            break;
                        default:
                            board.Advance();
                            break;
                    }
                }
                else
                {
                    board.Advance();
                }
                Thread.Sleep(100);
            }
        }
    }
}