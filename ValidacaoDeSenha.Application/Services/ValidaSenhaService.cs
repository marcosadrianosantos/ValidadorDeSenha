using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using ValidacaoDeSenha.Application.Interfaces;
using ValidacaoDeSenha.Application.ViewModel;

namespace ValidacaoDeSenha.Application.Services
{
    public class ValidaSenhaService : IValidaSenhaService
    {
        private readonly List<ExpressaoValidacaoViewModel> expressaoValidacao;
        private SenhaResponseViewModel senhaResponse;

        public ValidaSenhaService()
        {
            expressaoValidacao = new List<ExpressaoValidacaoViewModel>
            {
                new ExpressaoValidacaoViewModel
                {
                    expressao = @"(?=^.{9,})",
                    erro = "Senha deve ter 9 ou mais caracteres"
                },

                new ExpressaoValidacaoViewModel
                {
                    expressao = @"(?=.*[0-9].{1,}$)",
                    erro = "Senha deve ter ao menos 1 dígito"
                },

                new ExpressaoValidacaoViewModel
                {
                    expressao = @"(?=.*[a-z].{1,}$)",
                    erro = "Senha deve ter ao menos 1 letra minúscula"
                },

                new ExpressaoValidacaoViewModel
                {
                    expressao = @"(?=.*[A-Z].{1,}$)",
                    erro = "Senha deve ter ao menos 1 letra maiúscula"
                },

                new ExpressaoValidacaoViewModel
                {
                    expressao = @"(?=.*[!@#$%^&*()-+].{1,}$)",
                    erro = "Senha deve ter ao menos 1 caractere especial (!@#$%^&*()-+)"
                }
            };
        }
        public SenhaResponseViewModel VerificacaoSenha(SecureString senha)
        {
            char espacoBranco = '\u0020';

            senhaResponse = new SenhaResponseViewModel
            {
                mensagem = "Senha Valida",
                senhaIsValida = true
            };

            try
            {
                if (ValidacaoPatteners(senha))
                {
                    if (ConvertToUnSecureString(senha).Contains(espacoBranco))
                    {
                        senhaResponse.mensagem = "Senha não pode possuir espaços em branco";
                        senhaResponse.senhaIsValida = false;
                    }
                    else
                    {
                        if (VerificarCaracterRepetido(senha))
                        {
                            senhaResponse.mensagem = "Senha não pode ter caracter repetido";
                            senhaResponse.senhaIsValida = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                senhaResponse.mensagem = string.Format("A senha inserida consta erro: {0}", ex.Message.ToString());
                senhaResponse.senhaIsValida = false;
            }

            return senhaResponse;
        }

        private bool ValidacaoPatteners(SecureString senha)
        {
            foreach (ExpressaoValidacaoViewModel expressaoValida in expressaoValidacao)
            {
                Regex regex = new Regex(expressaoValida.expressao, RegexOptions.None, new TimeSpan(0, 2, 0));

                if (!regex.Match(ConvertToUnSecureString(senha).Trim()).Success)
                {
                    senhaResponse.mensagem = expressaoValida.erro;
                    senhaResponse.senhaIsValida = false;
                }
            }

            return senhaResponse.senhaIsValida;
        }

        public bool VerificarCaracterRepetido(SecureString senha)
        {
            bool caracterRepetido = false;

            foreach (Char ch in ConvertToUnSecureString(senha))
            {
                int contador = ConvertToUnSecureString(senha).Count(x => x == ch);
                if(contador > 1)
                {
                    caracterRepetido = true;
                    break;
                }
            }
            return caracterRepetido;
        }

        private string ConvertToUnSecureString(SecureString senha)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(senha);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
