using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using ValidacaoDeSenha.Application.Interfaces;
using ValidacaoDeSenha.Application.ViewModel;

namespace ValidacaoDeSenha.Controllers
{
    [Route("ValidarSenha")]
    [ApiController]
    public class ValidarSenhaController : ControllerBase
    {
        private readonly IValidaSenhaService service;

        public ValidarSenhaController(IValidaSenhaService service)
        {
            this.service = service; 
        }

        [HttpGet("validar/{senha}")]
        public IActionResult Get(string senha)
        {
            SecureString secureString = new SecureString();
            SenhaResponseViewModel response = new SenhaResponseViewModel();

            foreach (char item in senha) secureString.AppendChar(item);
            senha = string.Empty;

            try
            {
                response = service.VerificacaoSenha(secureString);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.senhaIsValida = false;
                response.mensagem = ex.Message.ToString();
                return StatusCode(500, response);
            }
        }
    }
}
