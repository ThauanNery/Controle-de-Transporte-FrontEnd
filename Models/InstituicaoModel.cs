using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class InstituicaoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NomeInstituicao { get; set; }
        public string CNPJ { get; set; }
    }
}
