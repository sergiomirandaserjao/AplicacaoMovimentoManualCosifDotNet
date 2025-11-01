using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Entities;
using DAL.Interfaces;
using System.Configuration;

namespace DAL.Implementations
{
    public class MovimentoRepositoryAdo : IMovimentoRepository
    {
        private readonly string _connString;

        public MovimentoRepositoryAdo()
        {
            // Leitura simplificada: altere para usar IConfiguration ou similar
            var cfg = System.IO.File.ReadAllText("Config/connectionstrings.json"); // quick read
            // NOTA: em produção use IConfiguration e desserialização segura
            _connString = System.Text.Json.JsonDocument.Parse(cfg).RootElement.GetProperty("DefaultConnection").GetString();
        }

        public async Task<IEnumerable<MovimentoManual>> ListarPorMesAnoAsync(int? ano, int? mes)
        {
            var list = new List<MovimentoManual>();
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("sp_ListaMovimentosPorMesAno", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dat_Ano", (object)ano ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Dat_Mes", (object)mes ?? DBNull.Value);
                await cn.OpenAsync();
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(new MovimentoManual {
                            DatMes = dr.GetInt32(dr.GetOrdinal("Mes")),
                            DatAno = dr.GetInt32(dr.GetOrdinal("Ano")),
                            CodProduto = dr.GetString(dr.GetOrdinal("Cod_Produto")),
                            DesDescricao = dr.IsDBNull(dr.GetOrdinal("Descricao")) ? null : dr.GetString(dr.GetOrdinal("Descricao")),
                            ValValor = dr.GetDecimal(dr.GetOrdinal("Valor")),
                            NumLancamento = dr.GetInt32(dr.GetOrdinal("Nr_Lancamento")),
                            // DesProduto not mapped to entity - UI can show via join result if DTO used
                        });
                    }
                }
            }
            return list;
        }

        public async Task<int> GetUltimoLancamentoAsync(int ano, int mes)
        {
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("SELECT ISNULL(MAX(Num_Lancamento), 0) FROM MOVIMENTO_MANUAL WHERE Dat_Ano = @ano AND Dat_Mes = @mes", cn))
            {
                cmd.Parameters.AddWithValue("@ano", ano);
                cmd.Parameters.AddWithValue("@mes", mes);
                await cn.OpenAsync();
                var res = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
        }

        public async Task<int> InserirAsync(MovimentoManual movimento)
        {
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(@"
INSERT INTO MOVIMENTO_MANUAL (Dat_Mes, Dat_Ano, Num_Lancamento, Cod_Produto, Cod_Cosif, Val_Valor, Des_Descricao, Dat_Movimento, Cod_Usuario)
VALUES (@DatMes, @DatAno, @NumLancamento, @CodProduto, @CodCosif, @ValValor, @DesDescricao, @DatMovimento, @CodUsuario);
SELECT CAST(SCOPE_IDENTITY() as int);", cn))
            {
                cmd.Parameters.AddWithValue("@DatMes", movimento.DatMes);
                cmd.Parameters.AddWithValue("@DatAno", movimento.DatAno);
                cmd.Parameters.AddWithValue("@NumLancamento", movimento.NumLancamento);
                cmd.Parameters.AddWithValue("@CodProduto", movimento.CodProduto);
                cmd.Parameters.AddWithValue("@CodCosif", movimento.CodCosif);
                cmd.Parameters.AddWithValue("@ValValor", movimento.ValValor);
                cmd.Parameters.AddWithValue("@DesDescricao", (object)movimento.DesDescricao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DatMovimento", movimento.DatMovimento);
                cmd.Parameters.AddWithValue("@CodUsuario", (object)movimento.CodUsuario ?? DBNull.Value);
                await cn.OpenAsync();
                var id = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(id);
            }
        }
    }
}
