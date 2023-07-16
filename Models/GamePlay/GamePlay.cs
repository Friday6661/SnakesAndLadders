using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnakeandLadders.Models;

public class GamePlay : IGamePlay
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GamePlayId { get; set; }
    public int BoardId { get; set; }
    public bool IsGameInProgress { get; set; }
    public Dictionary<int, PlayerDataInGame> PlayerDataInGame { get; set; }
    public virtual Board Board { get; set; }
    public virtual ICollection<Player> Players { get; set; }
    public virtual Dice Dice { get; set; }

    public GamePlay()
    {
        Players = new List<Player>();
        IsGameInProgress = false;
        PlayerDataInGame = new Dictionary<int, PlayerDataInGame>();
    }
}