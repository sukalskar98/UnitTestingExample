using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UnitTestingExample.Models;

public partial class DemoDb2Context : DbContext
{
    public DemoDb2Context()
    {
    }

    public DemoDb2Context(DbContextOptions<DemoDb2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-VVTG1LT\\SQLEXPRESS;Initial Catalog=DemoDB2;Integrated Security=SSPI;TrustServerCertificate=False;Encrypt=False;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
