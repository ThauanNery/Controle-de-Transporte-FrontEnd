using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class TransporteModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TipoTransporteId { get; set; }
        public int FuncionarioId { get; set; }
        public int MatriculaTransporteId { get; set; }
        public int? ManutencaoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

       
        public virtual TipoDeTransporteModel? TipoTransportes { get; set; }
       
        public virtual FuncionariosModel? Funcionario { get; set; }
       
        public virtual MatriculaTransporteModel? MatriculaTransporte { get; set; }
       
        public virtual ManutencaoModel? Manutencao { get; set; }
    }
}
