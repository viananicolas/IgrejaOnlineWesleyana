namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoConjugue")]
    public partial class TipoConjugue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoConjugue()
        {
            Conjugue = new HashSet<Conjugue>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string DescricaoTipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conjugue> Conjugue { get; set; }
    }
}
