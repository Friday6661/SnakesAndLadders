using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using shortid;

namespace SnakeandLadders.Models;
public class Player : IPlayerLib
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string PlayerId { get; set; }
    [Required]
    public string? PlayerName { get; set; }
    public int PlayerLevel { get; set; }

    public Player(string name)
    {
        PlayerId = ShortId.Generate();
        PlayerName = name;
    }

    public Player()
    {
        
    }
}