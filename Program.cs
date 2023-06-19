using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BoardLib;
using GameRunnerLib;

namespace SnakeandLadders;

static class Program
{
    public static async Task Main(string[] args)
    {
        GameRunner gameRunner = new();
        gameRunner.SetupGame();
        Console.Clear();
        await gameRunner.StartGame();
        Console.ReadLine();
    }
}