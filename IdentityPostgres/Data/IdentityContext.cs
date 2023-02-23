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

    public virtual DbSet<ConfigMailType> ConfigMailType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account");

            entity.ToTable("account", "identity");

            entity.HasIndex(e => new { e.ProviderId, e.Email }, "u_account").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)")
                .HasColumnName("created_on");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            entity.Property(e => e.Verified).HasColumnName("verified");
            entity.Property(e => e.VerifiedOn).HasColumnName("verified_on");

            entity.HasOne(d => d.Provider).WithMany(p => p.Account)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_account_provider");
        });

        modelBuilder.Entity<AccountLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account_login");

            entity.ToTable("account_login", "identity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)")
                .HasColumnName("created_on");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.Successful).HasColumnName("successful");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountLogin)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("fk_account_login_account");
        });

        modelBuilder.Entity<AccountPassword>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("pk_account_password");

            entity.ToTable("account_password", "identity");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("account_id");
            entity.Property(e => e.Hash)
                .HasMaxLength(60)
                .HasColumnName("hash");
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

            entity.HasOne(d => d.Account).WithOne(p => p.AccountPassword)
                .HasForeignKey<AccountPassword>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_password_account");
        });

        modelBuilder.Entity<AccountProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account_provider");

            entity.ToTable("account_provider", "identity");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config");

            entity.ToTable("config", "identity");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountVerificationRequired).HasColumnName("account_verification_required");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)")
                .HasColumnName("created_on");
            entity.Property(e => e.MailId).HasColumnName("mail_id");
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

            entity.HasOne(d => d.Mail).WithMany(p => p.Config)
                .HasForeignKey(d => d.MailId)
                .HasConstraintName("fk_config_config_mail");
        });

        modelBuilder.Entity<ConfigMail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config_mail");

            entity.ToTable("config_mail", "identity");

            entity.HasIndex(e => e.ProviderId, "u_config_mail").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(256)
                .HasColumnName("api_key");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)")
                .HasColumnName("created_on");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

            entity.HasOne(d => d.Provider).WithOne(p => p.ConfigMail)
                .HasForeignKey<ConfigMail>(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_config_mail_config_mail_provider");
        });

        modelBuilder.Entity<ConfigMailProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config_mail_provider");

            entity.ToTable("config_mail_provider", "identity");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ConfigMailType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_config_mail_type");

            entity.ToTable("config_mail_type", "identity");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
