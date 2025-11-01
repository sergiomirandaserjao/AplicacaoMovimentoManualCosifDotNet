-- Scripts para SQL Server: Criação de tabelas e stored procedure
CREATE TABLE PRODUTO (
    Cod_Produto VARCHAR(50) PRIMARY KEY,
    Des_Produto VARCHAR(200) NOT NULL,
    Sta_Status CHAR(1) DEFAULT 'A'
);

CREATE TABLE PRODUTO_COSIF (
    Cod_Produto VARCHAR(50) NOT NULL,
    Cod_Cosif VARCHAR(50) NOT NULL,
    Cod_Classificacao VARCHAR(50),
    Sta_Status CHAR(1) DEFAULT 'A',
    CONSTRAINT PK_PRODUTO_COSIF PRIMARY KEY (Cod_Produto, Cod_Cosif),
    CONSTRAINT FK_PRODUTO_COSIF_PRODUTO FOREIGN KEY (Cod_Produto) REFERENCES PRODUTO(Cod_Produto)
);

CREATE TABLE MOVIMENTO_MANUAL (
    Id_Movimento INT IDENTITY(1,1) PRIMARY KEY,
    Dat_Mes INT NOT NULL,
    Dat_Ano INT NOT NULL,
    Num_Lancamento INT NOT NULL,
    Cod_Produto VARCHAR(50) NOT NULL,
    Cod_Cosif VARCHAR(50) NOT NULL,
    Val_Valor DECIMAL(18,2) NOT NULL,
    Des_Descricao VARCHAR(1000),
    Dat_Movimento DATETIME NOT NULL DEFAULT GETDATE(),
    Cod_Usuario VARCHAR(100),
    CONSTRAINT FK_MOVIMENTO_PRODUTO FOREIGN KEY (Cod_Produto) REFERENCES PRODUTO(Cod_Produto),
    CONSTRAINT FK_MOVIMENTO_PRODUTO_COSIF FOREIGN KEY (Cod_Produto, Cod_Cosif) REFERENCES PRODUTO_COSIF(Cod_Produto, Cod_Cosif),
    CONSTRAINT UQ_MOVIMENTO_UNIQUE UNIQUE (Dat_Mes, Dat_Ano, Num_Lancamento, Cod_Produto, Cod_Cosif)
);

GO

CREATE PROCEDURE sp_ListaMovimentosPorMesAno
    @Dat_Ano INT = NULL,
    @Dat_Mes INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Dat_Mes AS Mes,
        Dat_Ano AS Ano,
        m.Cod_Produto AS Cod_Produto,
        p.Des_Produto AS Des_Produto,
        m.Num_Lancamento AS Nr_Lancamento,
        m.Des_Descricao AS Descricao,
        m.Val_Valor AS Valor
    FROM MOVIMENTO_MANUAL m
    INNER JOIN PRODUTO p ON p.Cod_Produto = m.Cod_Produto
    WHERE (@Dat_Ano IS NULL OR m.Dat_Ano = @Dat_Ano)
      AND (@Dat_Mes IS NULL OR m.Dat_Mes = @Dat_Mes)
    ORDER BY m.Dat_Ano, m.Dat_Mes, m.Num_Lancamento;
END;
