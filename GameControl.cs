using IBoardLib;
using BoardLib;
using IPlayerLib;
using PlayerLib;
using IDiceLib;
using DiceLib;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameControlLib;
class GameControl
{
    private List<Player> _players; // Better dihapus
    private Dice _dice;
    private Board _board;
    private Dictionary<Player, int> _playerPosition;
    private Dictionary<Player, int> _lastRollValue;
    public event Action<string> NotifMessage;

    public GameControl(Dice dice, Board board)
    {
        _players = new List<Player>();
        _dice = dice;
        _board = board;
        _playerPosition = new Dictionary<Player, int>();
        _lastRollValue = new Dictionary<Player, int>();
    }
    public void SetPlayers(List<Player> players)
    {
        _players = players;
    }
    public List<Player> GetPlayers()
    {
        return _players;
    }
    public void SetDice(int numberOfSides)
    {
        _dice = new Dice(numberOfSides);
    }
    public Dice GetDice()
    {
        return _dice;
    }
    public int GetNumberOfSides()
    {
        return _dice.GetNumberOfSides();
    }
    public void SetBoard(int boardSize)
    {
        _board = new Board(boardSize);
    }
    public Board GetBoard()
    {
        return _board;
    }
    public void IntitalizeSnakesAndLadders(Dictionary<int, int> snakes, Dictionary<int, int> ladders)
    {
        _board.SetSnake(snakes);
        _board.SetLadder(ladders);
    }
    public void AddPlayer(string name)
    {
        _players.Add(new Player(name));
    }
    public void RemovePlayer(string name)
    {
        _players.RemoveAll(player => player.GetName() == name);
    }
    public string GetPlayerName(Player player)
    {
        return player.GetName();
    }
    public void SetupPlayer()
    {
        int playerCount = GetPlayerCountFromInput();
        GetPlayerNames(playerCount);
    }
    private int GetPlayerCountFromInput()
    {
        int playerCount;
        while(true)
        {
            string input = Console.ReadLine(); //pharsing dari addplayer
            if (int.TryParse(input, out playerCount))
            {
                if (playerCount >= 2 && playerCount <= 4)
                {
                    break;
                }
                else
                {
                    NotifMessage?.Invoke("Input Player Out of Range. Please Input Again: ");
                }
            }
            else
            {
                NotifMessage?.Invoke("Invalid Input. Please Enter A Valid Number");
            }
        }
        return playerCount;
    }
    private void GetPlayerNames(int playerCount)
    {
        for (int i = 1; i <= playerCount; i++)
        {
            while (true)
            {
                NotifMessage?.Invoke($"Enter the Name of Players {i}: (at least 2 character)");
                string name = Console.ReadLine();
                if (name.Length >= 2)
                {
                    AddPlayer(name.ToUpper());
                    break;
                }
                else
                {
                    NotifMessage?.Invoke("Invalid Input! Try Again!");
                }
            }
        }
    }
    public Player GetPlayerAtPosition(int position)
    {
        foreach (KeyValuePair<Player, int> entry in _playerPosition)
        {
            if (entry.Value == position)
            {
                return entry.Key;
            }
        }
        return null;
    }
    public List<Player> GetPlayersInPosition(int position)
    {
        List<Player> playersInPosition = new List<Player>();
        foreach (KeyValuePair<Player, int> entry in _playerPosition)
        {
            if (entry.Value == position)
            {
                playersInPosition.Add(entry.Key);
            }
        }
        return playersInPosition;
    }
    public int GetPlayerPosition(Player player)
    {
        if (_playerPosition.ContainsKey(player))
        {
            return _playerPosition[player];
        }
        return 0;
    }
    public void SetPlayerPosition(Player player, int position)
    {
        if (_playerPosition.ContainsKey(player))
        {
            _playerPosition[player] = position;
        }
        else
        {
            _playerPosition.Add(player, position);
        }
    }
    public Dictionary<Player, int> GetAllPlayerPositions()
    {
        Dictionary<Player, int> playerPositions = new Dictionary<Player, int>();
        foreach (var player in _players)
        {
            int playerPosition = GetPlayerPosition(player);
            playerPositions.Add(player, playerPosition);
        }
        return playerPositions;
    }
    public void DrawBoard()
    {
        int numRows = (int)Math.Ceiling((double)_board.GetSize()/10);
        for (int i = numRows; i >= 1; i--)
        {
            for (int j = 1; j <= 10; j++)
            {
                Console.Write(" ______");
            }
            Console.WriteLine();

            for (int k = 0; k <= 2; k++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    int index;
                    if (i % 2 == 1)
                    {
                        index = ((i-1) * 10) +j;
                    }
                    else
                    {
                        index = (i * 10) - j + 1;
                    }

                    if (index <= _board.GetSize() && k == 0)
                    {
                        Console.Write($"|{index.ToString().PadLeft(4).PadRight(6)}");
                    }
                    else if (k == 1 && _board.GetSnake().ContainsKey(index))
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" [SH] ");
                        Console.ResetColor();
                    }
                    else if (k == 1 && _board.GetSnake().ContainsValue(index))
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" [ST] ");
                        Console.ResetColor();
                    }
                    else if (k == 1 && _board.GetLadder().ContainsKey(index))
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" [LB] ");
                        Console.ResetColor();
                    }
                    else if (k == 1 && _board.GetLadder().ContainsValue(index))
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" [LT] ");
                        Console.ResetColor();
                    }
                    else if (k == 2 && _playerPosition.ContainsValue(index))
                    {
                        List<Player> playersInPosition = GetPlayersInPosition(index);
                        if (playersInPosition.Count > 0)
                        {
                            string playerInitials ="";
                            foreach (Player player in playersInPosition)
                            {
                                playerInitials += player.GetName()[0] + " ";
                            }
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"{playerInitials.PadRight(6)}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("|      ");
                        }
                        // Player player = GetPlayerAtPosition(index);
                        // Console.Write($"|{player.GetName().Substring(0, 1)}     ");
                    }
                    else
                    {
                        Console.Write("|      ");
                    }
                }
                Console.WriteLine("|");
            }
            for (int j = 1; j <= 10; j++)
            {
                Console.Write("|______");
            }
            Console.WriteLine("|");
        }
    }
    public async Task Loading()
    {
        await Task.Delay(2000);
    }
    public void MovePlayer(Player player)
    {
        int currentPosition = GetPlayerPosition(player);
        int newPosition = currentPosition + _lastRollValue[player];
        if (newPosition <= _board.GetSize())
        {
            NotifMessage?.Invoke($"Player {player.GetName()} moves to position {newPosition}");
            if (GetBoard().GetSnake().ContainsKey(newPosition))
            {
                newPosition = HandleSnakeEncounter(player, newPosition);
                NotifMessage?.Invoke($"Player {player.GetName()} encountered a snake! moves to position {newPosition}");
            }
            else if (GetBoard().GetLadder().ContainsKey(newPosition))
            {
                newPosition = HandleLadderEncounter(player, newPosition);
                NotifMessage?.Invoke($"Player {player.GetName()} encountered a ladder! moves to position {newPosition}");
            }
        }
        else
        {
            newPosition = GetBoard().GetSize() - (newPosition - GetBoard().GetSize());
            NotifMessage?.Invoke($"Player {player.GetName()} exceeded the target position.Moving back to position {newPosition}");
        }
        SetPlayerPosition(player, newPosition);
    }
    public bool shouldRollAgain(Player player)
    {
        return _lastRollValue[player] == _dice.GetNumberOfSides();
    }
    public void RollDice(Player player)
    {
        int roll = _dice.GetRoll();
        NotifMessage?.Invoke($"\nPlayer {player.GetName()} rolled a dice {roll}");
        _lastRollValue[player] = roll;
    }
    public int HandleSnakeEncounter(Player player, int currentPosition)
    {
        int snakeEndPosition = _board.GetSnake()[currentPosition];
        return snakeEndPosition;
    }
    public int HandleLadderEncounter(Player player, int currentPosition)
    {
        int ladderEndPosition = _board.GetLadder()[currentPosition];
        return ladderEndPosition;
    }
    public bool IsgameFinished()
    {
        foreach (Player player in _players)
        {
            if (GetPlayerPosition(player) == _board.GetSize())
            {
                NotifMessage?.Invoke($"Player {player.GetName()} has won the game!");
                return true;
            }
        }
        return false;
    }
}