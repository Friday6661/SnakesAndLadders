using System;
using System.Collections.Generic;
using SnakeandLadders.Controllers;
using SnakeandLadders.Models;
using SnakeandLadders.Data;

namespace SnakeandLadders.Views;
public class BoardView
{
    private readonly BoardController _boardController;

    public BoardView(BoardController boardController)
    {
        _boardController = boardController;
    }

    public void ShowBoard(int boardId)
    {
        Board board = _boardController.GetBoardById(boardId);
        if (board != null)
        {
            Console.WriteLine($"Board ID: {board.BoardId}");
            Console.WriteLine($"Board Size: {board.Size}");
            Console.WriteLine("Snakes:");
            ShowSnakes(board.Snakes);
            Console.WriteLine("Ladders:");
            ShowLadders(board.Ladders);
        }
        else
        {
            Console.WriteLine("Board not found.");
        }
    }

    public void AddBoard()
    {
        Console.Write("Enter board size: ");
        int size = int.Parse(Console.ReadLine());

        _boardController.AddBoard(size);
        Console.WriteLine("Board added successfully.");
    }

    public void AddSnake()
    {
        Console.Write("Enter board ID to add snake: ");
        int boardId = int.Parse(Console.ReadLine());

        Console.Write("Enter snake head position: ");
        int headPosition = int.Parse(Console.ReadLine());

        Console.Write("Enter snake tail position: ");
        int tailPosition = int.Parse(Console.ReadLine());

        _boardController.AddSnake(boardId, headPosition, tailPosition);
        Console.WriteLine("Snake added successfully.");
    }

    public void AddLadder()
    {
        Console.Write("Enter board ID to add ladder: ");
        int boardId = int.Parse(Console.ReadLine());

        Console.Write("Enter ladder bottom position: ");
        int bottomPosition = int.Parse(Console.ReadLine());

        Console.Write("Enter ladder top position: ");
        int topPosition = int.Parse(Console.ReadLine());

        _boardController.AddLadder(boardId, bottomPosition, topPosition);
        Console.WriteLine("Ladder added successfully.");
    }

    private void ShowSnakes(ICollection<Snake> snakes)
    {
        if (snakes != null && snakes.Count > 0)
        {
            foreach (Snake snake in snakes)
            {
                Console.WriteLine($"Head Position: {snake.HeadPosition}, Tail Position: {snake.TailPosition}");
            }
        }
        else
        {
            Console.WriteLine("No snakes found.");
        }
    }

    private void ShowLadders(ICollection<Ladder> ladders)
    {
        if (ladders != null && ladders.Count > 0)
        {
            foreach (Ladder ladder in ladders)
            {
                Console.WriteLine($"Bottom Position: {ladder.BottomPosition}, Top Position: {ladder.TopPosition}");
            }
        }
        else
        {
            Console.WriteLine("No ladders found.");
        }
    }
}
