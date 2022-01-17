Projeto: Teste para validação de senha
Linguagem: C#, utilizando .Net Core 3.1

Descrição: Criar uma aplicação no qual tenho a entrada de uma senha e faço a verificação da mesma para saber se ela corresponde as validações solicitadas pelo teste.

Solução: Primeiramente separei o projeto por camadas de responsabilidade, sendo assim a controller só chama a service e por ventura a service por si, fará a validação da senha inserida. Acabei utilizando SecureString, pois o mesmo impede a leitura da variavel em dumps de memória. Para resolver este problema, acabei optando por utilizar as expressões regulares, já que a service é a responsável por conter minha regra de negócio.

Regra para realizar o teste:

    -Nove ou mais caracteres
    -Ao menos 1 dígito
    -Ao menos 1 letra minúscula
    -Ao menos 1 letra maiúscula
    -Ao menos 1 caractere especial
    -Considere como especial os seguintes caracteres: !@#$%^&*()-+
    -Não possuir caracteres repetidos dentro do conjunto
