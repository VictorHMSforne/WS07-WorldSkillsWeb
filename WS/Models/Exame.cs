using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS.Models
{
    [Table("Exames")]
    public class Exame
    {
        [Key]
        public int ExameId { get; set; }

        [Required(ErrorMessage = "Por favor digite um nome de Exame")]
        public string NomeExame { get; set; }

        [Required(ErrorMessage = "Digite a duração do Exame")]
        public int Dias { get; set; }
    }
}
