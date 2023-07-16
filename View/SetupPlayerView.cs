using System;
using SnakeandLadders.Controllers;

namespace SnakeandLadders.Views;

public class PlayerView
{
    private readonly PlayerController _playerController;
    public PlayerView(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void ShowPlayers()
    {
        var players = _playerController.GetPlayers();
        Console.WriteLine("Player List: ");
        foreach (var player in players)
        {
            Console.WriteLine($"Player ID: {player.PlayerId}, Player Name: {player.PlayerName}, Player Level: {player.PlayerLevel}");
        }
    }
    public void AddPlayer()
    {
        Console.Write("Enter player Name: ");
        string name = Console.ReadLine();

        if (name.Length < 2)
        {
            Console.WriteLine("Player name must have at least 2 characters.");
            return;
        }
        if (_playerController.GetPlayers().Any(p => p.PlayerName.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Player name is already taken.");
            return;
        }
        var player = _playerController.CreatePlayer(name);
        Console.WriteLine($"Player Added: \nPlayerID: {player.PlayerId}, \nPlayer Name: {player.PlayerName}, \nPlayer Level: {player.PlayerLevel}");
    }
}