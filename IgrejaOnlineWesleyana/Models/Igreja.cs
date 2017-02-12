using System.ComponentModel;

namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Igreja")]
    public partial class Igreja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Igreja()
        {
            Congregacao = new HashSet<Congregacao>();
            Membro1 = new HashSet<Membro>();
        }

        public int ID { get; set; }

        public int IDDistrito { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Igreja")]
        public string Nome { get; set; }

        public int? IDResponsavel { get; set; }

        [StringLength(100)]
        public string Endereco { get; set; }

        [StringLength(50)]
        public string Bairro { get; set; }

        [StringLength(50)]
        public string CEP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Congregacao> Congregacao { get; set; }

        public virtual Distrito Distrito { get; set; }

        public virtual Membro Membro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membro> Membro1 { get; set; }
    }
}
