using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using SnakeandLadders.Models;
using SnakeandLadders.Data;

namespace SnakeandLadders.Controllers;

public class GamePlayController
{
    private readonly ApplicationDbContext _context;
    private readonly BoardController _boardController;
    private readonly PlayerController _playerController;
    private readonly DiceController _diceController;
    private GamePlay _gamePlay;
    public GamePlayController(ApplicationDbContext context, BoardController boardController, PlayerController playerController, DiceController diceController)
    {
        _context = context;
        _boardController = boardController;
        _playerController = playerController;
        _diceController = diceController;
    }

    public void StartNewGame(int boardId)
    {
        Board board = _boardController.GetBoardById(boardId);
        if (board != null)
        {
            GamePlay gamePlay = new GamePlay
            {
                BoardId = boardId,
                IsGameInProgress = true
            };

            _context.GamePlays.Add(gamePlay);
            _context.SaveChanges();
        }
    }
    public GamePlay GetGamePlayById(int gamePlayId)
    {
        return _context.GamePlays.FirstOrDefault(gamePlay => gamePlay.GamePlayId == gamePlayId);
    }
    public void UpdateGamePlay(GamePlay gamePlay)
    {
        _context.SaveChanges();
    }
    public void AddPlayer(int gamePlayId, string playerName)
    {
        GamePlay gamePlay = GetGamePlayById(gamePlayId);
        if (gamePlay != null)
        {
            Player player = new Player(playerName);
            gamePlay.Players.Add(player);
            _context.SaveChanges();
        }
    }
    public string MovePlayer(Player player, int rollValue)
    {
        int currentPosition = GetPlayerPosition(player.Id);
        int newPosition = currentPosition + rollValue;

        var board = _boardController.GetBoardById(_gamePlay.BoardId);
        if (newPosition > board.Size)
        {
            return $"Player {player.PlayerName} cannot move beyond the board size. Skipped turn.";
        }
        HandleSnakesAndLadders(ref newPosition, board.Snakes, board.Ladders, player.PlayerName);
        UpdatePlayerPosition(player.Id, newPosition);
        checkPlayerFinishPosition(player);
        return $"Player {player.PlayerName} get position {newPosition}";
    }
    private int GetPlayerPosition(int playerId)
    {
        if (_gamePlay.PlayerDataInGame.TryGetValue(playerId, out var playerDataInGame))
        {
            return playerDataInGame.Position;
        }
        return 0;
    }
    private void UpdatePlayerPosition(int playerId, int newPosition)
    {
        if (_gamePlay.PlayerDataInGame.TryGetValue(playerId, out var playerDataInGame));
        {
            _gamePlay.PlayerDataInGame[playerId] = new PlayerDataInGame();
        }
        _gamePlay.PlayerDataInGame[playerId].Position = newPosition;
    }
    private string HandleSnakesAndLadders(ref int position, ICollection<Snake> snakes, ICollection<Ladder> ladders, string playerName)
    {
        if (snakes != null)
        {
            foreach (var snake in snakes)
            {
                if (snake.HeadPosition == position)
                {
                    position = snake.TailPosition;
                    return $"Player {playerName} encountered a snake! Moved to position {position}";
                    break;
                }
            }
        }
        if (ladders != null)
        {
            foreach (var ladder in ladders)
            {
                if (ladder.BottomPosition == position)
                {
                    position = ladder.TopPosition;
                    return $"Player {playerName} climbed a ladder! Moved to position {position}";
                    break;
                }
            }
        }
        return null;
    }
    private void checkPlayerFinishPosition(Player player)
    {
        var playerData = GetPlayerDataInGame(player.Id);
        if (playerData.Position >= GetBoardSize())
        {
            playerData.PlayerInFinishPosition++;
            if (playerData.PlayerInFinishPosition >= 3 )
            {
                _gamePlay.IsGameInProgress = false;
            }
        }
    }
    private int GetBoardSize()
    {
        var board = _boardController.GetBoardById(_gamePlay.BoardId);
        return board.Size;
    }
    private PlayerDataInGame GetPlayerDataInGame(int playerId)
    {
        if (_gamePlay.PlayerDataInGame.TryGetValue(playerId, out var playerDataInGame))
        {
            return playerDataInGame;
        }
        var playerDataNew = new PlayerDataInGame();
        _gamePlay.PlayerDataInGame[playerId] = playerDataNew;
        return playerDataNew;
    }
    private int RollDice()
    {
        return _diceController.RollDice(_gamePlay.Dice);
    }
    public void UpdatePlayerDataInGame(int gamePlayId, int playerId, PlayerDataInGame playerDataInGame)
    {
        GamePlay gamePlay = GetGamePlayById(gamePlayId);
        if (gamePlay != null && gamePlay.PlayerDataInGame.ContainsKey(playerId))
        {
            gamePlay.PlayerDataInGame[playerId] = playerDataInGame;
            _context.SaveChanges();
        }
    }
    public void EndGame(int gamePlayId)
    {
        GamePlay gamePlay = GetGamePlayById(gamePlayId);
        if (gamePlay != null)
        {
            gamePlay.IsGameInProgress = false;
            _context.SaveChanges();
        }
    }
}