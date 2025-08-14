using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TinyUrl.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tinyurldetail> Tinyurldetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Kvignesh005@");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Tinyurldetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tiny_urls_pkey");

            entity.ToTable("tinyurldetails");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('tiny_urls_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.OriginalUrl).HasColumnName("original_url");
            entity.Property(e => e.UrlCode).HasColumnName("url_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
