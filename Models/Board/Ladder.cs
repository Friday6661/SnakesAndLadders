using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnakeandLadders.Models;
public class Ladder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LadderId { get; set; }
    public int BottomPosition { get; set; }
    public int TopPosition { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set;}
}