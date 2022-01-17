using System;
using System.Collections.Generic;
using System.Text;

namespace ValidacaoDeSenha.Application.ViewModel
{
    public class SenhaResponseViewModel
    {
        public bool senhaIsValida { get; set; }
        public string mensagem { get; set; }
    }
}
