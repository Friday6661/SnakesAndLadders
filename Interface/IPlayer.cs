namespace IPlayerLib;

public interface IPlayer
{
    string GetId();
    void SetId(string id);
    string GetName();
    void SetName(string name);
}