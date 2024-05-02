using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class ManutencaoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite um Tipo Manutenção")]
        public string TipoManutencao { get; set; }

        [Required(ErrorMessage = "Digite um Valor")]
        public string Custo { get; set; }
    }
}
