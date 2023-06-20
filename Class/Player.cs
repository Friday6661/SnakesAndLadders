using IPlayerLib;
using shortid;
namespace PlayerLib;

public class Player : IPlayer
{
    private string _id;
    private string _name;
    //private static int _nextId = 1;

    public Player(string name)
    {
        _id = ShortId.Generate();
        _name = name;
    }

    public string GetId()
    {
        return _id;
    }
    public void SetId(string id)
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