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
    public void AddSnake(int boardId, int headPosition, int tailPosition)
    {
        var board = GetBoardById(boardId);
        if (board != null && headPosition < board.Size && tailPosition > 0 && headPosition != tailPosition)
        {
            if (board.Snakes == null)
            {
                board.Snakes = new List<Snake>();
            }

            board.Snakes.Add(new Snake { HeadPosition = headPosition, TailPosition = tailPosition });
            _context.SaveChanges();
        }
    }

    public void AddLadder(int boardId, int bottomPosition, int topPosition)
    {
        var board = GetBoardById(boardId);
        if (board != null && bottomPosition < topPosition && bottomPosition > 0 && bottomPosition != topPosition)
        {
            if (board.Ladders == null)
            {
                board.Ladders = new List<Ladder>();
            }
            board.Ladders.Add(new Ladder{BottomPosition = bottomPosition, TopPosition = topPosition});
            _context.SaveChanges();
        }
    }
    public Board GetBoardById(int boardId)
    {
        return _context.Boards.FirstOrDefault(board => board.BoardId == boardId);
    }

    public Board GetBoardBySize(int boardSize)
    {
        return _context.Boards.FirstOrDefault(board => board.Size == boardSize);
    }

    public void UpdateBoard(int boardId, int size)
    {
        var board = GetBoardById(boardId);
        if (board != null)
        {
            board.Size = size;
        }
    }
}