using System.Collections.Generic;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;
public class PizzaContext : DbContext
{
    public DbSet<Pizza> Pizzas { get; set; }
    public PizzaContext(DbContextOptions<PizzaContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>()
            .HasIndex(p => p.Name)
            .IsUnique();
    }
}