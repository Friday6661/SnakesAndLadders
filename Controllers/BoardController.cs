using System.Collections.Generic;
using SnakeandLadders.Models;
using SnakeandLadders.Data;

namespace SnakeandLadders.Controllers;

public class BoardController
{
    private readonly ApplicationDbContext _context;
    public BoardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddBoard(int size)
    {
        var board = new Board
        {
            Size = size,
            Snakes = new List<Snake>(),
            Ladders = new List<Ladder>()
        };

        _context.Boards.Add(board);
        _context.SaveChanges();
    }
    public string AddSnake(int boardId, int headPosition, int tailPosition)
    {
        var board = GetBoardById(boardId);
        if (board != null && headPosition < board.Size && tailPosition > 0 && headPosition != tailPosition)
        {
            if (board.Snakes == null)
            {
                board.Snakes = new List<Snake>();
            }

            bool isDuplicate = board.Snakes.Any(s => s.HeadPosition == headPosition || s.TailPosition == tailPosition);
            if (!isDuplicate)
            {
                board.Snakes.Add(new Snake { HeadPosition = headPosition, TailPosition = tailPosition });
                _context.SaveChanges();
            }
            return "Snake with the same position already exists";
        }
        return "Failed to add snake to the list";
    }

    public string AddLadder(int boardId, int bottomPosition, int topPosition)
    {
        var board = GetBoardById(boardId);
        if (board != null && bottomPosition < topPosition && bottomPosition > 0 && bottomPosition != topPosition)
        {
            if (board.Ladders == null)
            {
                board.Ladders = new List<Ladder>();
            }

            bool isDuplicate = board.Ladders.Any(l => l.BottomPosition == bottomPosition || l.TopPosition == topPosition);
            if (isDuplicate)
            {
                board.Ladders.Add(new Ladder{BottomPosition = bottomPosition, TopPosition = topPosition});
                _context.SaveChanges();
            }
            return "Ladder with the smae position already exists";
        }
        return "Failed to add ladder to the list";
    }
    public Board GetBoardById(int boardId)
    {
        return _context.Boards.FirstOrDefault(board => board.BoardId == boardId);
    }

    public Board GetBoardBySize(int boardSize)
    {
        return _context.Boards.FirstOrDefault(board => board.Size == boardSize);
    }
    public List<Board> GetAllBoards()
    {
        return _context.Boards.ToList();
    }

    public void UpdateBoard(int boardId, int size)
    {
        var board = GetBoardById(boardId);
        if (board != null)
        {
            board.Size = size;
        }
    }
    public List<Snake> GetSnakesByBoardId(int boardId)
    {
        return _context.Snakes.Where(s => s.BoardId == boardId).ToList();
    }
}