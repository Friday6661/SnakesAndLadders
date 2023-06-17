using IPlayerLib;
namespace PlayerLib;

public class Player : IPlayer
{
    private int _id;
    private string _name;
    private static int _nextId = 1;

    public Player(string name)
    {
        _id = _nextId;
        _name = name;
    }

    public int GetId()
    {
        return _id;
    }
    public void SetId(int id)
    {
        _id = id;
    }
    public string GetName()
    {
        return _name;
    }
    public void SetName(string name)
    {
        _name = name;
    }
}