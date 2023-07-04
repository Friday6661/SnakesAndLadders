using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SnakeandLadders.Models;
using SnakeandLadders.Data;

namespace SnakeandLadders.Controllers;

public class PlayerController
{
    private readonly ApplicationDbContext _context;

    public PlayerController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Player> GetPlayers()
    {
        return _context.Players.ToList();
    }
    public Player GetPlayerById(string id)
    {
        return _context.Players.FirstOrDefault(p => p.PlayerId == id);
    }
    public Player CreatePlayer(string name)
    {
        Player player = new Player(name);
        _context.Players.Add(player);
        _context.SaveChanges();
        return player;
    }
    public Player UpdatePlayer(string id, string name, int level)
    {
        Player player = _context.Players.FirstOrDefault(p => p.PlayerId == id);
        if (player != null)
        {
            player.PlayerName = name;
            player.PlayerLevel = level;
            _context.SaveChanges();
        }
        return player;
    }
    public bool DeletePlayer(string id)
    {
        Player player = _context.Players.FirstOrDefault(p => p.PlayerId == id);
        if (player != null)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
