using System.ComponentModel.DataAnnotations;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite um Login Valido")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite uma Senha Valida")]
        public string Senha { get; set; }
    }
}
