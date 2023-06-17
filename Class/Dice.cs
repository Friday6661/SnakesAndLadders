using IDiceLib;
namespace DiceLib;
public class Dice : IDice
{
    private int _numberOfSides;
    private Random _random;

    public Dice(int numberOfSides)
    {
        _numberOfSides = numberOfSides;
        _random = new Random();
    }
    public int GetNumberOfSides()
    {
        return _numberOfSides;
    }
    public void SetNumberOfSides(int numberOfSides)
    {
        _numberOfSides = numberOfSides;
    }
    public int GetRoll()
    {
        return _random.Next(1, _numberOfSides + 1);
    }
}