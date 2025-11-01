using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Entities;

namespace BLL.Services
{
    public class MovimentoService
    {
        private readonly IMovimentoRepository _repo;
        public MovimentoService(IMovimentoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MovimentoManual>> Listar(int? ano, int? mes) => await _repo.ListarPorMesAnoAsync(ano, mes);

        public async Task<int> InserirMovimentoAsync(MovimentoManual movimento)
        {
            if (movimento.DatMes < 1 || movimento.DatMes > 12) throw new ArgumentException("Mês inválido");
            if (movimento.DatAno < 1900) throw new ArgumentException("Ano inválido");

            var ultimo = await _repo.GetUltimoLancamentoAsync(movimento.DatAno, movimento.DatMes);
            movimento.NumLancamento = ultimo + 1;
            movimento.DatMovimento = DateTime.Now;

            return await _repo.InserirAsync(movimento);
        }
    }
}
