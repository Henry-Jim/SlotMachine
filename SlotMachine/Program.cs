namespace SlotMachine
{
    internal class Program
    {
        const int MONEY_INITIAL = 50;
        const int GRID_SIZE_DEFAULT = 3;
        const int RANDOM_LIMIT = 5;
        const int PAYOUT = 1;

        const int CHOICE_CENTER = 1;
        const int CHOICE_ALL_HORIZONTAL = 2;
        const int CHOICE_ALL_VERTICAL = 3;
        const int CHOICE_DIAGONAL = 4;
        const int CHOICE_ALL_LINES = 5;

        const string MESSAGE_WIN = "You win!";
        const string MESSAGE_LOSS = "You loss!";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Slot Machine Game!");

            int playMoney = MONEY_INITIAL;

            Random random = new Random();

            while (playMoney > 0)
            {
                Console.WriteLine($"You currently have ${playMoney}. How much would you like to wager?");
                int wager = 0;

                while (true)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out wager) && wager >0 && wager <= playMoney)
                    {
                        break; // Valid wager
                    }
                    Console.WriteLine("Invalid Wager. Please try again");

                }

                Console.WriteLine("Enter the grid size (e.g. 3 for 3x3)");
                int gridSize = GRID_SIZE_DEFAULT;

                while (true)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out gridSize) && gridSize > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid grid size. Please enter a number greater than 0");
                }

                int[,] grid = new int[gridSize, gridSize];

                Console.WriteLine("Choose line to play by enter the number:");
                Console.WriteLine($"{CHOICE_CENTER}: Center Line");
                Console.WriteLine($"{CHOICE_ALL_HORIZONTAL}: All Horizontal Lines");
                Console.WriteLine($"{CHOICE_ALL_VERTICAL}: All Vertical Lines");
                Console.WriteLine($"{CHOICE_DIAGONAL}: Diagonals");
                Console.WriteLine($"{CHOICE_ALL_LINES}: All Lines");
                int choice = 0;

                while (true)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out choice) && choice >= CHOICE_CENTER && choice <= CHOICE_ALL_LINES) // Make sure user input is between 1 - 5
                    {
                        break;
                    }
                    Console.WriteLine("Invalid choice. Please enter between 1 and 5");
                }

                // Generate the grid
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        grid[i, j] = random.Next(0, RANDOM_LIMIT);
                    }
                }

                // Print the grid
                Console.WriteLine("Slot Machine Result:");
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        Console.Write(grid[i, j] + " ");
                    }
                    Console.WriteLine();
                }

                int winnings = 0;

                // Horizontal
                if (choice == CHOICE_ALL_HORIZONTAL || choice == CHOICE_ALL_LINES)
                {
                    for (int i = 0; i < gridSize; i++)
                    {
                        bool win = true;
                        for (int j = 1;j < gridSize; j++)
                        {
                            if (grid[i, j] != grid[i, j - 1])
                            {
                                win = false;
                                break;
                            }
                        }
                        if (win)
                        {
                            winnings += PAYOUT;
                        }
                    }
                    
                }

                // Center
                if (choice == CHOICE_CENTER || choice == CHOICE_ALL_LINES)
                {
                    if (grid[1, 0] == grid[1, 1] && grid[1, 1] == grid[1, 2])
                    {
                        winnings += PAYOUT;
                    }
                }

                // Vertical
                if (choice == CHOICE_ALL_VERTICAL || choice == CHOICE_ALL_LINES)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        bool win = false;
                        for (int i = 1; j < gridSize; i++)
                        {
                            if (grid[i, j] != grid[i -1, j])
                            {
                                win = false;
                                break;
                            }
                        }
                        if (win)
                        {
                            winnings += PAYOUT;
                        }
                    }
                    
                }

                // Diagonal
                if (choice == CHOICE_DIAGONAL || choice == CHOICE_ALL_LINES)
                {
                    // Top-left to bottom-right diagonal
                    bool DiagonalWin = true;
                    for (int i = 1; i < gridSize; i++)
                    {
                        if (grid[i, i] != grid[i - 1, i - 1])
                        {
                            DiagonalWin = false;
                            break;
                        }
                    }
                    if (DiagonalWin)
                    {
                        winnings += PAYOUT;
                    }

                    // Top-right to bottom-left diagonal
                    bool antiDiagonalWin = true;
                    for (int i = 1; i < gridSize; i++)
                    {
                        if (grid[i, gridSize - i - 1] != grid[i - 1, gridSize - i])
                        {
                            antiDiagonalWin = false;
                            break;
                        }
                    }
                    if (antiDiagonalWin)
                    {
                        winnings += PAYOUT;
                    }

                    //if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
                    //{
                    //    winnings += PAYOUT;
                    //}
                    //if ((grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0]))
                    //{
                    //    winnings += PAYOUT;
                    //}
                }

                playMoney = playMoney - wager + winnings;
                Console.WriteLine("You have won $" + winnings + "! You currently have $" + playMoney + ".");

                if (playMoney <= 0)
                {
                    Console.WriteLine("Out of money. Game over!");
                    break;
                }

                Console.WriteLine("Do you want to play again? (y/n)");
                char playAgain = Console.ReadKey().KeyChar;
                if (playAgain == 'n')
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine();
                }

                Console.WriteLine("End of code");
            }

            //for (int i = 0; i < GRID_SIZE; i++) {
            //    for (int j = 0; j < GRID_SIZE; j++)
            //    {
            //        int randomNum = random.Next(0, RANDOM_LIMIT);
            //        grid[i,j] = randomNum;
            //    }
            //}
            //for (int i = 0; i < GRID_SIZE; i++)
            //{
            //    for (int j = 0; j < GRID_SIZE; j++)
            //    {
            //        Console.Write(grid[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            //if (grid[1, 0] == grid[1,1] && grid[1, 0] == grid[1, 2])
            //{
            //    Console.WriteLine(MESSAGE_WIN);
            //}
            //else
            //{
            //    Console.WriteLine(MESSAGE_LOSS);
            //}
        }
    }
}
