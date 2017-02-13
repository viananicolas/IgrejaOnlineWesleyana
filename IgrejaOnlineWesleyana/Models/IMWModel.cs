using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public partial class IMWModel : IdentityDbContext<ApplicationUser>
    {
        public IMWModel()
            : base("name=IMWModel")
        {
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
        }
        public static IMWModel Create()
        {
            return new IMWModel();
        }

        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<Congregacao> Congregacao { get; set; }
        public virtual DbSet<Conjuge> Conjuge { get; set; }
        public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Filho> Filho { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<GrauInstrucao> GrauInstrucao { get; set; }
        public virtual DbSet<Igreja> Igreja { get; set; }
        public virtual DbSet<Membro> Membro { get; set; }
        public virtual DbSet<Regiao> Regiao { get; set; }
        public virtual DbSet<TipoConjuge> TipoConjuge { get; set; }
        public DbSet<IdentityUserLogin> IdentityUserLogin { get; set; }
        public DbSet<IdentityUserClaim> IdentityUserClaim { get; set; }
        public DbSet<IdentityUserRole> IdentityUserRole { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cidade>()
                .Property(e => e.Cidade1)
                .IsUnicode(false);

            modelBuilder.Entity<Cidade>()
                .HasMany(e => e.Membro)
                .WithOptional(e => e.Cidade)
                .HasForeignKey(e => e.IDNaturalidade);

            modelBuilder.Entity<Cidade>()
                .HasMany(e => e.Membro1)
                .WithOptional(e => e.Cidade1)
                .HasForeignKey(e => e.IDCidade);

            modelBuilder.Entity<Congregacao>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Congregacao>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Congregacao>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Congregacao>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<Congregacao>()
                .HasMany(e => e.Membro1)
                .WithOptional(e => e.Congregacao)
                .HasForeignKey(e => e.IDCongregacao);

            modelBuilder.Entity<Conjuge>()
                .Property(e => e.NomeConjuge)
                .IsUnicode(false);

            modelBuilder.Entity<Conjuge>()
                .Property(e => e.TelefoneConjuge)
                .IsUnicode(false);

            modelBuilder.Entity<Conjuge>()
                .Property(e => e.EmailConjuge)
                .IsUnicode(false);

            modelBuilder.Entity<Distrito>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Distrito>()
                .HasMany(e => e.Igreja)
                .WithRequired(e => e.Distrito)
                .HasForeignKey(e => e.IDDistrito)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Distrito>()
                .HasMany(e => e.Membro1)
                .WithOptional(e => e.Distrito)
                .HasForeignKey(e => e.IDDistrito);

            modelBuilder.Entity<Estado>()
                .Property(e => e.UF)
                .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .Property(e => e.Estado1)
                .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Cidade)
                .WithRequired(e => e.Estado)
                .HasForeignKey(e => e.IDEstado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Membro)
                .WithOptional(e => e.Estado)
                .HasForeignKey(e => e.IDEstado);

            modelBuilder.Entity<Filho>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<GrauInstrucao>()
                .Property(e => e.TipoGrau)
                .IsUnicode(false);

            modelBuilder.Entity<GrauInstrucao>()
                .HasMany(e => e.Membro)
                .WithOptional(e => e.GrauInstrucao)
                .HasForeignKey(e => e.IDGrauInstrucao);

            modelBuilder.Entity<Igreja>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Igreja>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Igreja>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Igreja>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<Igreja>()
                .HasMany(e => e.Congregacao)
                .WithOptional(e => e.Igreja)
                .HasForeignKey(e => e.IDIgreja);

            modelBuilder.Entity<Igreja>()
                .HasMany(e => e.Membro1)
                .WithOptional(e => e.Igreja)
                .HasForeignKey(e => e.IDIgreja);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Nacionalidade)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Complemento)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.EstadoCivil)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.RG)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.OrgaoExpedidor)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .Property(e => e.CPF)
                .IsUnicode(false);

            modelBuilder.Entity<Membro>()
                .HasMany(e => e.Congregacoes)
                .WithOptional(e => e.Membro)
                .HasForeignKey(e => e.IDResponsavel);

            modelBuilder.Entity<Membro>()
                .HasMany(e => e.Conjugue)
                .WithRequired(e => e.Membro)
                .HasForeignKey(e => e.IDEsposa)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Membro>()
                .HasMany(e => e.Distritos)
                .WithOptional(e => e.Membro)
                .HasForeignKey(e => e.IDResponsavel);

            modelBuilder.Entity<Membro>()
                .HasMany(e => e.Filhos)
                .WithRequired(e => e.Membro)
                .HasForeignKey(e => e.IDMae)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Membro>()
                .HasMany(e => e.Foto1)
                .WithRequired(e => e.Membro)
                .HasForeignKey(e => e.IDMembro)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Membro>()
                .HasMany(e => e.Igrejas)
                .WithOptional(e => e.Membro)
                .HasForeignKey(e => e.IDResponsavel);

            modelBuilder.Entity<Membro>()
                .HasMany(e => e.Regioes)
                .WithOptional(e => e.Membro1)
                .HasForeignKey(e => e.IDResponsavel);

            modelBuilder.Entity<Regiao>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Regiao>()
                .HasMany(e => e.Distrito)
                .WithRequired(e => e.Regiao)
                .HasForeignKey(e => e.IDRegiao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Regiao>()
                .HasMany(e => e.Membro)
                .WithRequired(e => e.Regiao)
                .HasForeignKey(e => e.IDRegiao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoConjuge>()
                .Property(e => e.DescricaoTipo)
                .IsUnicode(false);

            modelBuilder.Entity<TipoConjuge>()
                .HasMany(e => e.Conjuge)
                .WithRequired(e => e.TipoConjuge)
                .HasForeignKey(e => e.IDTipoConjuge)
                .WillCascadeOnDelete(false);
        }
    }
}
