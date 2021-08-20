using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fundacion.Models
{
    public partial class db_Fundacion_FinalContext : DbContext
    {
        public db_Fundacion_FinalContext()
        {
        }

        public db_Fundacion_FinalContext(DbContextOptions<db_Fundacion_FinalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AreasxProyecto> AreasxProyectos { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<DepartamentosXproyecto> DepartamentosXproyectos { get; set; }
        public virtual DbSet<EquipoXproyecto> EquipoXproyectos { get; set; }
        public virtual DbSet<Personal> Personals { get; set; }
        public virtual DbSet<Proyecto> Proyectos { get; set; }
        public virtual DbSet<PublicacionesXproyecto> PublicacionesXproyectos { get; set; }
        public virtual DbSet<Validadore> Validadores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=25.89.73.124;Database=db_Fundacion_Final;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Area1)
                    .HasMaxLength(255)
                    .HasColumnName("Area");
            });

            modelBuilder.Entity<AreasxProyecto>(entity =>
            {
                entity.HasKey(e => new { e.IdProyecto, e.IdArea })
                    .HasName("AreasxProyecto$PrimaryKey");

                entity.ToTable("AreasxProyecto");

                entity.HasIndex(e => e.IdArea, "AreasxProyecto$IdArea");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.AreasxProyectos)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AreasxProyecto$[C:\\Database\\Proyectos_be.mdb].AreasAreasxProyecto");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.AreasxProyectos)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("AreasxProyecto$[C:\\Database\\Proyectos_be.mdb].ProyectosAreasxProyecto");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.Property(e => e.Departamento1)
                    .HasMaxLength(255)
                    .HasColumnName("Departamento");
            });

            modelBuilder.Entity<DepartamentosXproyecto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DepartamentosXProyecto");

                entity.Property(e => e.Departamento).HasMaxLength(255);

                entity.Property(e => e.IdProyecto).HasDefaultValueSql("((0))");

                entity.Property(e => e.NroDepartamento).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<EquipoXproyecto>(entity =>
            {
                entity.HasKey(e => new { e.IdProyecto, e.IdPersonal })
                    .HasName("EquipoXProyecto$PrimaryKey");

                entity.ToTable("EquipoXProyecto");

                entity.Property(e => e.IdProyecto).HasColumnName("Id_Proyecto");

                entity.Property(e => e.IdPersonal).HasColumnName("Id_Personal");

                entity.Property(e => e.Coordinador).HasDefaultValueSql("((0))");

                entity.Property(e => e.FuncionTarea)
                    .HasMaxLength(255)
                    .HasColumnName("Funcion_Tarea");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("SSMA_TimeStamp");

                entity.Property(e => e.Texto).HasMaxLength(255);

                entity.HasOne(d => d.IdPersonalNavigation)
                    .WithMany(p => p.EquipoXproyectos)
                    .HasForeignKey(d => d.IdPersonal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EquipoXProyecto$[C:\\Database\\Proyectos_be.mdb].PersonalEquipoXProyecto");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.EquipoXproyectos)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("EquipoXProyecto$[C:\\Database\\Proyectos_be.mdb].ProyectosEquipoXProyecto");
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.ToTable("Personal");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(255);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sector).HasMaxLength(255);

                entity.Property(e => e.Titulo).HasMaxLength(255);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasIndex(e => e.IdArea, "Proyectos$ID_Area");

                entity.Property(e => e.AnioFinalizacion).HasColumnName("Anio_Finalizacion");

                entity.Property(e => e.AnioInicio).HasColumnName("Anio_Inicio");

                entity.Property(e => e.CertConformidad).HasDefaultValueSql("((0))");

                entity.Property(e => e.CertificadoPor).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConsultoresAsoc).HasColumnName("Consultores_Asoc");

                entity.Property(e => e.Contratante).HasMaxLength(255);

                entity.Property(e => e.Departamento).HasMaxLength(255);

                entity.Property(e => e.Dirección).HasMaxLength(255);

                entity.Property(e => e.EnCurso)
                    .HasColumnName("En_Curso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FichaLista)
                    .HasColumnName("Ficha_Lista")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdArea).HasColumnName("ID_Area");

                entity.Property(e => e.MesFinalizacion).HasColumnName("Mes_Finalizacion");

                entity.Property(e => e.MesInicio).HasColumnName("Mes_Inicio");

                entity.Property(e => e.Moneda).HasMaxLength(255);

                entity.Property(e => e.MontoContrato).HasColumnName("Monto_contrato");

                entity.Property(e => e.NroContrato)
                    .HasMaxLength(255)
                    .HasColumnName("Nro_Contrato");

                entity.Property(e => e.PaisRegion)
                    .HasMaxLength(255)
                    .HasColumnName("Pais-region");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("SSMA_TimeStamp");

                entity.Property(e => e.Titulo).HasMaxLength(255);
            });

            modelBuilder.Entity<PublicacionesXproyecto>(entity =>
            {
                entity.HasKey(e => e.IdPublicacion)
                    .HasName("PublicacionesXProyecto$PrimaryKey");

                entity.ToTable("PublicacionesXProyecto");

                entity.HasIndex(e => e.IdProyecto, "PublicacionesXProyecto$Id_Proyecto");

                entity.Property(e => e.IdPublicacion).HasColumnName("Id_Publicacion");

                entity.Property(e => e.Año).HasMaxLength(255);

                entity.Property(e => e.CodigoBcs)
                    .HasMaxLength(255)
                    .HasColumnName("CodigoBCS");

                entity.Property(e => e.IdProyecto).HasColumnName("Id_Proyecto");

                entity.Property(e => e.Medio).HasMaxLength(255);

                entity.Property(e => e.Publicacion).HasMaxLength(255);

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.PublicacionesXproyectos)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PublicacionesXProyecto$[C:\\Database\\Proyectos_be.mdb].ProyectosPublicacionesXProyecto");
            });

            modelBuilder.Entity<Validadore>(entity =>
            {
                entity.Property(e => e.Nombre).HasMaxLength(255);

                entity.Property(e => e.Sector).HasMaxLength(255);

                entity.Property(e => e.Titulo).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
