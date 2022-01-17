using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using ValidacaoDeSenha.Application.ViewModel;

namespace ValidacaoDeSenha.Application.Interfaces
{
    public interface IValidaSenhaService
    {
        SenhaResponseViewModel VerificacaoSenha(SecureString senha);
        bool VerificarCaracterRepetido(SecureString senha);
    }
}
