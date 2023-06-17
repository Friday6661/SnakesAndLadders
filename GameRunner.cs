using GameControlLib;
using PlayerLib;
using DisplayLib;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace GameRunnerLib;

public class GameRunner
{
    private GameControl _gameControl;
    private Display _display;
    public event Action<string> NotifMessage;
    public GameRunner()
    {
        _gameControl = new GameControl(new DiceLib.Dice(6), new BoardLib.Board(100));
        _display = new Display();
        _gameControl.NotifMessage += _display.DisplayNotifMessage;
    }
    

    public void SetupGame()
    {
        _display.RefreshDisplay();
        _display.DisplayMessage("Enter the Number of Players: ");
        _gameControl.SetupPlayer();
        Console.ReadLine();
    }
    public async Task StartGame()
    {
        _display.DisplayMessage("Start Game");
        Task loadingTask = _gameControl.Loading();
        _display.DisplayMessage("Game Starting.....");
        await loadingTask;
        while (!_gameControl.IsgameFinished())
        {
            foreach (Player player in _gameControl.GetPlayers())
            {
                _gameControl.DrawBoard();
                _display.DisplayPlayerInfo(_gameControl.GetPlayerName(player), _gameControl.GetPlayerPosition(player));
                Console.ReadLine();
                bool rollAgain = true;
                int totalRolls = 0;

                while (rollAgain && totalRolls < 2)
                {
                    _gameControl.RollDice(player);
                    _gameControl.MovePlayer(player);
                    totalRolls++;
                    if (_gameControl.shouldRollAgain(player))
                    {
                        _display.DisplayMessage($"Player {player.GetName()} rolled a dice and got a{_gameControl.GetNumberOfSides()}! Roll the dice again.");
                    }
                    else
                    {
                        rollAgain = false;
                    }
                    AllPlayerPositions();
                }
            }
        }
    }
    private void AllPlayerPositions()
    {
        Dictionary<Player, int> playerPositions = _gameControl.GetAllPlayerPositions();
        foreach (var entry in playerPositions)
        {
            _display.DisplayMessage($"Player {entry.Key.GetName()} Position: {entry.Value}");
        }
    }
}