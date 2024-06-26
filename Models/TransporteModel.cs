﻿using System.ComponentModel.DataAnnotations.Schema;
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
        public int MatriculaFuncionarioId { get; set; }
        public int MatriculaTransporteId { get; set; }
        public int ManutencaoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        [JsonIgnore]
        public virtual TipoDeTransporteModel? TipoDeTransporte { get; set; }
        [JsonIgnore]
        public virtual MatriculaFuncionarioModel? MatriculaFuncionario { get; set; }
        [JsonIgnore]
        public virtual MatriculaTransporteModel? MatriculaTransporte { get; set; }
        [JsonIgnore]
        public virtual ManutencaoModel? Manutencao { get; set; }
    }
}
