using GameControlLib;
namespace DisplayLib;

public class Display
{
    private GameControl _gameControl;
    public Display()
    {
        _gameControl = new GameControl(new DiceLib.Dice(6), new BoardLib.Board(50));
        _gameControl.NotifMessage += DisplayNotifMessage;
    }
    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }
    // public void DisplaySnakeEncounter(string message)
    // {
    //     Console.WriteLine(message);
    // }
    // public void DisplayLadderEncounter(string message)
    // {
    //     Console.WriteLine(message);
    // }
    public void DisplayPlayerInfo(string name, int position)
    {
        Console.WriteLine($"\nPlayer: {name}, Position: {position}");
        Console.WriteLine($"Press Enter to roll the Dice for Player {name}");
    }
    public void DisplayWinningMessage(string message)
    {
        Console.WriteLine(message);
    }
    public void RefreshDisplay()
    {
        Console.Clear();
    }
    public void DisplayNotifMessage(string message)
    {
        Console.WriteLine(message);
    }
}