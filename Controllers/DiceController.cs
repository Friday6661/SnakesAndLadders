using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SnakeandLadders.Models;
using SnakeandLadders.Data;

namespace SnakeandLadders.Controllers;

public class DiceController
{
    private readonly ApplicationDbContext _context;
    public DiceController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Dice> GetDices()
    {
        return _context.Dices.ToList();
    }
    public Dice GetDiceByNumberOfSides(int numberOfSides)
    {
        return _context.Dices.FirstOrDefault(d => d.NumberOfSides == numberOfSides);
    }
    public Dice CreateDice(int numberOfSides)
    {
        Dice dice = new Dice(numberOfSides);
        _context.Dices.Add(dice);
        _context.SaveChanges();
        return dice;
    }
    public int RollDice(Dice dice)
    {
        Random random = new Random();
        return random.Next(1, dice.NumberOfSides + 1);
    }
}