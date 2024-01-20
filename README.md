
<h1>PetApi</h1>

  

PetApi é uma API de cadastro de itens para pets, desenvolvida com ASP.NET Core, SQL Server e Entity Framework.

  

## Tecnologias

  

- ASP.NET Core 8.1

- SQL Server 2019

- Entity Framework Core 8.1

- AutoMapper 10.1.1

- FluentValidation 10.3.0

- Swashbuckle.AspNetCore 6.5.0

  

## Funcionalidades

  

- CRUD de itens para pets, como ração, brinquedos, acessórios, etc.

- Validação dos dados de entrada usando FluentValidation

- Mapeamento dos modelos de domínio para os modelos de apresentação usando AutoMapper

- Documentação da API usando Swagger

## Detalhes

API usa os ViewModels para que somente os modelos de Json precisassem somente dos itens necessários.

Dentro de ViewModels ainda possui um arquivo chamado ResultViewModel.cs que padroniza os retornos dos EndsPoints facilitando a vida do Frontend.




## Instalação

  

Para executar a API localmente, você precisa ter instalado o .NET 8 SDK e o SQL Server 2019. Você também pode usar o Docker para rodar o SQL Server em um contêiner.

  

Clone o repositório do GitHub:

  

```bash

git  clone  https://github.com/gustavotorrezan1/PetApi.git

```

  

Entre na pasta do projeto:

  

```bash

cd  PetApi

```

  

Crie o banco de dados usando o comando:

  

```bash

dotnet  ef  database  update

```

  

Inicie a API usando o comando:

  

```bash

dotnet  run

```

  

A API estará disponível em http://localhost:5267 e a documentação do Swagger em http://localhost:5267/swagger.

  

## Uso

  

Você pode usar o Swagger UI ou qualquer cliente HTTP, como Postman ou Insomnia, para testar a API.

  

## Licença

  

[MIT]

```

  

Você pode copiar e colar esse código no seu repositório do GitHub ou usar algum editor online de markdown, como o [StackEdit], para visualizar o resultado. Espero que isso atenda às suas expectativas. 😊