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

        public int FuncionarioId { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }

        [ForeignKey("FuncionarioId")]

        public virtual FuncionariosModel? Funcionarios { get; set; }


    }
}

