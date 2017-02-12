using System.ComponentModel;

namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Distrito")]
    public partial class Distrito
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Distrito()
        {
            Igreja = new HashSet<Igreja>();
            Membro1 = new HashSet<Membro>();
        }

        public int ID { get; set; }

        public int IDRegiao { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Distrito")]
        public string Nome { get; set; }

        public int? IDResponsavel { get; set; }

        public virtual Membro Membro { get; set; }

        public virtual Regiao Regiao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Igreja> Igreja { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membro> Membro1 { get; set; }
    }
}
