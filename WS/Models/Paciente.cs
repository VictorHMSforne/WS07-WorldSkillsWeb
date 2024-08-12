using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome do Paciente!")]
        public string Nome { get; set; }

        public int ExameId { get; set; }
        public Exame Exame { get; set; }

        public DateTime Data { get; set; }
        public DateTime DataPronto { get; set; }
    }
}
