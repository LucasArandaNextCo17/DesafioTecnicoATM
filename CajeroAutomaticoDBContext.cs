using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ATM
{
    public partial class CajeroAutomaticoDBContext : DbContext
    {
        public CajeroAutomaticoDBContext()
        {
        }

        public CajeroAutomaticoDBContext(DbContextOptions<CajeroAutomaticoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Operacione> Operaciones { get; set; } = null!;
        public virtual DbSet<Tarjeta> Tarjetas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-206N7SB\\SQLEXPRESS;Database=CajeroAutomaticoDB;User=sa;Password=123456;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operacione>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CodigoOperacion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.Property(e => e.Idtarjeta).HasColumnName("IDTarjeta");

                entity.Property(e => e.MontoRetirado).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdtarjetaNavigation)
                    .WithMany(p => p.Operaciones)
                    .HasForeignKey(d => d.Idtarjeta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Operacion__IDTar__3B75D760");
            });

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Numero)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Pin)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
