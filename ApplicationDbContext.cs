using Microsoft.EntityFrameworkCore;
using SnakeandLadders.Models;

namespace SnakeandLadders.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Board> Boards { get; private set; }
    public DbSet<Player> Players { get; private set; }
    public DbSet<Snake> Snakes { get; private set; }
    public DbSet<Ladder> Ladders { get; private set; }
    public DbSet<Dice> Dices { get; private set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = "/home/friday/SnakesAndLadders/SnakesAndLadders.db";
        optionsBuilder.UseSqlite($"Data Source={dbPath};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Board>()
            .HasMany(b => b.Snakes)
            .WithOne(s => s.Board)
            .HasForeignKey(s => s.BoardId);

        modelBuilder.Entity<Board>()
            .HasMany(b => b.Ladders)
            .WithOne(l => l.Board)
            .HasForeignKey(l => l.BoardId);
    }
}