using SnakeandLadders.Controllers;
using SnakeandLadders.Data;
using SnakeandLadders.Views;
using System;

namespace SnakeandLadders
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                PlayerController playerController = new PlayerController(context);
                PlayerView consoleView = new PlayerView(playerController);

                BoardController boardController = new BoardController(context);
                BoardView boardView = new BoardView(boardController);

                bool isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine("=== Main Menu ===");
                    Console.WriteLine("1. Manage Players");
                    Console.WriteLine("2. Manage Boards");
                    Console.WriteLine("3. Exit");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ShowPlayerMenu(consoleView);
                            break;
                        case "2":
                            ShowBoardMenu(boardView);
                            break;
                        case "3":
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine();
                }
            }
        }

        static void ShowPlayerMenu(PlayerView playerView)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("=== Player Menu ===");
                Console.WriteLine("1. Show Players");
                Console.WriteLine("2. Add Player");
                Console.WriteLine("3. Update Player");
                Console.WriteLine("4. Delete Player");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        playerView.ShowPlayers();
                        break;
                    case "2":
                        playerView.AddPlayer();
                        break;
                    case "3":
                        // playerView.UpdatePlayer();
                        Console.WriteLine("Not implemented yet.");
                        break;
                    case "4":
                        // playerView.DeletePlayer();
                        Console.WriteLine("Not implemented yet.");
                        break;
                    case "5":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ShowBoardMenu(BoardView boardView)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("=== Board Menu ===");
                Console.WriteLine("1. Show Board");
                Console.WriteLine("2. Add Board");
                Console.WriteLine("3. Add Snake");
                Console.WriteLine("4. Add Ladder");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter board ID: ");
                        int boardId = int.Parse(Console.ReadLine());
                        boardView.ShowBoard(boardId);
                        break;
                    case "2":
                        boardView.AddBoard();
                        break;
                    case "3":
                        boardView.AddSnake();
                        break;
                    case "4":
                        boardView.AddLadder();
                        break;
                    case "5":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
