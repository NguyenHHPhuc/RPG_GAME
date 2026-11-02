using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RPG.DoiTuong;

namespace RPG.Services
{
    public class GameDbContext : DbContext
    {
        //các câu lệnh tạo database Add-Migration CreateGameSaves -Context GameDbContext -Verbose sau đó là Update-Database -Context GameDbContext -Verbose
        public DbSet<GameSave> GameSaves => Set<GameSave>();

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=DESKTOP-SO4EPRT\SQLEXPRESS;
                      Database=RPGSaveDB;
                      Trusted_Connection=True;
                      TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameSave>(entity =>
            {
                entity.ToTable("GameSaves");

                entity.HasKey(save => save.Id);

                entity.Property(save => save.SaveName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(save => save.CurrentMapId)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(save => save.SaveData)
                    .HasColumnType("nvarchar(max)")
                    .IsRequired();

                entity.Property(save => save.SavedAt)
                    .IsRequired();
            });
        }
    }
}