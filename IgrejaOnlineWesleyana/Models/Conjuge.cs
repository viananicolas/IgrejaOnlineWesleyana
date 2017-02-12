using Newtonsoft.Json;

namespace IgrejaOnlineWesleyana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Conjuge")]
    public partial class Conjuge
    {
        [Key]
        [JsonProperty(PropertyName = "idconjuge")]
        public int IDConjuge { get; set; }

        [StringLength(100)]
        [JsonProperty(PropertyName = "nomeconjuge")]
        public string NomeConjuge { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "datanascimentoconjuge")]
        public DateTime? DataNascimentoConjuge { get; set; }

        [StringLength(50)]
        [JsonProperty(PropertyName = "telefoneconjuge")]
        public string TelefoneConjuge { get; set; }
        [JsonProperty(PropertyName = "idtipo")]
        public int? IDTipoConjuge { get; set; }

        [JsonProperty(PropertyName = "emailconjuge")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        [StringLength(50)]
        public string EmailConjuge { get; set; }
        [JsonProperty(PropertyName = "idesposa")]
        public int? IDEsposa { get; set; }

        public virtual Membro Membro { get; set; }

        public virtual TipoConjuge TipoConjuge { get; set; }
    }
}
