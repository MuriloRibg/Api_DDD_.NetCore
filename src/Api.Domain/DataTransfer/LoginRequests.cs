using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DataTransfer
{
    public class LoginRequests
    {
        [Required(ErrorMessage = "O campo email é obrigário!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        [StringLength(100, ErrorMessage = "O email não pode ter mais de {1} caracteres!")]
        public string Email { get; set; }
    }
}
