using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace MELC.WebApp.MVC.Models.Users
{
    public class UsuarioRegistro
    {
        public UsuarioRegistro()
        {
            IsAdmin = false;
        }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Usuário")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Nome completo")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
        public string Senha { get; set; }

        [DisplayName("Confirme sua senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string SenhaConfirmacao { get; set; }

        [DisplayName("É administrador")]
        public bool IsAdmin { get; set; }
    }
}