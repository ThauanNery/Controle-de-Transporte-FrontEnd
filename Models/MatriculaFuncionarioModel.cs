﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controle_de_Transporte_FrontEnd.Models
{
    public class MatriculaFuncionarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Matricula { get; set; }

        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        [JsonIgnore]
        public virtual FuncionariosModel? Funcionarios { get; set; }
    }
}
