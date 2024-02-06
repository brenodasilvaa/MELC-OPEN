using System.ComponentModel.DataAnnotations;

namespace MELC.Identidade.API.Models
{
    public class UsuarioRegistro
    {
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Compare("Senha", ErrorMessage = "As senhas não são iguais.")]
        public string SenhaConfirmacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool IsAdmin { get; set; }
    }

    public class UsuarioLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string Senha { get; set; } = string.Empty;
    }

    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; } = string.Empty;
        public double ExpiraEm { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }

    public class UsuarioToken
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class Usuario
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UsuarioUpdateDelete
    {
        public bool Completed { get; set; }
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
