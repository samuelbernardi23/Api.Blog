📌 TopCon API

📖 Sobre o projeto

Esta API foi desenvolvida utilizando ASP.NET Core com Identity para autenticação baseada em cookies. O objetivo é fornecer um backend robusto para um sistema de blog, permitindo a criação, leitura, atualização e exclusão de postagens.

🚀 Como rodar o projeto

📌 Pré-requisitos

Certifique-se de ter os seguintes softwares instalados:

- .NET 9 SDK

- PostgreSQL
  - A string de conexão definida no ```appsettings.Development.json``` é um banco de dados que deixei aberto para teste, caso deseje.


🛠 Configuração do ambiente

Clone o repositório:

```
git clone https://github.com/samuelbernardi23/Api.Blog.git
cd Api.Blog
```

Configure as variáveis no appsettings.json:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=topcon;Username=seu_usuario;Password=sua_senha"
  },
  "Jwt": {
    "Issuer": "topcon_api",
    "Audience": "topcon_client",
    "Key": "sua-chave-secreta"
  }
}
```

### Endpoints
A documentação completa pode ser acessada via Swagger após iniciar a API  no modo IIS Express


### Aplicar as migrations
```
dotnet ef database drop
dotnet ef database update
```

-> Rodar a API
Após executar a API pela primeira vez:
- Irá ser criado o usuário admin padrão para testes
- Cria as Roles necessárias
```
admin@admin.com
Admin@2025
```

### Certificado
- Deixei um certificado auto-assinado no código visando facilitar o teste, então não será necessário gerar um.

### Tecnologias utilizadas

🔹 Backend

- ASP.NET Core 9 - Framework principal da aplicação.

- Entity Framework Core - ORM para manipulação do banco de dados PostgreSQL.

- Identity com Cookie Authentication - Para autenticação baseada em cookies.

- Swashbuckle (Swagger) - Documentação automática da API.

- FluentValidation - Para validação das entradas de dados.


🔹 Banco de dados

- PostgreSQL - Banco de dados relacional utilizado no projeto.
