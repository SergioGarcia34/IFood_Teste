using System;
using IfoodAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IfoodAPI.Repository
{
    public partial class iFoodDBContext : DbContext
    {
      
        public iFoodDBContext(DbContextOptions<iFoodDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Produto> Produto { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProd);

                entity.ToTable("PRODUTO");

                entity.Property(e => e.IdProd).HasColumnName("ID_PROD");

                entity.Property(e => e.DescProd)
                    .HasColumnName("DESC_PROD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DtInclusao)
                    .HasColumnName("DT_INCLUSAO")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
          
                entity.Property(e => e.NomProd)
                    .IsRequired()
                    .HasColumnName("NOM_PROD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StrImag).HasColumnName("STR_IMAG");

                entity.Property(e => e.VlVend)
                    .HasColumnName("VL_VEND")
                    .HasColumnType("decimal(18, 0)");

            });
                }           

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
