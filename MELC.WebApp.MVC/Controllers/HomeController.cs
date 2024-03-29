﻿using Microsoft.AspNetCore.Mvc;
using MELC.WebApp.MVC.Models;
using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return LocalRedirect("~/Clientes");
        
        return Redirect("Login");
    }

    [Route("erro/{id:length(3,3)}")]
    public IActionResult ErroTratamento(int id)
    {
        var modelErro = new ErrorViewModel();

        if (id == 500)
        {
            modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            modelErro.Titulo = "Ocorreu um erro!";
            modelErro.ErroCode = id;
        }
        else if (id == 404)
        {
            modelErro.Mensagem =
                "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
            modelErro.Titulo = "Ops! Página não encontrada.";
            modelErro.ErroCode = id;
        }
        else if (id == 403)
        {
            modelErro.Mensagem = "Você não tem permissão para fazer isto.";
            modelErro.Titulo = "Acesso Negado";
            modelErro.ErroCode = id;
        }
        else
        {
            return StatusCode(404);
        }

        return View("~/Views/Shared/Error.cshtml", modelErro);
    }
}
