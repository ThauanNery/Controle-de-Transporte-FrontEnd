using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class FuncionariosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NomeFuncionario { get; set; }
        public int DepartamentoId { get; set; }
        public int CargoId { get; set; }

        [ForeignKey("DepartamentoId")]
        [JsonIgnore]
        public virtual DepartamentoModel? Departamento { get; set; }

        [ForeignKey("CargoId")]
        [JsonIgnore]
        public virtual CargoModel? Cargo  { get; set; }
    }
}
