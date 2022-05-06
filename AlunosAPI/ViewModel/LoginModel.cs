using System.ComponentModel.DataAnnotations;

namespace AlunosAPI.ViewModel
{
    public class LoginModel
    {
        [Required( ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inváldo")]
        public string Email { get; set;}

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(25, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caractees.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
