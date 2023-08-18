Exemplo de API Rest em C#, utilizando CQRS e Mediatr.

Apresenta a segregação de responsabilidades entre Commands e Queries. Para este exemplo, foi utilizado
somente um banco de dados SQLite, porém, o objetivo é a demonstração da divisão das responsabilidades.

ToDoList.Api: Minimal API em C#, para receber as chamadas de algum frontend
ToDoList.Application: regras de negócio, onde são separados commands e queries
ToDoList.Application.Test: testes unitários das regras de negócio
ToDoList.Domain: dominio da aplicação, contém as entidades de negócio e interfaces
ToDoList.Infrastructure: camada de acesso a dados

Apesar de tratar apenas uma entidade simples, o projeto demonstra vários conceitos importantes que
podem ser aplicados em projetos reais, tais como a segregação das regras de negócio, regras de
domínio, testes unitários, minimal api, etc.

Todo o projeto foi implementado usando somente o Visual Studio Code. Para executar o projeto,
use o terminal (dentro da pasta raiz do projeto, onde está o arquivo sln):

    dotnet run --project .\src\ToDoList.Api

