public interface IBoard
{
    int Size { get; }
    IDictionary<int, int> Snakes { get; }
    IDictionary<int, int> Ladders { get; }
}