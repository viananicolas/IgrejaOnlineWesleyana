using System.ComponentModel;

namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Congregacao")]
    public partial class Congregacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Congregacao()
        {
            Membro1 = new HashSet<Membro>();
        }

        public int ID { get; set; }

        public int? IDIgreja { get; set; }

        [StringLength(50)]
        [DisplayName("Congregação")]
        public string Nome { get; set; }

        public int? IDResponsavel { get; set; }

        [StringLength(100)]
        public string Endereco { get; set; }

        [StringLength(50)]
        public string Bairro { get; set; }

        [StringLength(50)]
        public string CEP { get; set; }

        public virtual Igreja Igreja { get; set; }

        public virtual Membro Membro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membro> Membro1 { get; set; }
    }
}
