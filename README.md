<div align="center">
  <h1>Desafio do Balta.io</h1>
</div>

> Este projeto faz parte de uma série de desafios da comunidade balta.io.
> Nesta primeira etapa, foi necessário criar uma API básica para o cadastro de usuários e a realização de operações CRUD em uma única entidade.

<br>

<p align="center">
  <img alt="GitHub language count" src="https://img.shields.io/github/languages/count/georgeguii/desafio-balta-api-ibge">
  <img alt="GitHub top language" src="https://img.shields.io/github/languages/top/georgeguii/desafio-balta-api-ibge">
  <img alt="GitHub issues" src="https://img.shields.io/github/issues/georgeguii/desafio-balta-api-ibge">
  <img alt="GitHub issues" src="https://img.shields.io/github/issues-raw/georgeguii/desafio-balta-api-ibge">
</p>

[Confira o projeto aqui](https://desafio-api-balta-io.azurewebsites.net/swagger/index.html)

### Gerenciamento de Localidades e Usuários
Este é um projeto básico onde é possível realizar operações de CRUD para as seguintes entidades:
- IBGE (localidade)
- Usuários

## Objetivo
<p align="justify">
  Este projeto foi desenvolvido como resposta a um desafio proposto na comunidade <a href="https://balta.io">balta.io</a>, visando promover o trabalho em equipe, aumentar o networking,
  aplicar conceitos, o engajamento da comunidade, aprimorar habilidades, adquirir conhecimento e promover oportunidades. O desafio destaca a habilidade dos participantes em desenvolver,
  gerenciar dados de forma eficiente e criar soluções que atendam às necessidades dos usuários,
 promovendo a colaboração e inovação na comunidade de tecnologia.
</p>

 ### Features

- [X] Listar localidades
- [X] Adicionar localidades
- [X] Buscar localidades (código do IBGE, cidade e estado)
- [X] Atualizar localidades
- [X] Apagar localidades
- [X] Adiciona usuários
- [X] Ativar conta
- [X] Editar usuários (nome, email e senha)
- [X] Apagar usuários
- [X] Realizar login

## Tecnologias
 No back-end serão utilizadas as principais tecnologias em torno da stack .NET, sendo algumas delas:
 - C# 11
 - .NET 7.0
 - Entity Framework Core 7
 - ASP.NET Core Minimal API
 - Fluent Validation
 - MSTest
 - Moq
 - AutoFixture
 - BCrypt
 - SQL Server
 - Git e Github
 - Swagger UI
 - JWT e Bearer Authentication
 - Azure

## Arquitetura / Design Patterns
 - RESTful
 - Domain Driven Design (DDD)
 - Repository Pattern
 - Clean Architecture
 - Command Query Responsibility Segregation (CQRS)
 - Unit of Work (UoW)
 - Design By Contract
 - Injeção de dependência

<hr>
 
## Instalando e preparando o ambiente

### Pré-Requisitos
- .NET Core 7 SDK 
- Microsoft SQL Server
- MS SQl Server (ou outro SGBD de sua escolha)

### Clonando o repositório
1. Crie um diretório para o projeto no seu computador.
2. Abra o terminal no repositório criado.
3. Execute o comando abaixo:

```bash
git clone https://github.com/georgeguii/desafio-balta-api-ibge
```

## Configurando e executando o projeto
1. Abra o projeto que acabou de clonar com sua IDE favorita;
2. Configure o Arquivo appsettings.json com suas variáveis:
```json
{
  "ConnectionStrings": {
      "Database": "my_connection_string"
    },
    "Secrets": {
      "JwtPrivateKey": "my_jwt_private_key"
    }
}
```

### Visual Studio Code
3. Entre na pasta do Desafio-Balta-IBGE.API;
4. Execute o comando no terminal
   
```sh
$ dotnet ef database update
```

5. Em seguida, execute o projeto com o comando:
   
```sh
$ dotnet run
```

### Visual Studio
3. Defina o projeto Desafio-Balta-IBGE.API como inicialização
4. Abra o Console de Gerenciador de Pacotes do NuGet
5. Execute o comando:
   
```sh
$ Update-Database
```

5. Após a criação do banco de dados, execute o projeto pressionando o botão "Iniciar" ou pressionando F5.
6. Ao executar com sucesso, o Swagger será aberto:
   
![Swagger que sera aberto ao executar a aplicação com sucesso](https://github.com/georgeguii/desafio-balta-api-ibge/assets/83482242/5e86c063-7112-4e7f-8740-0f61427c265f)
 
 <hr>

 ## Padrões
 ### Métodos
Requisições para a API devem seguir os padrões:
| Método   | Descrição                                             |
|----------|-------------------------------------------------------|
| `GET`    | Retorna informações de um ou mais registros.          |
| `POST`   | Utilizado para criar um novo registro.                |
| `PUT`    | Atualiza dados de um registro ou altera sua situação. |
| `DELETE` | Remove/Desativa um registro do sistema.               |
 
 <hr>
 
 ### Respostas

| Código| Descrição |
|-------|-------------------------------------------------------------------|
| `200` | Requisição executada com sucesso.                                 |
| `201` | Objeto criado com sucesso.                                        |
| `400` | Erros de validação ou os campos informados não existem no sistema.|
| `401` | Dados de acesso inválidos.                                        |
| `404` | Registro pesquisado não encontrado.                               |
| `409` | Objeto entrou em conflito com outro.                              |

---

Feito com ♥ by George Silva e Guilherme Beltran :wave:
