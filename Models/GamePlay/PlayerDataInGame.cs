using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnakeandLadders.Models;
public class PlayerDataInGame
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlayerDataInGameId { get; set; }
    public int Position { get; set; }
    public int LastRollValue { get; set; }
    public int PlayerInFinishPosition { get; set; }
    public bool RollAgain { get; set; }
    public int TotalRoll { get; set; }
}