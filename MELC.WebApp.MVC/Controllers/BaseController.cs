using Microsoft.AspNetCore.Mvc;
using MELC.WebApp.MVC.Models;
using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        public ICollection<string> Erros { get; set; } = new List<string>();
        protected bool ValidarResposta(ResponseResult responseResult)
        {
            if (responseResult != null && responseResult.Errors.Messages.Any())
            {
                foreach (var mensagem in responseResult.Errors.Messages)
                {
                    Erros.Add(mensagem);
                }

                return true;
            }

            return false;
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            ModelState.AddModelError(string.Empty, erro);
        }
    }
}
