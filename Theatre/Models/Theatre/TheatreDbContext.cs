using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Theatre.Models.Theatre;

public partial class TheatreDbContext : DbContext
{
    public TheatreDbContext()
    {
    }

    public TheatreDbContext(DbContextOptions<TheatreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Spectacle> Spectacles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:TheatreCS");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Actor__3214EC0738A705ED");

            entity.ToTable("Actor");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Spectacle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Spectacl__3214EC072AF43042");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Genre).HasMaxLength(30);
            entity.Property(e => e.Lieu).HasMaxLength(30);
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.Actor).WithMany(p => p.Spectacles)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Spectacles_ToActor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
