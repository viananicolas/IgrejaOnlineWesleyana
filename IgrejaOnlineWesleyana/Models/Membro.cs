using IgrejaOnlineWesleyana.Extensions;

namespace IgrejaOnlineWesleyana.Models
{
    using FluentValidation.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Membro")]
    [Validator(typeof(CPFValidator))]
    public partial class Membro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Membro()
        {
            Congregacoes = new HashSet<Congregacao>();
            Conjugue = new HashSet<Conjugue>();
            Distritos = new HashSet<Distrito>();
            Filhos = new HashSet<Filho>();
            Foto1 = new HashSet<Foto>();
            Igrejas = new HashSet<Igreja>();
            Regioes = new HashSet<Regiao>();
        }

        public int ID { get; set; }
        [Display(Name ="Região")]

        public int IDRegiao { get; set; }
        [Display(Name = "Distritos")]

        public int? IDDistrito { get; set; }
        [Display(Name = "Igrejas")]

        public int? IDIgreja { get; set; }
        [Display(Name = "Congregação")]

        public int? IDCongregacao { get; set; }

        [Required]
        [Display(Name = "Nome completo")]
        [StringLength(100, MinimumLength=3, ErrorMessage="O nome é muito curto")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "O email é inválido")]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name="Data de Nascimento")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [StringLength(50)]
        public string Telefone { get; set; }

        [StringLength(50)]
        public string Celular { get; set; }

        [StringLength(50)]
        public string Nacionalidade { get; set; }
        [Display(Name = "Naturalidade")]

        public int? IDNaturalidade { get; set; }
        [Display(Name = "Endereço e número")]

        [StringLength(100)]
        public string Endereco { get; set; }

        [StringLength(50)]
        public string Complemento { get; set; }

        [StringLength(50)]
        public string Bairro { get; set; }
        [Display(Name = "Cidade")]

        public int? IDCidade { get; set; }
        [Display(Name = "Estado")]

        public int? IDEstado { get; set; }

        [StringLength(10)]
        public string CEP { get; set; }
        [Display(Name = "Estado civil")]

        [StringLength(50)]
        public string EstadoCivil { get; set; }

        [StringLength(50)]
        public string RG { get; set; }
        [Display(Name = "Órgão expedidor")]

        [StringLength(50)]
        public string OrgaoExpedidor { get; set; }

        [StringLength(50)]
        public string CPF { get; set; }
        [Display(Name = "Grau de instrução")]

        public int? IDGrauInstrucao { get; set; }

        [Column(TypeName = "image")]
        public byte[] Foto { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual Cidade Cidade1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Congregacao> Congregacoes { get; set; }

        public virtual Congregacao Congregacao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conjugue> Conjugue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Distrito> Distritos { get; set; }

        public virtual Distrito Distrito { get; set; }

        public virtual Estado Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Filho> Filhos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Foto> Foto1 { get; set; }

        public virtual GrauInstrucao GrauInstrucao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Igreja> Igrejas { get; set; }

        public virtual Igreja Igreja { get; set; }

        public virtual Regiao Regiao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Regiao> Regioes { get; set; }
    }
}
