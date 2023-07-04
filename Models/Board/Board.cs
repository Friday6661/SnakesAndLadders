using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnakeandLadders.Models;
public enum CellType
{
    Normal,
    SnakeHead,
    SnakeTail,
    LadderBottom,
    LadderTop,
    Player
}
public class Board
{
    [Key]
    public int BoardId { get; set; }
    public int Size { get; set; }
    public virtual ICollection<Snake> Snakes { get; set; }
    public virtual ICollection<Ladder> Ladders { get; set; }
}