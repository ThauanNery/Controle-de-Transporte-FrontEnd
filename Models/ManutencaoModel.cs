using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class ManutencaoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string TipoManutencao { get; set; }

        [Required]
        public string Custo { get; set; }
    }
}
