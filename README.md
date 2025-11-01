Aplicação do Movimento Manual - Área Financeira - COSIF - DOT NET - Com todos as Tecnologias de Dot Net C# como Asp.Net MVC Core, Asp.Net MVC , Asp.Net Web Forms Site . Windows Forms , ORMs como Ado.Net , NHibernate, Linq To SQL, Dapper e EF (Entity Framework). Utilização de Stored Procedures e , SQL Server , Design Patterns e SOLID

Projeto Completo >

- Camadas bem definidas (Design Pattern - Pardrões de Projeto em Camadas)

Model → classes de entidade (MovimentoManual, PlanoConta, Cosif)

DAL → acesso ao banco com ADO.NET, SqlConnection, SqlCommand, SqlDataReader

BLL → regras de negócio e validações (ex: bloqueio de lançamentos já processados)   - Onde fica a Lógica do Negócio do Projeto  (O que POs e Analistas de Negócios explicam)

UI → ASP.NET WebForms (.aspx) e MVC (.cshtml) exemplos incluídos

- Banco de Dados SQL Server

Scripts .sql com CREATE TABLE e CREATE PROCEDURE

Stored procedure SP_INSERIR_MOVIMENTO_MANUAL pronta (inclui validações)

- ADO.NET puro

Código de conexão via SqlConnection

Comandos INSERT, UPDATE, SELECT, DELETE

Controle de transações
- Design Pattern aplicado

Repository Pattern (na DAL)

Camada de Negócio (Service Layer) — separa lógica do banco

MVC / N-tier architecture
→ separação de responsabilidades completa

- Boas práticas

Código comentado

Nomenclatura coerente (BLL, DAL, Models)

SQL otimizado com JOIN e PK/FK

- Pronto para compilar

Estrutura organizada por pastas (/App_Code, /Models, /DAL, /BLL, /UI)

.aspx e .cs funcionais
