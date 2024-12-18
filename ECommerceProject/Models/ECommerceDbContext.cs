using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ECommerceProject.Models;

public partial class ECommerceDbContext : DbContext
{
    public ECommerceDbContext()
    {
    }

    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<PaymentInfo> PaymentInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=mydb;user=root;password=LjFf12x:)", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PRIMARY");

            entity
                .ToTable("Comment")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CommentId, "commentId_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserUserId, "fk_Comment_User1_idx");

            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.CommentText)
                .HasColumnType("text")
                .HasColumnName("commentText");
            entity.Property(e => e.CommentTitle)
                .HasMaxLength(45)
                .HasColumnName("commentTitle");
            entity.Property(e => e.UserUserId).HasColumnName("User_userId");

            entity.HasOne(d => d.UserUser).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Comment_User1");
        });

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<PaymentInfo>(entity =>
        {
            entity.HasKey(e => e.PaymentInfoId).HasName("PRIMARY");

            entity
                .ToTable("PaymentInfo")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.UserUserId, "fk_PaymentInfo_User1_idx");

            entity.Property(e => e.PaymentInfoId)
                .ValueGeneratedNever()
                .HasColumnName("paymentInfoId");
            entity.Property(e => e.PaymentInfoCreditCardNumber).HasColumnName("paymentInfoCreditCardNumber");
            entity.Property(e => e.PaymentInfoCreditCardUserName)
                .HasMaxLength(45)
                .HasColumnName("paymentInfoCreditCardUserName");
            entity.Property(e => e.PaymentInfoCvv)
                .HasMaxLength(45)
                .HasColumnName("paymentInfoCVV");
            entity.Property(e => e.PaymentInfoExpirationDate)
                .HasMaxLength(45)
                .HasColumnName("paymentInfoExpirationDate");
            entity.Property(e => e.PaymentInfoTitle)
                .HasMaxLength(45)
                .HasColumnName("paymentInfoTitle");
            entity.Property(e => e.UserUserId).HasColumnName("User_userId");

            entity.HasOne(d => d.UserUser).WithMany(p => p.PaymentInfos)
                .HasForeignKey(d => d.UserUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PaymentInfo_User1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity
                .ToTable("Product")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CommentCommentId, "fk_Product_Comment1_idx");

            entity.HasIndex(e => e.SellerSellerId, "fk_Product_Seller1_idx");

            entity.HasIndex(e => e.ProductId, "productId_UNIQUE").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.CommentCommentId).HasColumnName("Comment_commentId");
            entity.Property(e => e.ProductDescription)
                .HasColumnType("text")
                .HasColumnName("productDescription");
            entity.Property(e => e.ProductPrice).HasColumnName("productPrice");
            entity.Property(e => e.ProductTitle)
                .HasMaxLength(45)
                .HasColumnName("productTitle");
            entity.Property(e => e.SellerSellerId).HasColumnName("Seller_sellerId");

            entity.HasOne(d => d.CommentComment).WithMany(p => p.Products)
                .HasForeignKey(d => d.CommentCommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_Comment1");

            entity.HasOne(d => d.SellerSeller).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellerSellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_Seller1");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PRIMARY");

            entity
                .ToTable("Seller")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.UserUserId, "fk_Seller_User1_idx");

            entity.Property(e => e.SellerId).HasColumnName("sellerId");
            entity.Property(e => e.SellerRating).HasColumnName("sellerRating");
            entity.Property(e => e.SellerTitle)
                .HasMaxLength(45)
                .HasColumnName("sellerTitle");
            entity.Property(e => e.UserUserId).HasColumnName("User_userId");

            entity.HasOne(d => d.UserUser).WithMany(p => p.Sellers)
                .HasForeignKey(d => d.UserUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Seller_User1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity
                .ToTable("User")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.UserEmail, "userEmail_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserId, "userId_UNIQUE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserBirthDate)
                .HasMaxLength(45)
                .HasColumnName("userBirthDate");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(45)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserGender).HasColumnName("userGender");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(45)
                .HasColumnName("userPassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
