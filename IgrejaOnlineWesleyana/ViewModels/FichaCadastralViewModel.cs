using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;
using IgrejaOnlineWesleyana.Models;
using Newtonsoft.Json;

namespace IgrejaOnlineWesleyana.ViewModels
{
    public class FichaCadastralViewModel
    {
        [JsonProperty(PropertyName="id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome completo")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome é muito curto")]
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "O email é inválido")]
        [StringLength(50)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "datanascimento")]
        public DateTime DataNascimento { get; set; }

        [JsonProperty(PropertyName = "telefone")]
        [StringLength(50)]
        public string Telefone { get; set; }
        [JsonProperty(PropertyName = "celular")]
        [StringLength(50)]
        public string Celular { get; set; }
        [StringLength(50)]
        [JsonProperty(PropertyName = "rg")]

        public string RG { get; set; }
        [Display(Name = "Órgão expedidor")]
        [JsonProperty(PropertyName = "orgaoexpedidor")]

        [StringLength(50)]
        public string OrgaoExpedidor { get; set; }

        [StringLength(50)]
        [JsonProperty(PropertyName = "cpf")]
        public string CPF { get; set; }
        [JsonProperty(PropertyName = "nacionalidade")]
        public string Nacionalidade { get; set; }
        [JsonProperty(PropertyName = "cep")]
        public string CEP { get; set; }
        [Display(Name = "Endereço e número")]
        [JsonProperty(PropertyName = "endereco")]
        [StringLength(100)]
        public string Endereco { get; set; }
        [JsonProperty(PropertyName = "complemento")]
        [StringLength(50)]
        public string Complemento { get; set; }
        [JsonProperty(PropertyName = "bairro")]
        [StringLength(50)]
        public string Bairro { get; set; }
        [Display(Name = "Estado Civil")]
        [JsonProperty(PropertyName = "estadocivil")]
        [StringLength(50)]
        public string EstadoCivil { get; set; }

        public virtual Regiao Regiao { get; set; }
        public virtual Distrito Distrito { get; set; }
        public virtual Igreja Igreja { get; set; }
        public virtual Congregacao Congregacao { get; set; }
        public virtual Conjugue ConjugueMembro { get; set; }
        public virtual ICollection<Conjugue> Conjugue { get; set; }
        public virtual Estado Estado { get; set; }
        public byte[] Foto { get; set; }

        public virtual Filho Filho { get; set; }
        public virtual GrauInstrucao GrauInstrucao { get; set; }
        [JsonProperty(PropertyName = "idregiao")]
        [Required(ErrorMessage = "Selecione uma região")]
        public int IDRegiao { get; set; }
        [JsonProperty(PropertyName = "iddistrito")]
        public int? IDDistrito { get; set; }
        [JsonProperty(PropertyName = "idigreja")]
        public int? IDIgreja { get; set; }
        [JsonProperty(PropertyName = "idcongregacao")]
        public int? IDCongregacao { get; set; }
        [JsonProperty(PropertyName = "idconjugue")]
        public int? IDConjugue { get; set; }
        [JsonProperty(PropertyName = "idestado")]
        public int? IDEstado { get; set; }
        [JsonProperty(PropertyName = "idgrauinstrucao")]
        public int? IDGrauInstrucao { get; set; }
        [JsonProperty(PropertyName = "idcidade")]
        public int? IDCidade { get; set; }
        [JsonProperty(PropertyName = "idnaturalidade")]
        public int? IDNaturalidade { get; set; }
        [JsonProperty(PropertyName = "idtipoconjugue")]
        public int? IDTipoConjugue { get; set; }
        [JsonProperty(PropertyName = "fotoescolhida")]
        public string FotoEscolhida { get; set; }

        public virtual ICollection<Regiao> Regioes { get; set; }
        public virtual ICollection<Estado> Estados { get; set; }
        public virtual ICollection<Cidade> Cidades { get; set; }
        public virtual ICollection<TipoConjugue> TiposConjugue { get; set; }
        public virtual ICollection<Cidade> Naturalidades { get; set; }

        public virtual ICollection<Distrito> Distritos { get; set; }
        public virtual ICollection<Congregacao> Congregacoes { get; set; }
        public virtual ICollection<GrauInstrucao> GrausInstrucao { get; set; }
        public virtual ICollection<Filho> Filhos { get; set; }
        public virtual ICollection<Igreja> Igrejas { get; set; }
    }
}