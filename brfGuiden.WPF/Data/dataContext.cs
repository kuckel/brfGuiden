using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace brfGuiden.Models;

public partial class dataContext : DbContext
{
    public dataContext()
    {
    }

    public dataContext(DbContextOptions<dataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bostad> Bostad { get; set; }

    public virtual DbSet<Forening> Forening { get; set; }

    public virtual DbSet<Leverantor> Leverantorer { get; set; }

    public virtual DbSet<Medlem> Medlemmar { get; set; }

    public virtual DbSet<StyrelseMedlem> StyrelseMedlemmar { get; set; }

  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"DataSource={AppDomain.CurrentDomain.BaseDirectory + "\\data\\data.db"}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bostad>(entity =>
        {
            entity.ToTable("Bostad");

            entity.Property(e => e.BostadId)
                .HasColumnType("varchar(40)")
                .HasColumnName("BostadID");
            entity.Property(e => e.Adress).HasColumnType("varchar(40)");
            entity.Property(e => e.ExternNr).HasColumnType("varchar(5)");
            entity.Property(e => e.InterntNr).HasColumnType("varchar(5)");
            entity.Property(e => e.Rum).HasDefaultValueSql("0");

            entity.HasMany(d => d.medlemmar).WithMany(p => p.bostad)
                .UsingEntity<Dictionary<string, object>>(
                    "MedlemBostad",
                    r => r.HasOne<Medlem>().WithMany()
                        .HasForeignKey("MedlemIdRef")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Bostad>().WithMany()
                        .HasForeignKey("BostadIdRef")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("BostadIdRef", "MedlemIdRef");
                        j.ToTable("Medlem_Bostad");
                    });
        });

        modelBuilder.Entity<Forening>(entity =>
        {
            entity.ToTable("Forening");

            entity.Property(e => e.ForeningId)
                .HasColumnType("varchar(40)")
                .HasColumnName("ForeningID");
            entity.Property(e => e.Adress).HasColumnType("varchar(40)");
            entity.Property(e => e.Namn).HasColumnType("varchar(40)");
            entity.Property(e => e.OrgNr).HasColumnType("varchar(12)");
            entity.Property(e => e.PostNr).HasColumnType("varchar(6)");
            entity.Property(e => e.PostOrt).HasColumnType("varchar(40)");
        });

        modelBuilder.Entity<Leverantor>(entity =>
        {
            entity.ToTable("Leverantor");

            entity.Property(e => e.LeverantorId)
                .HasColumnType("varchar(40)")
                .HasColumnName("LeverantorID");
            entity.Property(e => e.Adress).HasColumnType("varchar(40)");
            entity.Property(e => e.Betyg).HasDefaultValueSql("0");
            entity.Property(e => e.Hemsida).HasColumnType("varchar(40)");
            entity.Property(e => e.Namn).HasColumnType("varchar(40)");
            entity.Property(e => e.OrgNr).HasColumnType("varchar(12)");
            entity.Property(e => e.PostNr).HasColumnType("varchar(6)");
            entity.Property(e => e.PostOrt).HasColumnType("varchar(40)");
            entity.Property(e => e.Telefon).HasColumnType("varchar(40)");
        });

        modelBuilder.Entity<Medlem>(entity =>
        {
            entity.ToTable("Medlem");

            entity.Property(e => e.MedlemId)
                .HasColumnType("varchar(40)")
                .HasColumnName("MedlemID");
            entity.Property(e => e.MedlemNr).HasColumnType("varchar(12)");
            entity.Property(e => e.Namn).HasColumnType("varchar(40)");
        });

        modelBuilder.Entity<StyrelseMedlem>(entity =>
        {
            entity.ToTable("StyrelseMedlem");

            entity.Property(e => e.StyrelseMedlemId)
                .HasColumnType("varchar(40)")
                .HasColumnName("StyrelseMedlemID");
            entity.Property(e => e.Epost).HasColumnType("varchar(40)");
            entity.Property(e => e.Namn).HasColumnType("varchar(40)");
            entity.Property(e => e.Roll).HasColumnType("varchar(15)");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
