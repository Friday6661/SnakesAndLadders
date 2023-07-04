using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnakeandLadders.Models;
public class Snake
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SnakeId { get; set; }
    public int HeadPosition { get; set; }
    public int TailPosition { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set; }
}