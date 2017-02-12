using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using IgrejaOnlineWesleyana.Extensions;
using IgrejaOnlineWesleyana.Models;
using Newtonsoft.Json;

namespace IgrejaOnlineWesleyana.ViewModels
{
    [Validator(typeof(CPFValidatorViewModel))]
    public class FichaCadastralViewModel
    {
        [JsonProperty(PropertyName="id")]
        public int Id { get; set; }

        //[Required]
        [Display(Name = "Nome completo")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome é muito curto")]
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "O email é inválido")]
        [StringLength(50)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = true)]
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
        [JsonIgnore]
        public Regiao Regiao { get; set; }
        [JsonIgnore]
        public Distrito Distrito { get; set; }
        [JsonIgnore]
        public Igreja Igreja { get; set; }
        [JsonIgnore] 
        public Congregacao Congregacao { get; set; }
        //public Conjuge ConjugeMembro { get; set; }
        [JsonIgnore]
        public Estado Estado { get; set; }
        public byte[] Foto { get; set; }


        [StringLength(100)]
        [JsonProperty(PropertyName = "nomeconjuge")]
        public string NomeConjuge { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = true)]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "datanascimentoconjuge")]
        public DateTime? DataNascimentoConjuge { get; set; }

        [StringLength(50)]
        [JsonProperty(PropertyName = "telefoneconjuge")]
        public string TelefoneConjuge { get; set; }
        [JsonProperty(PropertyName = "idtipo")]
        public int? IDTipo { get; set; }

        [JsonProperty(PropertyName = "emailconjuge")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        [StringLength(50)]
        public string EmailConjuge { get; set; }
        [JsonProperty(PropertyName = "idesposa")]
        public int? IDEsposa { get; set; }



        public Filho Filho { get; set; }
        //public GrauInstrucao GrauInstrucao { get; set; }
        [JsonProperty(PropertyName = "idregiao")]
        //[Required(ErrorMessage = "Selecione uma região")]
        public int IDRegiao { get; set; }
        [JsonProperty(PropertyName = "iddistrito")]
        public int? IDDistrito { get; set; }
        [JsonProperty(PropertyName = "idigreja")]
        public int? IDIgreja { get; set; }
        [JsonProperty(PropertyName = "idcongregacao")]
        public int? IDCongregacao { get; set; }
        [JsonProperty(PropertyName = "idconjuge")]
        public int? IDConjuge { get; set; }
        [JsonProperty(PropertyName = "idestado")]
        public int? IDEstado { get; set; }
        [JsonProperty(PropertyName = "idgrauinstrucao")]
        public int? IDGrauInstrucao { get; set; }
        [JsonProperty(PropertyName = "idcidade")]
        public int? IDCidade { get; set; }
        [JsonProperty(PropertyName = "idnaturalidade")]
        public int? IDNaturalidade { get; set; }
        [JsonProperty(PropertyName = "idtipoconjuge")]
        public int? IDTipoConjuge { get; set; }
        [JsonProperty(PropertyName = "fotoescolhida")]
        public string FotoEscolhida { get; set; }

        [JsonIgnore]
        public List<Regiao> Regioes { get; set; }
        [JsonIgnore]

        public List<Estado> Estados { get; set; }
        [JsonIgnore]

        public List<Cidade> Cidades { get; set; }
        [JsonIgnore]

        public List<TipoConjuge> TiposConjuge { get; set; }
        [JsonIgnore]

        public List<Cidade> Naturalidades { get; set; }
        [JsonIgnore]

        public List<Distrito> Distritos { get; set; }
        [JsonIgnore]

        public List<Congregacao> Congregacoes { get; set; }
        [JsonIgnore]

        public List<GrauInstrucao> GrausInstrucao { get; set; }
        public List<Filho> Filhos { get; set; }
        [JsonIgnore]
        public List<Igreja> Igrejas { get; set; }
    }
}