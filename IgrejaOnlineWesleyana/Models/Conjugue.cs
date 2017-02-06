using Newtonsoft.Json;

namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Conjugue")]
    public partial class Conjugue
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [StringLength(100)]
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "datanascimento")]
        public DateTime? DataNascimento { get; set; }

        [StringLength(50)]
        [JsonProperty(PropertyName = "telefone")]
        public string Telefone { get; set; }
        [JsonProperty(PropertyName = "idtipo")]
        public int? IDTipo { get; set; }

        [JsonProperty(PropertyName = "email")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        [StringLength(50)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "idesposa")]
        public int? IDEsposa { get; set; }

        public virtual Membro Membro { get; set; }

        public virtual TipoConjugue TipoConjugue { get; set; }
    }
}
