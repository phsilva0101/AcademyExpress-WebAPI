using System.ComponentModel.DataAnnotations;

namespace AlunosAPI.Model
{
    public class Alunos
    {
        public long Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string CPF { get; set; }

        public string Cidade { get; set; }

        [Required]
        public int Idade { get; set; }

    }
}
