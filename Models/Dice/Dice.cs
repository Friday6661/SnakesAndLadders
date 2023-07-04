using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnakeandLadders.Models;

public class Dice : IDice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DiceId { get; set; }
    public int NumberOfSides { get; set; }

    public Dice(int NumberOfSides)
    {
        this.NumberOfSides = NumberOfSides;
    }
}