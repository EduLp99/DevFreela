# DevFreela

##Projeto de estudo de uma plataforma Freelancer usando .NET 7.0

**DevFreela** foi desenvolvido com o objetivo de fixação dos conceitos de **Arquitetura Limpa**, **Padrão Repository**, **CQRS**, **JWT** e **Testes Unitários**. É uma API onde os usuários podem se cadastrar como **Freelancers** e/ou **Clientes**, ofertando projetos (no caso de clientes) e prestando serviços (no caso de freelancers).

### Funcionalidade principal
Os clientes podem publicar projetos com as informações iniciais (título, descrição, valor), e freelancers podem se candidatar para executar esses projetos.

## Tecnologias Utilizadas ⌨️
- ASP.NET Core 7
- Entity Framework Core
- SQL Server
- XUnit
- Autenticação e Autorização com JWT Bearer

## Funcionalidades ⚙️
- **Cadastro de usuários** (Cliente e Freelancer)
- **Login de usuários** utilizando autenticação e autorização via JWT
- **CRUD de Projetos:** Apenas o cliente tem permissão para criar, editar e excluir projetos
- **Comentários em projetos:** Clientes e freelancers podem deixar comentários para comunicação sobre o projeto
- **Status do projeto:** Cliente pode iniciar (Start) e finalizar (Finish) o projeto

## Padrões, conceitos e arquitetura utilizada 📂
- Padrão Repository
- Arquitetura Limpa
- CQRS
- Fluent Validation para validação de API
- Testes unitários com XUnit e NSubstitute
