using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Zookeeper> Zookeepers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.AnimalId).HasName("PK__Animals__DE680F926B112D73");

            entity.HasOne(d => d.Department).WithMany(p => p.Animals).HasConstraintName("FK_Animals_Departments");

            entity.HasOne(d => d.Zookeeper).WithMany(p => p.Animals).HasConstraintName("FK_Animals_Zookeepers");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__C22324222DADADB0");

            entity.Property(e => e.DepartmentId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Zookeeper>(entity =>
        {
            entity.HasKey(e => e.ZookeeperId).HasName("PK__Zookeepe__9F75EAF632A240DA");

            entity.Property(e => e.ZookeeperId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
