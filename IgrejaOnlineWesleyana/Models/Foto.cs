namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Foto")]
    public partial class Foto
    {
        public int ID { get; set; }

        public int IDMembro { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] DadoFoto { get; set; }

        public virtual Membro Membro { get; set; }
    }
}
