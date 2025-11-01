using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<IEnumerable<MovimentoManual>> ListarPorMesAnoAsync(int? ano, int? mes);
        Task<int> GetUltimoLancamentoAsync(int ano, int mes);
        Task<int> InserirAsync(MovimentoManual movimento);
    }
}
