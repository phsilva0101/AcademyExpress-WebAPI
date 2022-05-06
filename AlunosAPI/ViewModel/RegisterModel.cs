using System.ComponentModel.DataAnnotations;

namespace AlunosAPI.ViewModel
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirma senha")]
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais")]
        public string ConfirmPassword { get; set; }
    }
}
