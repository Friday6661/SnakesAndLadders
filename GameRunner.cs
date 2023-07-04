// using GameControlLib;
// using PlayerLib;
// using DisplayLib;
// using System.Threading;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// namespace GameRunnerLib;

// public class GameRunner
// {
//     private GameControl _gameControl;
//     private Display _display;
//     public event Action<string> NotifMessage;
//     public GameRunner()
//     {
//         _gameControl = new GameControl(new DiceLib.Dice(6), new BoardLib.Board(50));
//         _display = new Display();
//         _gameControl.NotifMessage += _display.DisplayNotifMessage;
//     }

//     public void SetupGame()
//     {
//         Dictionary<int, int> snakes = new Dictionary<int, int>()
//         {
//             {16, 6},
//             {47, 26},
//             {49, 11},
//         };
//         Dictionary<int, int> ladders = new Dictionary<int, int>()
//         {
//             {3, 22},
//             {5, 25},
//             {12, 42}
//         };
//         _gameControl.IntitalizeSnakesAndLadders(snakes, ladders);
//         _display.RefreshDisplay();
//         _display.DisplayMessage("=================[ Welcome to Snakes and Ladders ]=================");
//         _display.DisplayMessage("Setup Game: ");
//         _display.DisplayMessage("Enter the Number of Players: ");
//         _gameControl.SetupPlayer();
//     }
//     public async Task StartGame()
//     {
//         _display.DisplayMessage("=================[ Start Game ]=================");;
//         Task loadingTask = _gameControl.Loading();
//         _display.DisplayMessage("Game Starting.....");
//         await loadingTask;
//         while (!_gameControl.IsgameFinished())
//         {
//             foreach (Player player in _gameControl.GetPlayers())
//             {
//                 _display.DisplayPlayerInfo(_gameControl.GetPlayerName(player), _gameControl.GetPlayerPosition(player));
//                 Console.ReadLine();
//                 bool rollAgain = true;
//                 int totalRolls = 0;

//                 while (rollAgain && totalRolls < 2)
//                 {
//                     _gameControl.RollDice(player);
//                     _gameControl.MovePlayer(player);
//                     _gameControl.DrawBoard();
//                     totalRolls++;
//                     if (_gameControl.shouldRollAgain(player))
//                     {
//                         _display.DisplayMessage($"Player {player.GetName()} rolled a dice and got a {_gameControl.GetNumberOfSides()}! Roll the dice again.");
//                         Console.ReadLine();
//                     }
//                     else
//                     {
//                         rollAgain = false;
//                     }
//                     AllPlayerPositions();
//                 }
//             }
//         }
//     }
//     private void AllPlayerPositions()
//     {
//         Dictionary<Player, int> playerPositions = _gameControl.GetAllPlayerPositions();
//         foreach (var entry in playerPositions)
//         {
//             _display.DisplayMessage($"Player {entry.Key.GetName()} Position: {entry.Value}");
//         }
//     }
// }