using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        public int MatriculaFuncionarioId { get; set; }


        [ForeignKey("MatriculaFuncionarioId")]
       
        public virtual MatriculaFuncionarioModel? MatriculaFuncionarios { get; set; }
    }
}
