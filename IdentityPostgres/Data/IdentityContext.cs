using System;
using System.Collections.Generic;
using IdentityPostgres.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data;

public partial class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Account { get; set; }

    public virtual DbSet<AccountLogin> AccountLogin { get; set; }

    public virtual DbSet<AccountPassword> AccountPassword { get; set; }

    public virtual DbSet<AccountProvider> AccountProvider { get; set; }

    public virtual DbSet<Config> Config { get; set; }

    public virtual DbSet<ConfigMail> ConfigMail { get; set; }

    public virtual DbSet<ConfigMailProvider> ConfigMailProvider { get; set; }

    public virtual DbSet<ConfigMailTemplate> ConfigMailTemplate { get; set; }

    public virtual DbSet<ConfigMailType> ConfigMailType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)");

            entity.HasOne(d => d.Provider).WithMany(p => p.Account)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_account_provider");
        });

        modelBuilder.Entity<AccountLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account_login");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountLogin).HasConstraintName("fk_account_login_account");
        });

        modelBuilder.Entity<AccountPassword>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("pk_account_password");

            entity.Property(e => e.AccountId).ValueGeneratedNever();

            entity.HasOne(d => d.Account).WithOne(p => p.AccountPassword)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_password_account");
        });

        modelBuilder.Entity<AccountProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account_provider");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)");

            entity.HasOne(d => d.Mail).WithMany(p => p.Config).HasConstraintName("fk_config_config_mail");
        });

        modelBuilder.Entity<ConfigMail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config_mail");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)");

            entity.HasOne(d => d.Provider).WithOne(p => p.ConfigMail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_config_mail_config_mail_provider");
        });

        modelBuilder.Entity<ConfigMailProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config_mail_provider");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ConfigMailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config_mail_template");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)");

            entity.HasOne(d => d.Mail).WithMany(p => p.ConfigMailTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_config_mail_template_config_mail");

            entity.HasOne(d => d.Type).WithMany(p => p.ConfigMailTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_config_mail_template_config_mail_type");
        });

        modelBuilder.Entity<ConfigMailType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config_mail_type");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
