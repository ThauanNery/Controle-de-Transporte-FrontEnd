using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class DepartamentoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NomeDepartamento { get; set; }
        public int InstituicaoId { get; set; }

        [ForeignKey("InstituicaoId")]
        [JsonIgnore]
        public virtual InstituicaoModel? Instituicao { get; set;}
    }
}
