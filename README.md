🧑‍💻 Teste Prático – Desenvolvedor FullStack

Este repositório contém a implementação de um teste técnico para a vaga de Desenvolvedor FullStack.
O projeto contempla funcionalidades de cadastro de clientes e gestão de beneficiários, com validações completas e integração com banco de dados.

📌 Funcionalidades Implementadas
👥 Clientes

-Inclusão do campo CPF como obrigatório no formulário de clientes.

-Validação completa do CPF:

-Formatação automática (999.999.999-99).

-Validação matemática do dígito verificador.

-Prevenção de duplicidade (não permite cadastrar dois clientes com o mesmo CPF).

-Inclusão do CPF na tabela CLIENTES.

🧾 Beneficiários

-Adicionado o botão "Beneficiários" na tela de cadastro de cliente.

-Exibição de um pop-up ao clicar no botão, contendo:

-Campo CPF (com máscara e validação, igual ao cadastro de clientes).

-Campo Nome.

-Listagem de beneficiários em grid com opções de edição e exclusão.

-Regras adicionais:

-Não permite cadastro de beneficiários com o mesmo CPF para um mesmo cliente.

-Cadastro simultâneo de cliente e beneficiários ao clicar em Salvar.

-Persistência dos dados na tabela BENEFICIARIOS, com as colunas:
  ID, CPF, NOME, IDCLIENTE.

🧪 Testes

Para os testes de validação de CPF e dados, foram utilizados dados fictícios gerados pelo site 4Devs
.

🛠️ Tecnologias Utilizadas

⚙️ .NET Framework 4.8 (SDK)

🌐 ASP.NET MVC

🎨 Bootstrap 3

💻 jQuery

🗄️ SQL Server (procedures para inclusão, alteração e exclusão)

🔢 jQuery Mask + lógica customizada para validação de CPF

👉 Este projeto foi desenvolvido com foco em boas práticas, validações robustas e organização de entidades relacionadas, garantindo a entrega do solicitado com eficiência.
