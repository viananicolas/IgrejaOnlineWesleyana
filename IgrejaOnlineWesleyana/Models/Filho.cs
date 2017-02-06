using Newtonsoft.Json;

namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Filho")]
    public partial class Filho
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "datanascimentofilho")]
        public DateTime DataNascimento { get; set; }

        public int IDMae { get; set; }

        public virtual Membro Membro { get; set; }
    }
}
