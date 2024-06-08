using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CorporatePortalAPI.Models;

public partial class CorporatePortalContext : DbContext
{
    public CorporatePortalContext()
    {
    }

    public CorporatePortalContext(DbContextOptions<CorporatePortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entry> Entries { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=CorporatePortal; Username=postgres; Password=111;").UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Entry_pkey");

            entity.ToTable("Entry");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(d => d.Task).WithMany(p => p.Entries)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Entry_TaskId_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Project_pkey");

            entity.ToTable("Project");

            entity.Property(e => e.Code).HasMaxLength(55);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_ProjectId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
