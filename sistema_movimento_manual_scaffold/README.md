# Sistema Movimento Manual - Scaffold
Scaffold gerado automaticamente: SQL Server + ADO.NET.
Contém estrutura de camadas (Domain, DAL (ADO.NET), BLL, UI (ASP.NET Core MVC, WebForms, WinForms)) e scripts SQL.

## Como usar
- Abra a pasta no Visual Studio.
- Restaure referências, ajuste connection string no arquivo `Config/connectionstrings.json` ou diretamente nos arquivos de DAL.
- Execute o script `scripts/sqlserver.sql` em seu banco SQL Server para criar tabelas e stored procedure.

