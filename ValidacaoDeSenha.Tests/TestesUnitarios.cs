using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using ValidacaoDeSenha.Application.Interfaces;
using ValidacaoDeSenha.Application.Services;
using ValidacaoDeSenha.Application.ViewModel;
using Xunit;

namespace ValidacaoDeSenha.Tests
{
    public class TestesUnitarios
    {
        private readonly IValidaSenhaService validarSenha;
        public TestesUnitarios()
        {
            validarSenha = new ValidaSenhaService();
        }

        [Fact]
        public void TestaSenhaComSuceso()
        {
            string mensagem = "Senha validada com sucesso";
            SecureString senha = new SecureString();
            senha.AppendChar('A');
            senha.AppendChar('b');
            senha.AppendChar('c');
            senha.AppendChar('@');
            senha.AppendChar('e');
            senha.AppendChar('f');
            senha.AppendChar('g');
            senha.AppendChar('1');
            senha.AppendChar('x');
            senha.AppendChar('9');
            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.True(result.senhaIsValida);
            Assert.Equal(mensagem, result.mensagem);

        }

        [Fact]
        public void TestaSenhaMenorQueTamanhoEsperado()
        {
            string mensagemErro = "Senha deve ter 9 ou mais caracteres";
            SecureString senha = new SecureString();
            senha.AppendChar('A');
            senha.AppendChar('b');
            senha.AppendChar('c');
            senha.AppendChar('@');
            senha.AppendChar('e');
            senha.AppendChar('f');
            senha.AppendChar('g');
            senha.AppendChar('1');

            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.False(result.senhaIsValida);
            Assert.Equal(mensagemErro, result.mensagem);

        }


        [Fact]
        public void TestaSenhaComEspacos()
        {
            string mensagemErro = "Senha não deve possuir espaços";
            SecureString senha = new SecureString();
            senha.AppendChar('A');
            senha.AppendChar('b');
            senha.AppendChar('c');
            senha.AppendChar('@');
            senha.AppendChar('e');
            senha.AppendChar(' ');
            senha.AppendChar('g');
            senha.AppendChar('1');
            senha.AppendChar('H');
            senha.AppendChar('i');
            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.False(result.senhaIsValida);
            Assert.Equal(mensagemErro, result.mensagem);

        }

        [Fact]
        public void TestaSenhaSemDigitos()
        {
            string mensagemErro = "Senha deve ter ao menos 1 dígito";
            SecureString senha = new SecureString();
            senha.AppendChar('A');
            senha.AppendChar('b');
            senha.AppendChar('c');
            senha.AppendChar('@');
            senha.AppendChar('e');
            senha.AppendChar('f');
            senha.AppendChar('g');
            senha.AppendChar('e');
            senha.AppendChar('H');
            senha.AppendChar('i');
            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.False(result.senhaIsValida);
            Assert.Equal(mensagemErro, result.mensagem);

        }

        [Fact]
        public void TestaSenhaSemMinusculas()
        {
            string mensagemErro = "Senha deve ter ao menos 1 letra minúscula";
            SecureString senha = new SecureString();
            senha.AppendChar('A');
            senha.AppendChar('B');
            senha.AppendChar('C');
            senha.AppendChar('@');
            senha.AppendChar('E');
            senha.AppendChar('F');
            senha.AppendChar('G');
            senha.AppendChar('3');
            senha.AppendChar('H');
            senha.AppendChar('I');
            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.False(result.senhaIsValida);
            Assert.Equal(mensagemErro, result.mensagem);

        }

        [Fact]
        public void TestaSenhaSemMaiusculas()
        {
            string mensagemErro = "Senha deve ter ao menos 1 letra maiúscula";
            SecureString senha = new SecureString();
            senha.AppendChar('a');
            senha.AppendChar('b');
            senha.AppendChar('c');
            senha.AppendChar('@');
            senha.AppendChar('e');
            senha.AppendChar('f');
            senha.AppendChar('g');
            senha.AppendChar('3');
            senha.AppendChar('h');
            senha.AppendChar('i');
            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.False(result.senhaIsValida);
            Assert.Equal(mensagemErro, result.mensagem);

        }

        [Fact]
        public void TestaSenhaSemCaractereEspecial()
        {
            string mensagemErro = "Senha deve ter ao menos 1 caractere especial (!@#$%^&*()-+)";
            SecureString senha = new SecureString();
            senha.AppendChar('A');
            senha.AppendChar('b');
            senha.AppendChar('c');
            senha.AppendChar('d');
            senha.AppendChar('e');
            senha.AppendChar('f');
            senha.AppendChar('g');
            senha.AppendChar('1');
            senha.AppendChar('H');
            senha.AppendChar('i');
            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.False(result.senhaIsValida);
            Assert.Equal(mensagemErro, result.mensagem);

        }

        [Fact]
        public void TestaSenhaSemCaracteresRepetidos()
        {
            string mensagemErro = "Senha não deve possuir caracteres repetidos";
            SecureString senha = new SecureString();
            senha.AppendChar('A');
            senha.AppendChar('b');
            senha.AppendChar('c');
            senha.AppendChar('A');
            senha.AppendChar('e');
            senha.AppendChar('&');
            senha.AppendChar('g');
            senha.AppendChar('1');
            senha.AppendChar('H');
            senha.AppendChar('i');
            SenhaResponseViewModel result = validarSenha.VerificacaoSenha(senha);

            Assert.False(result.senhaIsValida);
            Assert.Equal(mensagemErro, result.mensagem);

        }
    }
}
